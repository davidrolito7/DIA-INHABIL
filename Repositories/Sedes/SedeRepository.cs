using ApiDiaInhabil.Data;
using ApiDiaInhabil.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiDiaInhabil.Repositories.Sedes;

public class SedeRepository(ApiDiaInhabilContext context) : ISedeRepository
{
    public async Task<IReadOnlyCollection<Sede>> ListarAsync() =>
        await context.Sedes.AsNoTracking()
            .Include(sede => sede.Areas)
            .OrderBy(sede => sede.Descripcion)
            .ToListAsync();

    public async Task<IReadOnlyCollection<Area>> ListarAreasAsync() =>
        await context.Areas.AsNoTracking()
            .Include(area => area.Sede)
            .OrderBy(area => area.IdArea)
            .ToListAsync();
    public Task<Sede?> ObtenerPorIdAsync(int id)=>
        context.Sedes.AsNoTracking()
            .Include(sede => sede.Areas)
            .FirstOrDefaultAsync(sede => sede.Id == id);

    public Task<Sede?> ObtenerParaActualizarAsync(int id) =>
        context.Sedes
            .Include(sede => sede.Areas)
            .FirstOrDefaultAsync(sede => sede.Id == id);

    public async Task<IReadOnlyCollection<Area>> ObtenerAreasAsync(IReadOnlyCollection<int> idsArea) =>
        await context.Areas
            .Where(area => idsArea.Contains(area.IdArea))
            .ToListAsync();

    public Task GuardarCambiosAsync() => context.SaveChangesAsync();

    public async Task<Sede> CrearAsync(Sede sede, IReadOnlyCollection<int> idsArea)
    {
        var idsUnicos = idsArea.Distinct().ToArray();
        var areas = await context.Areas
            .Where(area => idsUnicos.Contains(area.IdArea))
            .ToListAsync();

        if (areas.Count != idsUnicos.Length)
            throw new KeyNotFoundException("Una o más áreas no existen.");

        sede.Areas = areas;
        await context.Sedes.AddAsync(sede);
        await context.SaveChangesAsync();
        return sede;

    }
}
