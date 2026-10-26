using System.ComponentModel.DataAnnotations;

namespace ApiDiaInhabil.DTOs.Requests;

public class ActualizarDiaInhabilSedeRequest
{
    [Range(1, int.MaxValue, ErrorMessage = "El día inhábil es requerido.")]
    public int? IdDiaInhabil { get; init; }

    [Range(1, int.MaxValue, ErrorMessage = "La sede es requerida.")]
    public int? IdSede { get; init; }

    public bool? EsGeneral { get; init; }
    public bool? Activo { get; init; }

    /// <summary>Al enviarse, reemplaza las Áreas de la relación.</summary>
    public List<DiaInhabilAreaRequest>? DiaInhabilAreas { get; init; }
}
