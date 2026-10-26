namespace ApiDiaInhabil.Infrastructure;

public static class MexicoCityDateTime
{
    private static readonly TimeZoneInfo ZonaHoraria = ObtenerZonaHoraria();

    public static DateTime Ahora() => TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, ZonaHoraria);

    public static DateTime Convertir(DateTime fecha) => fecha.Kind switch
    {
        DateTimeKind.Utc => TimeZoneInfo.ConvertTimeFromUtc(fecha, ZonaHoraria),
        DateTimeKind.Local => TimeZoneInfo.ConvertTime(fecha, ZonaHoraria),
        _ => DateTime.SpecifyKind(fecha, DateTimeKind.Unspecified)
    };

    private static TimeZoneInfo ObtenerZonaHoraria()
    {
        try
        {
            return TimeZoneInfo.FindSystemTimeZoneById("America/Mexico_City");
        }
        catch (TimeZoneNotFoundException)
        {
            return TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)");
        }
    }
}
