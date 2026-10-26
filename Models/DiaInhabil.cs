namespace ApiDiaInhabil.Models;

public class DiaInhabil
{
	public int Id { get; set; }
	public string Nombre { get; set; } = string.Empty;
	public string? Descripcion { get; set; }
	public DateTime FechaInicio { get; set; }
	public DateTime FechaFin { get; set; }
	public bool Activo { get; set; }
	public ICollection<DiaInhabilSede> Sedes { get; set; } = [];
}
