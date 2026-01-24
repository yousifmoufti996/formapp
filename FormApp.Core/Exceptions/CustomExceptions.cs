using System.Text.Json;

namespace FormApp.Core.Exceptions;

public class ExceptionModel
{
    public int HttpStatusCode { get; set; }
    public int StatusCode { get; set; }
    public string? Message { get; set; }

    public override string ToString() => JsonSerializer.Serialize(this);
}

public class NotFoundException : Exception
{
    public int StatusCode { get; }

    public NotFoundException(string message, int statusCode = 404) : base(message)
    {
        StatusCode = statusCode;
    }

    public NotFoundException(string message) : base(message)
    {
        StatusCode = 404;
    }
}

public class BadRequestException : Exception
{
    public int StatusCode { get; }

    public BadRequestException(string message, int statusCode = 400) : base(message)
    {
        StatusCode = statusCode;
    }

    public BadRequestException(string message) : base(message)
    {
        StatusCode = 400;
    }
}

public class UnauthorizedException : Exception
{
    public int StatusCode { get; }

    public UnauthorizedException(string message, int statusCode = 401) : base(message)
    {
        StatusCode = statusCode;
    }

    public UnauthorizedException(string message) : base(message)
    {
        StatusCode = 401;
    }
}
