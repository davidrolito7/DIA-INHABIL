using ApiDiaInhabil.DTOs.Requests;
using ApiDiaInhabil.DTOs.Responses;
using ApiDiaInhabil.Services;
using ApiDiaInhabil.Services.DiasInhabiles;
using Microsoft.AspNetCore.Mvc;

namespace ApiDiaInhabil.Controllers;

[Route("api/dias-inhabiles")]
[ApiController]
public class DiasInhabilesController(IDiaInhabilService diaInhabilService) : ControllerBase
{
	[HttpGet]
	[ProducesResponseType(typeof(GenericResponse<IReadOnlyCollection<DiaInhabilResponse>>), StatusCodes.Status200OK)]
	public async Task<ActionResult<GenericResponse<IReadOnlyCollection<DiaInhabilResponse>>>> Listar()
	{
		var dias = await diaInhabilService.ListarAsync();
		return Ok(new GenericResponse<IReadOnlyCollection<DiaInhabilResponse>>
		{
			Success = true,
			Status = StatusCodes.Status200OK,
			Message = "Días inhábiles obtenidos correctamente",
			Data = dias
		});
	}

	[HttpGet("{id:int}")]
	[ProducesResponseType(typeof(GenericResponse<DiaInhabilResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(GenericResponse<object>), StatusCodes.Status404NotFound)]
	public async Task<ActionResult<DiaInhabilResponse>> ObtenerPorId(int id)
	{
		var dia = await diaInhabilService.ObtenerPorIdAsync(id);

		if (dia is null)
		{
			return NotFound(new GenericResponse<object>
			{
				Success = false,
				Status = StatusCodes.Status404NotFound,
				Message = "No se encontró el día inhábil",
				Error = $"No existe un registro con el id {id}."
			});
		}
		return Ok(new GenericResponse<DiaInhabilResponse>
		{
			Success = true,
			Status = StatusCodes.Status200OK,
			Message = "Día inhabil obtenido correctamente",
			Data = dia
		});
			
	}

	[HttpPost]
	[ProducesResponseType(typeof(GenericResponse<DiaInhabilResponse>), StatusCodes.Status201Created)]
	public async Task<ActionResult<DiaInhabilResponse>> Crear( [FromBody] CrearDiaInhabilRequest request)
	{
		var dia = await diaInhabilService.CrearAsync(request);

		var response = new GenericResponse<DiaInhabilResponse>
		{
			Success = true,
			Status = StatusCodes.Status201Created,
			Message = "Día inhabil creado correctamente",
			Data = dia
		};
		return CreatedAtAction(
			nameof(ObtenerPorId),
			new { id = dia.Id },
			response);
		
		//return CreatedAtAction(nameof(ObtenerPorId), new { id = dia.Id }, dia);
	}

	[HttpPatch("{id:int}")]
	[ProducesResponseType(typeof(GenericResponse<DiaInhabilResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(GenericResponse<object>), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(GenericResponse<object>), StatusCodes.Status404NotFound)]
	public async Task<ActionResult<GenericResponse<DiaInhabilResponse>>> Actualizar(
		int id, [FromBody] ActualizarDiaInhabilRequest request)
	{
		try
		{
			var dia = await diaInhabilService.ActualizarAsync(id, request);
			if (dia is null)
				return NotFound(new GenericResponse<object>
				{
					Success = false,
					Status = StatusCodes.Status404NotFound,
					Message = "No se encontró el día inhábil.",
					Error = $"No existe un registro con el id {id}."
				});

			return Ok(new GenericResponse<DiaInhabilResponse>
			{
				Success = true,
				Status = StatusCodes.Status200OK,
				Message = "Día inhábil actualizado correctamente.",
				Data = dia
			});
		}
		catch (ArgumentException exception)
		{
			return BadRequest(new GenericResponse<object>
			{
				Success = false,
				Status = StatusCodes.Status400BadRequest,
				Message = "No se pudo actualizar el día inhábil.",
				Error = exception.Message
			});
		}
	}


	
}
