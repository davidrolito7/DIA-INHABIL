using ApiDiaInhabil.Models;

namespace ApiDiaInhabil.Repositories.DiasInhabiles;

public interface IDiaInhabilRepository
{
	Task<IReadOnlyCollection<DiaInhabil>> ListarAsync();
	Task<DiaInhabil?> ObtenerPorIdAsync(int id);
	Task<DiaInhabil> CrearAsync(DiaInhabil diaInhabil);
	Task<DiaInhabil?> ObtenerParaActualizarAsync(int id);
	Task GuardarCambiosAsync();

}
