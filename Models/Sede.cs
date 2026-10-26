namespace ApiDiaInhabil.Models;

public class Sede
{
	public int Id { get; set; }
	public string Nombre { get; set; } = string.Empty;
	public string? Descripcion { get; set; }
	public bool Activo { get; set; }
	public ICollection<DiaInhabilSede> DiasInhabiles { get; set; } = [];
	public ICollection<Area> Areas { get; set; } = [];
}

