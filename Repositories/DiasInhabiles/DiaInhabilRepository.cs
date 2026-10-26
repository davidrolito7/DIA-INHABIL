using ApiDiaInhabil.Data;
using ApiDiaInhabil.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiDiaInhabil.Repositories.DiasInhabiles;

public class DiaInhabilRepository(ApiDiaInhabilContext context) : IDiaInhabilRepository
{
	public async Task<IReadOnlyCollection<DiaInhabil>> ListarAsync() =>
		await context.DiasInhabiles.AsNoTracking().OrderBy(dia => dia.FechaInicio).ToListAsync();

	public Task<DiaInhabil?> ObtenerPorIdAsync(int id) =>
		context.DiasInhabiles.AsNoTracking().FirstOrDefaultAsync(dia => dia.Id == id);

	public Task<DiaInhabil?> ObtenerParaActualizarAsync(int id) =>
		context.DiasInhabiles.FirstOrDefaultAsync(dia => dia.Id == id);

	public Task GuardarCambiosAsync() => context.SaveChangesAsync();

	public async Task<DiaInhabil> CrearAsync(DiaInhabil diaInhabil)
	{
		await context.DiasInhabiles.AddAsync(diaInhabil);
		await context.SaveChangesAsync();
		return diaInhabil;
	}

	
}
