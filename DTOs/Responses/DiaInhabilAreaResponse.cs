namespace ApiDiaInhabil.DTOs.Responses;

public class DiaInhabilAreaResponse
{
    public int Id { get; set; }
    public int IdArea { get; set; }
    public string? NombreArea { get; set; }
    public int TipoAplicacion { get; set; }
    public bool Activo { get; set; }
}