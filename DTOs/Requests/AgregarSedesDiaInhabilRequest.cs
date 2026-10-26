using System.ComponentModel.DataAnnotations;

namespace ApiDiaInhabil.DTOs.Requests;

public class AgregarSedesDiaInhabilRequest : IValidatableObject
{
    [MinLength(1, ErrorMessage = "Debe especificar al menos una sede.")]
    public List<SedeDiaInhabilRequest> Sedes { get; set; } = [];

    public List<int> IdAreasIncluidas { get; set; } = [];

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Sedes.GroupBy(sede => sede.IdSede).Any(grupo => grupo.Count() > 1))
            yield return new ValidationResult("No se puede enviar la misma sede más de una vez.", [nameof(Sedes)]);
    }
}
