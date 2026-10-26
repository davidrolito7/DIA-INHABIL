using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.AccessControl;
using ApiDiaInhabil.DTOs;
using ApiDiaInhabil.DTOs.Requests;
using ApiDiaInhabil.DTOs.Responses;
using ApiDiaInhabil.Models;
using ApiDiaInhabil.Repositories.Sedes;

namespace ApiDiaInhabil.Services.Sedes;

public class SedeService(ISedeRepository sedeRepository) : ISedeService
{
    public async Task<IReadOnlyCollection<SedeResponse>> ListarAsync()
    {
        var sedes = await sedeRepository.ListarAsync();
        return sedes.Select(MapearSede).ToArray();
    }

    public async Task<IReadOnlyCollection<AreaResponse>> ListarAreasAsync()
    {
        var areas = await sedeRepository.ListarAreasAsync();
        return areas.Select(MapearArea).ToArray();
    }

    public async Task<SedeResponse?> ObtenerPorIdAsync(int id)
    {
        var sede = await sedeRepository.ObtenerPorIdAsync(id);
        return sede is null ? null : MapearSede(sede);
		
    }
    private static SedeResponse MapearSede(Sede sede) => new()
    {
        Id = sede.Id,
        Nombre = sede.Nombre,
        Descripcion = sede.Descripcion,
        Activo = sede.Activo,
        NumeroAreas = sede.Areas.Count,
        Areas = sede.Areas.Select(area => new AreaResponse
        {
            IdArea = area.IdArea,
            Nombre = area.Nombre,
            Activo = area.Activo,
            IdSede = area.IdSede
        }).ToList()
    };

    private static AreaResponse MapearArea(Area area) => new()
    {
        IdArea = area.IdArea,
        Nombre = area.Nombre,
        Activo = area.Activo,
        IdSede = area.IdSede,
        Sede = area.Sede is null
            ? null
            : new SedeResponse
            {
                Id = area.Sede.Id,
                Nombre = area.Sede.Nombre,
                Descripcion = area.Sede.Descripcion
            }
    };

    public async Task<SedeResponse> CrearAsync(SedeRequest request)
    {
        var sede = new Sede
        {
            Nombre = request.Nombre,
            Descripcion = request.Descripcion
        };
        var creado = await sedeRepository.CrearAsync(sede, request.IdAreas);
        return MapearSede(creado);
    }

    public async Task<SedeResponse?> ActualizarAsync(int id, ActualizarSedeRequest request)
    {
        var sede = await sedeRepository.ObtenerParaActualizarAsync(id);
        if (sede is null)
            return null;

        if (request.Nombre is not null)
            sede.Nombre = request.Nombre.Trim();
        if (request.Descripcion is not null)
            sede.Descripcion = request.Descripcion.Trim();
        if (request.Activo.HasValue)
            sede.Activo = request.Activo.Value;

        if (request.IdAreas is not null)
        {
            var idsArea = request.IdAreas.Distinct().ToArray();
            var areas = await sedeRepository.ObtenerAreasAsync(idsArea);
            if (areas.Count != idsArea.Length)
                throw new KeyNotFoundException("Una o más áreas no existen.");

            foreach (var area in sede.Areas.Where(area => !idsArea.Contains(area.IdArea)))
                area.IdSede = null;
            foreach (var area in areas)
                area.IdSede = sede.Id;
        }

        await sedeRepository.GuardarCambiosAsync();
        var actualizada = await sedeRepository.ObtenerPorIdAsync(id);
        return actualizada is null ? null : MapearSede(actualizada);
    }
}
