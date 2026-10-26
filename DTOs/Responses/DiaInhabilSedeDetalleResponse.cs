namespace ApiDiaInhabil.DTOs.Responses;

public class DiaInhabilSedeDetalleResponse
{
    public int NumeroSedes { get; set; }
    public DiaInhabilResponse DiaInhabil { get; set; } = null!;
    public List<SedeDiaInhabilResponse> Sedes { get; set; } = [];
}

public class SedeDiaInhabilResponse
{
    public int Id { get; set; }
    public bool EsGeneral { get; set; }
    public bool Activo { get; set; }
    public DateTime FechaRegistro { get; set; }
    public List<int> IdAreasIncluidas { get; set; } = [];
    public SedeResponse Sede { get; set; } = null!;
}
