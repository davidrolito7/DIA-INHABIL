using ApiDiaInhabil.DTOs;
using ApiDiaInhabil.DTOs.Requests;
using ApiDiaInhabil.DTOs.Responses;
using ApiDiaInhabil.Models;
using ApiDiaInhabil.Repositories;
using ApiDiaInhabil.Repositories.DiasInhabiles;
using ApiDiaInhabil.Infrastructure;

namespace ApiDiaInhabil.Services.DiasInhabiles;

public class DiaInhabilService(IDiaInhabilRepository diaInhabilRepository) : IDiaInhabilService
{
	public async Task<IReadOnlyCollection<DiaInhabilResponse>> ListarAsync()
	{
		var dias = await diaInhabilRepository.ListarAsync();
		return dias.Select(MapearDiaInhabil).ToArray();
	}

	public async Task<DiaInhabilResponse?> ObtenerPorIdAsync(int id)
	{
		var dia = await diaInhabilRepository.ObtenerPorIdAsync(id);
		return dia is null ? null : MapearDiaInhabil(dia);
	}

	public async Task<DiaInhabilResponse> CrearAsync(CrearDiaInhabilRequest request)
	{
		var dia = new DiaInhabil
		{
			Nombre = request.Nombre.Trim(),
			Descripcion = string.IsNullOrWhiteSpace(request.Descripcion) ? null : request.Descripcion.Trim(),
			FechaInicio = MexicoCityDateTime.Convertir(request.FechaInicio!.Value),
			FechaFin = MexicoCityDateTime.Convertir(request.FechaFin!.Value),
			//Activo = request.Activo
		};

		var creado = await diaInhabilRepository.CrearAsync(dia);
		return MapearDiaInhabil(creado);
	}

	public async Task<DiaInhabilResponse?> ActualizarAsync(int id, ActualizarDiaInhabilRequest request)
	{
		var dia = await diaInhabilRepository.ObtenerParaActualizarAsync(id);
		if (dia is null)
			return null;

		var fechaInicio = MexicoCityDateTime.Convertir(request.FechaInicio ?? dia.FechaInicio);
		var fechaFin = MexicoCityDateTime.Convertir(request.FechaFin ?? dia.FechaFin);
		if (fechaFin < fechaInicio)
			throw new ArgumentException("La fecha de fin no puede ser anterior a la fecha de inicio.");

		if (request.Nombre is not null)
			dia.Nombre = request.Nombre.Trim();
		if (request.Descripcion is not null)
			dia.Descripcion = string.IsNullOrWhiteSpace(request.Descripcion) ? null : request.Descripcion.Trim();
		dia.FechaInicio = fechaInicio;
		dia.FechaFin = fechaFin;
		if (request.Activo.HasValue)
			dia.Activo = request.Activo.Value;

		await diaInhabilRepository.GuardarCambiosAsync();
		return MapearDiaInhabil(dia);
	}

	private static DiaInhabilResponse MapearDiaInhabil(DiaInhabil dia) => new()
	{
		Id = dia.Id,
		Nombre = dia.Nombre,
		Descripcion = dia.Descripcion,
		FechaInicio = dia.FechaInicio,
		FechaFin = dia.FechaFin,
		Activo = dia.Activo
	};
	
}
