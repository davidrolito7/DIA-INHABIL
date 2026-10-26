namespace ApiDiaInhabil.Models;

public class DiaInhabilArea
{
	public int Id { get; set; }
	public int IdDiaInhabilSede { get; set; }
	public int IdArea { get; set; }
	public int TipoAplicacion { get; set; }
	public bool Activo { get; set; }
	public DateTime FechaRegistro { get; set; }
	public DiaInhabilSede? DiaInhabilSede { get; set; }
	public Area? Area { get; set; }
}
