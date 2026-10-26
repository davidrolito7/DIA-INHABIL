using ApiDiaInhabil.Data;
using ApiDiaInhabil.Models;
using Microsoft.EntityFrameworkCore;
using DiaInhabilSedeEntity = ApiDiaInhabil.Models.DiaInhabilSede;

namespace ApiDiaInhabil.Repositories.DiaInhabilSede;

public class DiaInhabilSedeRepository(ApiDiaInhabilContext context) : IDiaInhabilSedeRepository
{
    public async Task<IReadOnlyCollection<DiaInhabilSedeEntity>> ListarAsync() =>
        await context.DiasInhabilesSedes
            .AsNoTracking()
            .Include(relacion => relacion.DiaInhabil)
            .Include(relacion => relacion.Sede)
            .ThenInclude(sede => sede.Areas)
            .Include(relacion => relacion.DiaInhabilAreas)
            .ThenInclude(relacionArea => relacionArea.Area)
            .OrderBy(relacion => relacion.DiaInhabil!.FechaInicio)
            .ThenBy(relacion => relacion.Sede!.Nombre)
            .ToListAsync();

    public Task<DiaInhabilSedeEntity?> ObtenerPorIdAsync(int id) =>
        context.DiasInhabilesSedes
            .AsNoTracking()
            .Include(relacion => relacion.DiaInhabil)
            .Include(relacion => relacion.Sede)
            .ThenInclude(sede => sede.Areas)
            .Include(relacion => relacion.DiaInhabilAreas)
            .ThenInclude(relacionArea => relacionArea.Area)
            .FirstOrDefaultAsync(relacion => relacion.Id == id);

    public async Task<IReadOnlyCollection<DiaInhabilSedeEntity>> ObtenerPorDiaInhabilAsync(
        int idDiaInhabil) =>
        await context.DiasInhabilesSedes
            .AsNoTracking()
            .Include(relacion => relacion.DiaInhabil)
            .Include(relacion => relacion.Sede)
            .ThenInclude(sede => sede.Areas)
            .Include(relacion => relacion.DiaInhabilAreas)
            .ThenInclude(relacionArea => relacionArea.Area)
            .Where(relacion => relacion.IdDiaInhabil == idDiaInhabil)
            .OrderBy(relacion => relacion.Sede!.Nombre)
            .ToListAsync();

    public async Task<IReadOnlyDictionary<int, int>> ContarSedesPorDiaAsync(
        IReadOnlyCollection<int> idsDiaInhabil) =>
        await context.DiasInhabilesSedes.AsNoTracking()
            .Where(relacion => idsDiaInhabil.Contains(relacion.IdDiaInhabil))
            .GroupBy(relacion => relacion.IdDiaInhabil)
            .ToDictionaryAsync(grupo => grupo.Key, grupo => grupo.Count());

    public Task<DiaInhabilSedeEntity?> ObtenerParaActualizarAsync(int id) =>
        context.DiasInhabilesSedes
            .Include(relacion => relacion.DiaInhabilAreas)
            .FirstOrDefaultAsync(relacion => relacion.Id == id);

    public Task<DiaInhabilSedeEntity?> ObtenerParaActualizarAsync(
        int idDiaInhabil, int idSede) =>
        context.DiasInhabilesSedes
            .Include(relacion => relacion.DiaInhabilAreas)
            .FirstOrDefaultAsync(relacion => relacion.IdDiaInhabil == idDiaInhabil &&
                relacion.IdSede == idSede);

    public void EliminarAreas(IEnumerable<DiaInhabilArea> areas) =>
        context.DiasInhabilesAreas.RemoveRange(areas);

    public Task GuardarCambiosAsync() => context.SaveChangesAsync();

    public Task<bool> ExisteDiaInhabilAsync(int idDiaInhabil) =>
        context.DiasInhabiles.AnyAsync(dia => dia.Id == idDiaInhabil);

    public Task<bool> ExisteSedeAsync(int idSede) => context.Sedes.AnyAsync(x => x.Id ==  idSede);
    

    public async Task<bool> AreasPertenecenASedeAsync(int idSede, IReadOnlyCollection<int> idsArea)
    {
        var idsUnicos = idsArea.Distinct().ToArray();
        return idsUnicos.Length == 0 || await context.Areas.AsNoTracking()
            .CountAsync(area => idsUnicos.Contains(area.IdArea)) == idsUnicos.Length;
    }

    public Task<bool> ExisteRelacionAsync(int idDiaInhabil, int idSede) =>
    context.DiasInhabilesSedes.AnyAsync(relacion =>
            relacion.IdDiaInhabil == idDiaInhabil && relacion.IdSede == idSede);

    public Task<bool> ExisteAlgunaRelacionParaDiaAsync(int idDiaInhabil) =>
        context.DiasInhabilesSedes.AnyAsync(relacion =>
            relacion.IdDiaInhabil == idDiaInhabil);
    

    public async Task<DiaInhabilSedeEntity> CrearAsync(DiaInhabilSedeEntity diaInhabilSede)
    {
        await context.DiasInhabilesSedes.AddAsync(diaInhabilSede);
        await context.SaveChangesAsync();
        return diaInhabilSede;
    }

    public async Task CrearVariosAsync(IEnumerable<DiaInhabilSedeEntity> diasInhabilesSedes)
    {
        await context.DiasInhabilesSedes.AddRangeAsync(diasInhabilesSedes);
        await context.SaveChangesAsync();
    }
}
