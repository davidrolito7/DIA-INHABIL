using System.ComponentModel.DataAnnotations;

namespace ApiDiaInhabil.DTOs.Requests;

public class DiaInhabilAreaRequest
{
    [Range(1, int.MaxValue, ErrorMessage = "El área es requerida.")]
    public int IdArea { get; set; }

    /// <summary>
    /// 1 = Incluye el área
    /// 0 = Excluye el área
    /// </summary>
    [Range(0, 1, ErrorMessage = "TipoAplicacion debe ser 0 o 1.")]
    public int TipoAplicacion { get; set; }
}