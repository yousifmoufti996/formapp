using System.ComponentModel;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FormApp.API.Filters;

public class EnumSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (!context.Type.IsEnum) return;

        schema.Enum.Clear();
        schema.Type = "integer";
        schema.Format = "int32";

        var enumValues = Enum.GetValues(context.Type);
        var descriptions = new List<string>();

        foreach (var enumValue in enumValues)
        {
            var intValue = Convert.ToInt32(enumValue);
            schema.Enum.Add(new OpenApiInteger(intValue));

            var memberInfo = context.Type.GetMember(enumValue.ToString()!).FirstOrDefault();
            var descriptionAttribute = memberInfo?.GetCustomAttributes(typeof(DescriptionAttribute), false)
                .Cast<DescriptionAttribute>()
                .FirstOrDefault();

            var description = descriptionAttribute?.Description ?? enumValue.ToString()!;
            descriptions.Add($"{intValue} = {description}");
        }

        schema.Description = string.Join(", ", descriptions);
    }
}
