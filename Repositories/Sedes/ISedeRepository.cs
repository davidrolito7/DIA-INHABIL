using ApiDiaInhabil.Models;

namespace ApiDiaInhabil.Repositories.Sedes;

public interface ISedeRepository
{
    Task<IReadOnlyCollection<Sede>> ListarAsync();
    Task <Sede?> ObtenerPorIdAsync(int id);
    Task<Sede> CrearAsync(Sede sede, IReadOnlyCollection<int> idsArea);
    Task<Sede?> ObtenerParaActualizarAsync(int id);
    Task<IReadOnlyCollection<Area>> ObtenerAreasAsync(IReadOnlyCollection<int> idsArea);
    Task GuardarCambiosAsync();
    Task<IReadOnlyCollection<Area>> ListarAreasAsync();
}
