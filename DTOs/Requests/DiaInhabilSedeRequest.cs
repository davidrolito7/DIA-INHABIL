using System.ComponentModel.DataAnnotations;

namespace ApiDiaInhabil.DTOs.Requests;

public class DiaInhabilSedeRequest : IValidatableObject
{
    [Range(1, int.MaxValue, ErrorMessage = "El día inhábil es requerido.")]
    public int IdDiaInhabil { get; set; }

    [MinLength(1, ErrorMessage = "Debe especificar al menos una sede.")]
    public List<SedeDiaInhabilRequest> Sedes { get; set; } = [];

    /// <summary>Áreas externas incluidas sólo para esta configuración de día inhábil.</summary>
    public List<int> IdAreasIncluidas { get; set; } = [];

    // Compatibilidad interna mientras se sustituye la implementación anterior.

    public IEnumerable<ValidationResult> Validate(
        ValidationContext validationContext)
    {
        if (Sedes.GroupBy(sede => sede.IdSede).Any(grupo => grupo.Count() > 1))
        {
            yield return new ValidationResult(
                "No se puede enviar la misma sede más de una vez.",
                [nameof(Sedes)]);
        }

        if (IdAreasIncluidas.GroupBy(idArea => idArea)
            .Any(grupo => grupo.Count() > 1))
        {
            yield return new ValidationResult(
                "No se puede incluir la misma área más de una vez.",
                [nameof(IdAreasIncluidas)]);
        }
    }
}

public class SedeDiaInhabilRequest : IValidatableObject
{
    [Range(1, int.MaxValue, ErrorMessage = "La sede es requerida.")]
    public int IdSede { get; set; }

    public bool EsGeneral { get; set; }

    /// <summary>Áreas propias de la sede que no aplican al día inhábil.</summary>
    public List<int> IdAreasExcluidas { get; set; } = [];


    public IEnumerable<ValidationResult> Validate(
        ValidationContext validationContext)
    {
        var idsArea = IdAreasExcluidas.ToArray();

        if (!EsGeneral && idsArea.Length == 0)
        {
            yield return new ValidationResult(
                "Debe especificar al menos un área cuando el día inhábil no es general.",
                [nameof(IdAreasExcluidas)]
            );
        }

        if (EsGeneral && idsArea.Length > 0)
        {
            yield return new ValidationResult(
                "No se deben especificar áreas cuando el día inhábil es general.",
                [nameof(IdAreasExcluidas)]
            );
        }

        if (idsArea
            .GroupBy(idArea => idArea)
            .Any(grupo => grupo.Count() > 1))
        {
            yield return new ValidationResult(
                "No se puede enviar la misma área más de una vez.",
                [nameof(IdAreasExcluidas)]
            );
        }
    }
}
