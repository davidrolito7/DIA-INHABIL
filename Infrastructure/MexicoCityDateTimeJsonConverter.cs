using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ApiDiaInhabil.Infrastructure;

public sealed class MexicoCityDateTimeJsonConverter : JsonConverter<DateTime>
{
    private const string Formato = "yyyy-MM-dd'T'HH:mm:ss.fffffff";

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert,
        JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
            throw new JsonException("La fecha debe ser una cadena ISO 8601.");

        var valor = reader.GetString();
        if (!DateTime.TryParse(valor, CultureInfo.InvariantCulture,
                DateTimeStyles.RoundtripKind, out var fecha))
            throw new JsonException("La fecha no tiene un formato válido.");

        return MexicoCityDateTime.Convertir(fecha);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value,
        JsonSerializerOptions options) =>
        writer.WriteStringValue(MexicoCityDateTime.Convertir(value).ToString(Formato,
            CultureInfo.InvariantCulture));
}
