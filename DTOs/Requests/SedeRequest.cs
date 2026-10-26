using System.ComponentModel.DataAnnotations;

namespace ApiDiaInhabil.DTOs.Requests;

public class SedeRequest
{
    [Required(ErrorMessage = "El campo es obligatorio.")]
    [StringLength(150, ErrorMessage = "El nombre no puede exceder los 150 caracteres.")]
    public string Nombre { get; init; } = string.Empty;
    
    [Required(ErrorMessage = "El campo es obligatorio.")]
	[StringLength(150, ErrorMessage = "La descripcion no puede exceder los 150 caracteres.")]
	public string? Descripcion { get; init; }

	public List<int> IdAreas { get; init; } = [];
}
