using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataAccess.EFCore.Converter
{
    public class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
    {
        private static readonly string[] TimeFormats = { "HH:mm:ss", "H:mm:ss", "HH:mm", "H:mm" };

        public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            foreach (var format in TimeFormats)
            {
                if (TimeOnly.TryParseExact(value, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var time))
                {
                    return time;
                }
            }
            throw new FormatException($"String '{value}' was not recognized as a valid TimeOnly.");
        }

        public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("HH:mm:ss", CultureInfo.InvariantCulture));
        }
    }
}
