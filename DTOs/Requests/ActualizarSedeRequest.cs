using System.ComponentModel.DataAnnotations;

namespace ApiDiaInhabil.DTOs.Requests;

public class ActualizarSedeRequest
{
    [StringLength(150, ErrorMessage = "El nombre no puede exceder los 150 caracteres.")]
    public string? Nombre { get; init; }

    [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres.")]
    public string? Descripcion { get; init; }

    public bool? Activo { get; init; }

    /// <summary>Al enviarse, reemplaza las Áreas asignadas a la sede.</summary>
    public List<int>? IdAreas { get; init; }
}
