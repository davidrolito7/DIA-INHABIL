namespace ApiDiaInhabil.DTOs.Responses;

public class AreaResponse
{
    public int IdArea { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public bool Activo { get; set; }
    public int? IdSede { get; set; }
    public SedeResponse? Sede { get; set; }

}
