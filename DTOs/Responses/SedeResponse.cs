using ApiDiaInhabil.DTOs.Responses;

namespace ApiDiaInhabil.DTOs;

public class SedeResponse
{
	public int Id { get; set; }
	public string Nombre { get; set; } = string.Empty;
	public string? Descripcion { get; set; }
	public bool Activo { get; set; }
	public int NumeroAreas { get; set; }
	public List<AreaResponse> Areas { get; set; } = [];
	public List<DiaInhabilAreaResponse> AreasExcluidas { get; set; } = [];
}
