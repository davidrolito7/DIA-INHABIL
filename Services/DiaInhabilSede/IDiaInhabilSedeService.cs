using ApiDiaInhabil.DTOs.Requests;
using ApiDiaInhabil.DTOs.Responses;

namespace ApiDiaInhabil.Services.DiaInhabilSede;

public interface IDiaInhabilSedeService
{
    Task<IReadOnlyCollection<DiaInhabilSedeListadoResponse>> ListarAsync();
    Task<DiaInhabilSedeDetalleResponse?> ObtenerPorIdAsync(int idDiaInhabil);
    Task<IReadOnlyCollection<DiaInhabilSedeResponse>> CrearAsync(DiaInhabilSedeRequest request);
    Task<IReadOnlyCollection<DiaInhabilSedeResponse>> AgregarSedesAsync(
        int idDiaInhabil, AgregarSedesDiaInhabilRequest request);
    Task<DiaInhabilSedeResponse?> ActualizarAsync(int id, ActualizarDiaInhabilSedeRequest request);
}
