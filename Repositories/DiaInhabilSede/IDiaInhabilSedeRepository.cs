
using ApiDiaInhabil.Models;
using DiaInhabilSedeEntity = ApiDiaInhabil.Models.DiaInhabilSede;

namespace ApiDiaInhabil.Repositories.DiaInhabilSede;

public interface IDiaInhabilSedeRepository
{
    Task<IReadOnlyCollection<DiaInhabilSedeEntity>> ListarAsync();
    Task<IReadOnlyDictionary<int, int>> ContarSedesPorDiaAsync(IReadOnlyCollection<int> idsDiaInhabil);
    Task<DiaInhabilSedeEntity?> ObtenerPorIdAsync(int id);
    Task<IReadOnlyCollection<DiaInhabilSedeEntity>> ObtenerPorDiaInhabilAsync(int idDiaInhabil);
    Task<DiaInhabilSedeEntity> CrearAsync(DiaInhabilSedeEntity diaInhabilSede);
    Task CrearVariosAsync(IEnumerable<DiaInhabilSedeEntity> diasInhabilesSedes);
    Task<DiaInhabilSedeEntity?> ObtenerParaActualizarAsync(int id);
    Task<DiaInhabilSedeEntity?> ObtenerParaActualizarAsync(int idDiaInhabil, int idSede);
    void EliminarAreas(IEnumerable<DiaInhabilArea> areas);
    Task GuardarCambiosAsync();

    Task<bool> ExisteDiaInhabilAsync(int idDiaInhabil);
    Task<bool> ExisteSedeAsync(int idSede);
    Task<bool> AreasPertenecenASedeAsync(int idSede, IReadOnlyCollection<int> idsArea);

    Task<bool> ExisteRelacionAsync(int idDiaInhabil, int idSede);
    Task<bool> ExisteAlgunaRelacionParaDiaAsync(int idDiaInhabil);
}
