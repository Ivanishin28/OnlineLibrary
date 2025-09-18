using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Host.WebApi.Web.JsonConverters;

public class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    private readonly string _serializationFormat;

    public DateOnlyJsonConverter() : this("yyyy-MM-dd") { }

    public DateOnlyJsonConverter(string serializationFormat)
    {
        _serializationFormat = serializationFormat;
    }

    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (DateTime.TryParse(reader.GetString(), out var dateTime))
        {
            return DateOnly.FromDateTime(dateTime);
        }

        throw new JsonException("Invalid date format for DateOnly");
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(_serializationFormat));
    }
}

