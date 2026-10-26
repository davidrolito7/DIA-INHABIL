namespace ApiDiaInhabil.DTOs;

public class CrearDiaInhabilAreaDto
{
	public int IdDiaInhabilSede { get; set; }
	public int IdArea { get; set; }
	public int TipoAplicacion { get; set; }
	public bool Activo { get; set; }
	public DateTime FechaRegistro { get; set; }
}

public class DiaInhabilAreaDto : CrearDiaInhabilAreaDto
{
	public int Id { get; set; }
}
