namespace ApiDiaInhabil.DTOs.Responses;

public class DiaInhabilSedeListadoResponse
{
    public int NumeroSedes { get; set; }
    public DiaInhabilResponse DiaInhabil { get; set; } = null!;
}
