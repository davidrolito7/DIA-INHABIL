namespace ApiDiaInhabil.DTOs;

public class CrearDiaInhabilSedeDto
{
	public int IdDiaInhabil { get; set; }
	public int IdSede { get; set; }
	public bool Activo { get; set; }
	public DateTime FechaRegistro { get; set; }
}

public class DiaInhabilSedeDto : CrearDiaInhabilSedeDto
{
	public int Id { get; set; }
}
