using ApiDiaInhabil.DTOs.Requests;
using ApiDiaInhabil.DTOs.Responses;
using ApiDiaInhabil.Services.DiaInhabilSede;
using Microsoft.AspNetCore.Mvc;

namespace ApiDiaInhabil.Controllers;

[Route("api/DiaInhabilSede")]
[ApiController]
public class DiaInhabilSedeController(IDiaInhabilSedeService diaInhabilSedeService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(GenericResponse<IReadOnlyCollection<DiaInhabilSedeListadoResponse>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<GenericResponse<IReadOnlyCollection<DiaInhabilSedeListadoResponse>>>> Listar()
    {
        var relaciones = await diaInhabilSedeService.ListarAsync();

        return Ok(new GenericResponse<IReadOnlyCollection<DiaInhabilSedeListadoResponse>>
        {
            Success = true,
            Status = StatusCodes.Status200OK,
            Message = "Días inhábiles por sede obtenidos correctamente.",
            Data = relaciones
        });
    }

    [HttpGet("{idDiaInhabil:int}")]
    [ProducesResponseType(typeof(GenericResponse<DiaInhabilSedeDetalleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GenericResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GenericResponse<DiaInhabilSedeDetalleResponse>>> ObtenerPorId(int idDiaInhabil)
    {
        var dia = await diaInhabilSedeService.ObtenerPorIdAsync(idDiaInhabil);

        if (dia is null)
        {
            return NotFound(new GenericResponse<object>
            {
                Success = false,
                Status = StatusCodes.Status404NotFound,
                Message = "No se encontró el día inhábil por sede.",
                Error = $"No existe un registro con el id {idDiaInhabil}."
            });
        }

        return Ok(new GenericResponse<DiaInhabilSedeDetalleResponse>
        {
            Success = true,
            Status = StatusCodes.Status200OK,
            Message = "Día inhábil por sede obtenido correctamente.",
            Data = dia
        });
    }

    [HttpPost]
    [ProducesResponseType(typeof(GenericResponse<IReadOnlyCollection<DiaInhabilSedeResponse>>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(GenericResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GenericResponse<object>), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<GenericResponse<IReadOnlyCollection<DiaInhabilSedeResponse>>>> Crear(
        [FromBody] DiaInhabilSedeRequest request)
    {
        try
        {
            var diaInhabilSede = await diaInhabilSedeService.CrearAsync(request);
            var response = new GenericResponse<IReadOnlyCollection<DiaInhabilSedeResponse>>
            {
                Success = true,
                Status = StatusCodes.Status201Created,
                Message = "Día inhábil por sede creado correctamente.",
                Data = diaInhabilSede
            };

            return StatusCode(StatusCodes.Status201Created, response);
        }
        catch (KeyNotFoundException exception)
        {
            return NotFound(new GenericResponse<object>
            {
                Success = false,
                Status = StatusCodes.Status404NotFound,
                Message = "No se pudo crear la relación día inhábil-sede.",
                Error = exception.Message
            });
        }
        catch (InvalidOperationException exception)
        {
            return Conflict(new GenericResponse<object>
            {
                Success = false,
                Status = StatusCodes.Status409Conflict,
                Message = "La relación día inhábil-sede ya existe.",
                Error = exception.Message
            });
        }
    }

    [NonAction]
    [ProducesResponseType(typeof(GenericResponse<DiaInhabilSedeResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GenericResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GenericResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GenericResponse<object>), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<GenericResponse<DiaInhabilSedeResponse>>> Actualizar(
        int id, [FromBody] ActualizarDiaInhabilSedeRequest request)
    {
        try
        {
            var relacion = await diaInhabilSedeService.ActualizarAsync(id, request);
            if (relacion is null)
            {
                return NotFound(new GenericResponse<object>
                {
                    Success = false,
                    Status = StatusCodes.Status404NotFound,
                    Message = "No se encontró el día inhábil por sede.",
                    Error = $"No existe un registro con el id {id}."
                });
            }

            return Ok(new GenericResponse<DiaInhabilSedeResponse>
            {
                Success = true,
                Status = StatusCodes.Status200OK,
                Message = "Día inhábil por sede actualizado correctamente.",
                Data = relacion
            });
        }
        catch (KeyNotFoundException exception)
        {
            return NotFound(new GenericResponse<object>
            {
                Success = false,
                Status = StatusCodes.Status404NotFound,
                Message = "No se pudo actualizar la relación día inhábil-sede.",
                Error = exception.Message
            });
        }
        catch (ArgumentException exception)
        {
            return BadRequest(new GenericResponse<object>
            {
                Success = false,
                Status = StatusCodes.Status400BadRequest,
                Message = "No se pudo actualizar la relación día inhábil-sede.",
                Error = exception.Message
            });
        }
        catch (InvalidOperationException exception)
        {
            return Conflict(new GenericResponse<object>
            {
                Success = false,
                Status = StatusCodes.Status409Conflict,
                Message = "La relación día inhábil-sede ya existe.",
                Error = exception.Message
            });
        }
    }

    [HttpPatch("{idDiaInhabil:int}")]
    [ProducesResponseType(typeof(GenericResponse<IReadOnlyCollection<DiaInhabilSedeResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GenericResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GenericResponse<object>), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<GenericResponse<IReadOnlyCollection<DiaInhabilSedeResponse>>>> AgregarSedes(
        int idDiaInhabil, [FromBody] AgregarSedesDiaInhabilRequest request)
    {
        try
        {
            var relaciones = await diaInhabilSedeService.AgregarSedesAsync(idDiaInhabil, request);
            return Ok(new GenericResponse<IReadOnlyCollection<DiaInhabilSedeResponse>>
            {
                Success = true,
                Status = StatusCodes.Status200OK,
                Message = "Sedes agregadas correctamente al día inhábil.",
                Data = relaciones
            });
        }
        catch (KeyNotFoundException exception)
        {
            return NotFound(new GenericResponse<object>
            {
                Success = false,
                Status = StatusCodes.Status404NotFound,
                Message = "No se pudieron agregar las sedes.",
                Error = exception.Message
            });
        }
        catch (InvalidOperationException exception)
        {
            return Conflict(new GenericResponse<object>
            {
                Success = false,
                Status = StatusCodes.Status409Conflict,
                Message = "No se pudieron agregar las sedes.",
                Error = exception.Message
            });
        }
    }
}
