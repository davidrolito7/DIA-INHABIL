using ApiDiaInhabil.DTOs.Requests;
using ApiDiaInhabil.DTOs.Responses;

namespace ApiDiaInhabil.Services.DiasInhabiles;

public interface IDiaInhabilService
{
	Task<IReadOnlyCollection<DiaInhabilResponse>> ListarAsync();
	Task<DiaInhabilResponse?> ObtenerPorIdAsync(int id);
	Task<DiaInhabilResponse> CrearAsync(CrearDiaInhabilRequest request);
	Task<DiaInhabilResponse?> ActualizarAsync(int id, ActualizarDiaInhabilRequest request);
	
}
