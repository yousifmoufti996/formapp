using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using Serilog;
using FormApp.Application.Interfaces;
using FormApp.Application.Services;
using FormApp.Application.Validators;
using FormApp.Core.Entities;
using FormApp.Infrastructure.Data;
using FormApp.API.Services;
using FormApp.Helper.Logger;
using FormApp.API.Extensions;
using FormApp.API.Filters;
using FormApp.API.Configuration;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
LoggerConfig.ConfigureLogger();
builder.Host.UseSerilog();

// Add services to the container
builder.Services.AddControllers();

// Add memory cache for rate limiting
builder.Services.AddMemoryCache();

// Configure rate limit settings from appsettings
builder.Services.Configure<RateLimitSettings>(
    builder.Configuration.GetSection(RateLimitSettings.SectionName));

// Configure rate limiting
builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
    {
        return RateLimitPartition.GetNoLimiter("no-limit");
    });

    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

// Configure FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();

// Configure Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Identity
builder.Services.AddIdentity<User, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Configure Identity options
builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 6;
    
    // User settings
    options.User.RequireUniqueEmail = true;
});

// Configure JWT Authentication
var jwtSecret = builder.Configuration["Jwt:SecretKey"] ?? throw new InvalidOperationException("JWT Secret not configured");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
        ClockSkew = TimeSpan.Zero
    };
    
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault();
            if (!string.IsNullOrEmpty(token) && !token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                // If token doesn't have "Bearer " prefix, add it
                context.Request.Headers["Authorization"] = "Bearer " + token;
            }
            return Task.CompletedTask;
        },
        OnAuthenticationFailed = context =>
        {
            Log.Error("Authentication failed: " + context.Exception.Message);
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            Log.Information("Token validated for user: " + context.Principal?.Identity?.Name);
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization();

// Register services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();

// Register repositories
builder.Services.AddScoped<FormApp.Core.IRepositories.IUserRepository, FormApp.Infrastructure.Repositories.UserRepository>();
builder.Services.AddScoped<FormApp.Core.IRepositories.IUploadedFileRepository, FormApp.Infrastructure.Repositories.UploadedFileRepository>();
// Normalized repositories
builder.Services.AddScoped<FormApp.Core.IRepositories.ITransactionRepository, FormApp.Infrastructure.Repositories.TransactionRepository>();
builder.Services.AddScoped<FormApp.Core.IRepositories.ISubscriberRepository, FormApp.Infrastructure.Repositories.SubscriberRepository>();
builder.Services.AddScoped<FormApp.Core.IRepositories.ISubscriptionRepository, FormApp.Infrastructure.Repositories.SubscriptionRepository>();
builder.Services.AddScoped<FormApp.Core.IRepositories.IMeterScaleRepository, FormApp.Infrastructure.Repositories.MeterScaleRepository>();
builder.Services.AddScoped<FormApp.Core.IRepositories.ISubscriptionViolationRepository, FormApp.Infrastructure.Repositories.SubscriptionViolationRepository>();
builder.Services.AddScoped<FormApp.Core.IRepositories.ITransformerRepository, FormApp.Infrastructure.Repositories.TransformerRepository>();
builder.Services.AddScoped<FormApp.Core.IRepositories.ITransactionAttachmentRepository, FormApp.Infrastructure.Repositories.TransactionAttachmentRepository>();

// Register business services
builder.Services.AddScoped<ITransactionRecordService, TransactionRecordService>();
builder.Services.AddScoped<ITransactionAttachmentService, TransactionAttachmentService>();

// Register file services
var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
builder.Services.AddSingleton<IFileService>(new FileService(uploadPath));
builder.Services.AddSingleton<ISecureImageService>(sp => 
{
    var logger = sp.GetRequiredService<ILogger<SecureImageService>>();
    return new SecureImageService(uploadPath, logger);
});

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "FormApp API",
        Version = "v1",
        Description = "API for FormApp with authentication"
    });

    // Add JWT Authentication to Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
    
    // Configure enums to show as integers with descriptions
    c.SchemaFilter<EnumSchemaFilter>();
});

var app = builder.Build();

// Configure exception handling
app.ConfigureExceptionHandler();

// Configure the HTTP request pipeline
// Enable Swagger in all environments
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "FormApp API v1");
    c.RoutePrefix = "swagger";
});

// Disable HTTPS redirection for now (use reverse proxy for HTTPS)
// app.UseHttpsRedirection();

// Enable static files for uploads
app.UseStaticFiles();

app.UseCors("AllowAll");

// Log Authorization header for debugging
app.Use(async (context, next) =>
{
    var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
    Log.Information($"Request to: {context.Request.Path} | Authorization Header: {(string.IsNullOrEmpty(authHeader) ? "MISSING" : authHeader.Substring(0, Math.Min(50, authHeader.Length)) + "...")}");
    await next();
});

// Enable rate limiting
app.UseRateLimiter(); // Uncomment to enable in production

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Seed database
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await FormApp.Infrastructure.Data.Seeders.DatabaseSeeder.SeedAsync(userManager, dbContext);
}

// Log application start
Log.Information("FormApp API starting...");

app.Run();
