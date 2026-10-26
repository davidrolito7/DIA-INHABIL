using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiDiaInhabil.Models;

[Table("ADM_AREA")]
public class Area
{
	[Key]
	public int IdArea { get; set; }
	public string Nombre { get; set; } = string.Empty;
	public bool Activo { get; set; }
	public int? IdSede { get; set; }
	public Sede? Sede { get; set; }

}
