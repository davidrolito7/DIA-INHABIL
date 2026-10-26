namespace ApiDiaInhabil.DTOs.Responses;

public class DiaInhabilResponse
{
	public int Id { get; init; }
	public string Nombre { get; init; } = string.Empty;
	public string? Descripcion { get; init; }
	public DateTime FechaInicio { get; init; }
	public DateTime FechaFin { get; init; }
	public bool Activo { get; init; }
}
