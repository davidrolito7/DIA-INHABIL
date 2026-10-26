using System.ComponentModel.DataAnnotations;

namespace ApiDiaInhabil.DTOs.Requests;

public class ActualizarDiaInhabilRequest
{
    [StringLength(150, ErrorMessage = "El nombre no puede exceder 150 caracteres.")]
    public string? Nombre { get; init; }

    [StringLength(500, ErrorMessage = "La descripción no puede exceder 500 caracteres.")]
    public string? Descripcion { get; init; }

    public DateTime? FechaInicio { get; init; }
    public DateTime? FechaFin { get; init; }
    public bool? Activo { get; init; }
}
