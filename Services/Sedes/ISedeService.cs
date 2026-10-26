using ApiDiaInhabil.DTOs;
using ApiDiaInhabil.DTOs.Requests;
using ApiDiaInhabil.DTOs.Responses;
using ApiDiaInhabil.Models;

namespace ApiDiaInhabil.Services.Sedes;

public interface ISedeService
{
    Task<IReadOnlyCollection<SedeResponse>> ListarAsync();
    Task<SedeResponse?> ObtenerPorIdAsync(int id);
    Task<SedeResponse> CrearAsync(SedeRequest sede);
    Task<SedeResponse?> ActualizarAsync(int id, ActualizarSedeRequest request);
    Task<IReadOnlyCollection<AreaResponse>> ListarAreasAsync();
}
