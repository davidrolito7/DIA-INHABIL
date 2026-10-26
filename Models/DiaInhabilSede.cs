namespace ApiDiaInhabil.Models;

public class DiaInhabilSede
{
    public int Id { get; set; }
    public int IdDiaInhabil { get; set; }
    public int IdSede { get; set; }
    public bool EsGeneral { get; set; }
    public bool Activo { get; set; }
    public DateTime FechaRegistro { get; set; }

    public DiaInhabil? DiaInhabil { get; set; }
    public Sede? Sede { get; set; }
    public ICollection<DiaInhabilArea> DiaInhabilAreas { get; set; } = [];
}