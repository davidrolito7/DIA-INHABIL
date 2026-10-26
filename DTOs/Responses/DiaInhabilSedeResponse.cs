namespace ApiDiaInhabil.DTOs.Responses;

public class DiaInhabilSedeResponse
{
    public int Id { get; set; }
    // public int IdDiaInhabil { get; set; }
    // public int IdSede { get; set; }
    public bool EsGeneral { get; set; }
    public bool Activo { get; set; }
    public DateTime FechaRegistro { get; set; }
    public int NumeroSedes { get; set; }

    public DiaInhabilResponse DiaInhabil { get; set; } = null!;
    public SedeResponse Sede { get; set; } = null!;
}
