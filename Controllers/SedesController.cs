using ApiDiaInhabil.DTOs;
using ApiDiaInhabil.DTOs.Requests;
using ApiDiaInhabil.DTOs.Responses;
using ApiDiaInhabil.Services.Sedes;
using Microsoft.AspNetCore.Mvc;

namespace ApiDiaInhabil.Controllers;

[Route("api/sedes")]
[ApiController]
public class SedesController(ISedeService sedeService): ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(GenericResponse<IReadOnlyCollection<SedeResponse>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<SedeResponse>>> ListarSedes()
    {
    	var sedes = await sedeService.ListarAsync();

	    return Ok(new GenericResponse<IReadOnlyCollection<SedeResponse>>
	    {
		    Success = true,
		    Status = StatusCodes.Status200OK,
		    Message = "Sedes obtenidas correctamente",
		    Data = sedes
	    });
	    //return Ok(sedes);
    }
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(GenericResponse<SedeResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GenericResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SedeResponse>> ObtenerPorId(int id)
    {
	    var sede = await sedeService.ObtenerPorIdAsync(id);
	   // return sede is null ? NotFound() : Ok(sede);

	    if (sede is null)
	    {
		    return NotFound(new GenericResponse<object>
		    {
			    Success = false,
			    Status = StatusCodes.Status404NotFound,
				Message = "No se encontró la sede ",
				Error = $"No existe un registro con el id {id}.",

		    });
	    }

	    return Ok(new GenericResponse<SedeResponse>
	    {
		    Success = true,
		    Status = StatusCodes.Status200OK,
		    Message = "Sede obtenida correctamente",
		    Data = sede
	    });
    }

    [HttpPost]
    [ProducesResponseType(typeof(GenericResponse<SedeResponse>), StatusCodes.Status201Created)]
    public async Task<ActionResult<SedeResponse>> Crear( [FromBody] SedeRequest request)
    {
	    try
	    {
		    var sede = await sedeService.CrearAsync(request);
		    var response = new GenericResponse<SedeResponse>
		    {
			    Success = true,
			    Status = StatusCodes.Status201Created,
			    Message = "Sede creada correctamente",
			    Data = sede
		    };
		    return CreatedAtAction(
			    nameof(ObtenerPorId),
			    new { id = sede.Id },
			    response);
	    }
	    catch (KeyNotFoundException exception)
	    {
		    return NotFound(new GenericResponse<object>
		    {
			    Success = false,
			    Status = StatusCodes.Status404NotFound,
			    Message = "No se pudo crear la sede.",
			    Error = exception.Message
		    });
	    }

    }

    [HttpPatch("{id:int}")]
    [ProducesResponseType(typeof(GenericResponse<SedeResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GenericResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GenericResponse<SedeResponse>>> Actualizar(
        int id, [FromBody] ActualizarSedeRequest request)
    {
        try
        {
            var sede = await sedeService.ActualizarAsync(id, request);
            if (sede is null)
                return NotFound(RespuestaNoEncontrada(id));

            return Ok(new GenericResponse<SedeResponse>
            {
                Success = true,
                Status = StatusCodes.Status200OK,
                Message = "Sede actualizada correctamente.",
                Data = sede
            });
        }
        catch (KeyNotFoundException exception)
        {
            return NotFound(new GenericResponse<object>
            {
                Success = false,
                Status = StatusCodes.Status404NotFound,
                Message = "No se pudo actualizar la sede.",
                Error = exception.Message
            });
        }
    }

    private static GenericResponse<object> RespuestaNoEncontrada(int id) => new()
    {
        Success = false,
        Status = StatusCodes.Status404NotFound,
        Message = "No se encontró la sede.",
        Error = $"No existe un registro con el id {id}."
    };
    [HttpGet ("areas")]
    [ProducesResponseType(typeof(GenericResponse<IReadOnlyCollection<AreaResponse>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<AreaResponse>>> ListarAreas()
    {
	    var areas = await sedeService.ListarAreasAsync();

	    return Ok(new GenericResponse<IReadOnlyCollection<AreaResponse>>
	    {
		    Success = true,
		    Status = StatusCodes.Status200OK,
		    Message = "Areas obtenidas correctamente",
		    Data = areas
	    });
	    //return Ok(sedes);
    }
    
}
