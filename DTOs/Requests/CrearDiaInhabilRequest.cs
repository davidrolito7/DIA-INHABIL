using System.ComponentModel.DataAnnotations;

namespace ApiDiaInhabil.DTOs.Requests;

public class CrearDiaInhabilRequest : IValidatableObject
{
	[Required(ErrorMessage = "El nombre es obligatorio.")]
	[StringLength(150, ErrorMessage = "El nombre no puede exceder 150 caracteres.")]
	public string Nombre { get; init; } = string.Empty;

	[StringLength(500, ErrorMessage = "La descripción no puede exceder 500 caracteres.")]
	public string? Descripcion { get; init; }

	[Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
	public DateTime? FechaInicio { get; init; }

	[Required(ErrorMessage = "La fecha de fin es obligatoria.")]
	public DateTime? FechaFin { get; init; }

	//public bool Activo { get; init; } = true;

	public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
	{
		if (FechaInicio.HasValue && FechaFin.HasValue && FechaFin < FechaInicio)
		{
			yield return new ValidationResult("La fecha de fin no puede ser anterior a la fecha de inicio.", [nameof(FechaInicio), nameof(FechaFin)]);
		}
	}
}
