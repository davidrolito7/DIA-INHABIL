using ApiDiaInhabil.DTOs;
using ApiDiaInhabil.DTOs.Requests;
using ApiDiaInhabil.DTOs.Responses;
using ApiDiaInhabil.Models;
using ApiDiaInhabil.Repositories.DiaInhabilSede;
using ApiDiaInhabil.Repositories.DiasInhabiles;
using ApiDiaInhabil.Repositories.Sedes;
using ApiDiaInhabil.Infrastructure;
using DiaInhabilSedeEntity = ApiDiaInhabil.Models.DiaInhabilSede;
using ApiDiaInhabil.Models;
namespace ApiDiaInhabil.Services.DiaInhabilSede;

public class DiaInhabilSedeService (
    IDiaInhabilSedeRepository diaInhabilSedeRepository,
    IDiaInhabilRepository diaInhabilRepository,
    ISedeRepository sedeRepository) : IDiaInhabilSedeService
{
    public async Task<IReadOnlyCollection<DiaInhabilSedeListadoResponse>> ListarAsync()
    {
        var relaciones = await diaInhabilSedeRepository.ListarAsync();
        return relaciones.GroupBy(relacion => relacion.IdDiaInhabil)
            .Select(grupo => new DiaInhabilSedeListadoResponse
            {
                NumeroSedes = grupo.Count(),
                DiaInhabil = MapearDiaInhabil(grupo.First().DiaInhabil!)
            })
            .ToArray();
    }
    
    public async Task<DiaInhabilSedeDetalleResponse?> ObtenerPorIdAsync(int idDiaInhabil)
    {
        var relaciones = await diaInhabilSedeRepository.ObtenerPorDiaInhabilAsync(idDiaInhabil);
        if (relaciones.Count == 0)
            return null;

        var primeraRelacion = relaciones.First();
        return new DiaInhabilSedeDetalleResponse
        {
            NumeroSedes = relaciones.Count,
            DiaInhabil = MapearDiaInhabil(primeraRelacion.DiaInhabil!),
            Sedes = relaciones.Select(MapearSedeDiaInhabil).ToList()
        };
    }


    public async Task<IReadOnlyCollection<DiaInhabilSedeResponse>> CrearAsync(
        DiaInhabilSedeRequest request)
    {
        if (await diaInhabilRepository.ObtenerPorIdAsync(request.IdDiaInhabil) is null)
            throw new KeyNotFoundException("No existe el dia inhabil.");
        if (await diaInhabilSedeRepository.ExisteAlgunaRelacionParaDiaAsync(
                request.IdDiaInhabil))
        {
            throw new InvalidOperationException(
                "El dia inhabil ya tiene sedes relacionadas; use PATCH para agregar mas sedes.");
        }

        var entidades = new List<DiaInhabilSedeEntity>();
        var relacionesActualizadas = new List<DiaInhabilSedeEntity>();
        var fechaRegistro = MexicoCityDateTime.Ahora();

        foreach (var sedeRequest in request.Sedes)
        {
            if (await sedeRepository.ObtenerPorIdAsync(sedeRequest.IdSede) is null)
                throw new KeyNotFoundException("No existe la sede.");
            if (await diaInhabilSedeRepository.ExisteRelacionAsync(
                    request.IdDiaInhabil, sedeRequest.IdSede))
                throw new InvalidOperationException("La relacion ya existe.");

            var entidad = new DiaInhabilSedeEntity
            {
                IdDiaInhabil = request.IdDiaInhabil,
                IdSede = sedeRequest.IdSede,
                EsGeneral = sedeRequest.EsGeneral,
                Activo = true,
                FechaRegistro = fechaRegistro
            };

            if (!sedeRequest.EsGeneral || request.IdAreasIncluidas.Count > 0)
            {
                var areasAplicables = await ObtenerExcepcionesValidasAsync(
                    sedeRequest.IdSede, ObtenerSolicitudesAreas(sedeRequest,
                        request.IdAreasIncluidas));
                foreach (var areaRequest in areasAplicables)
                {
                    entidad.DiaInhabilAreas.Add(new Models.DiaInhabilArea
                    {
                        IdArea = areaRequest.IdArea,
                        TipoAplicacion = areaRequest.TipoAplicacion,
                        Activo = true,
                        FechaRegistro = fechaRegistro
                    });
                }
            }

            entidades.Add(entidad);
        }

        await diaInhabilSedeRepository.CrearVariosAsync(entidades);
        var numeroSedesPorDia = await diaInhabilSedeRepository.ContarSedesPorDiaAsync(
            [request.IdDiaInhabil]);
        var resultado = new List<DiaInhabilSedeResponse>();
        foreach (var entidad in entidades)
        {
            var creada = await diaInhabilSedeRepository.ObtenerPorIdAsync(entidad.Id)
                ?? throw new InvalidOperationException("No fue posible recuperar la relacion creada.");
            resultado.Add(MapearDiaInhabilSede(creada,
                numeroSedesPorDia.GetValueOrDefault(creada.IdDiaInhabil)));
        }

        return resultado;

    }

#if false
        var diaInhabil = await diaInhabilRepository.ObtenerPorIdAsync(
            request.IdDiaInhabil);

        if (diaInhabil is null)
            throw new KeyNotFoundException("No existe el día inhábil.");

        var sede = await sedeRepository.ObtenerPorIdAsync(request.IdSede);

        if (sede is null)
            throw new KeyNotFoundException("No existe la sede.");

        var existeRelacion =
            await diaInhabilSedeRepository.ExisteRelacionAsync(
                request.IdDiaInhabil,
                request.IdSede);

        if (existeRelacion)
            throw new InvalidOperationException("La relación ya existe.");

        var areasAplicables = request.EsGeneral
            ? []
            : await ObtenerExcepcionesValidasAsync(request.IdSede, request.DiaInhabilAreas);

        var fechaRegistro = MexicoCityDateTime.Ahora();

        var entidad = new DiaInhabilSedeEntity
        {
            IdDiaInhabil = request.IdDiaInhabil,
            IdSede = request.IdSede,
            EsGeneral = request.EsGeneral,
            Activo = true,
            FechaRegistro = fechaRegistro
        };

        if (!request.EsGeneral)
        {
            foreach (var areaRequest in areasAplicables)
            {
                entidad.DiaInhabilAreas.Add(new Models.DiaInhabilArea
                {
                    IdArea = areaRequest.IdArea,
                    TipoAplicacion = areaRequest.TipoAplicacion,
                    Activo = true,
                    FechaRegistro = fechaRegistro
                });
            }
        }

        var resultado =
            await diaInhabilSedeRepository.CrearAsync(entidad);

        var creado =
            await diaInhabilSedeRepository.ObtenerPorIdAsync(resultado.Id);

        if (creado is null)
            throw new InvalidOperationException(
                "No fue posible recuperar la relación creada.");

        var numeroSedesPorDia = await diaInhabilSedeRepository.ContarSedesPorDiaAsync(
            [creado.IdDiaInhabil]);
        return [MapearDiaInhabilSede(creado,
            numeroSedesPorDia.GetValueOrDefault(creado.IdDiaInhabil))];
    }

#endif
    public async Task<IReadOnlyCollection<DiaInhabilSedeResponse>> AgregarSedesAsync(
        int idDiaInhabil, AgregarSedesDiaInhabilRequest request)
    {
        if (await diaInhabilRepository.ObtenerPorIdAsync(idDiaInhabil) is null)
            throw new KeyNotFoundException("No existe el dia inhabil.");

        var entidades = new List<DiaInhabilSedeEntity>();
        var relacionesActualizadas = new List<DiaInhabilSedeEntity>();
        var fechaRegistro = MexicoCityDateTime.Ahora();
        foreach (var sedeRequest in request.Sedes)
        {
            if (await sedeRepository.ObtenerPorIdAsync(sedeRequest.IdSede) is null)
                throw new KeyNotFoundException("No existe la sede.");
            var relacionExistente = await diaInhabilSedeRepository.ObtenerParaActualizarAsync(
                idDiaInhabil, sedeRequest.IdSede);
            if (relacionExistente is not null)
            {
                foreach (var area in relacionExistente.DiaInhabilAreas.Where(area =>
                    area.Activo && area.TipoAplicacion == 1 &&
                    !request.IdAreasIncluidas.Contains(area.IdArea)))
                {
                    area.Activo = false;
                }

                if (sedeRequest.EsGeneral)
                {
                    relacionExistente.EsGeneral = true;
                    foreach (var area in relacionExistente.DiaInhabilAreas
                        .Where(area => area.Activo && area.TipoAplicacion == 0))
                    {
                        area.Activo = false;
                    }

                    if (request.IdAreasIncluidas.Count > 0)
                    {
                        await RegistrarCambiosDeAreasAsync(relacionExistente,
                            sedeRequest.IdSede, ObtenerSolicitudesAreas(sedeRequest,
                                request.IdAreasIncluidas), fechaRegistro);
                    }

                    relacionesActualizadas.Add(relacionExistente);
                    continue;
                }

                await RegistrarCambiosDeAreasAsync(relacionExistente, sedeRequest.IdSede,
                    ObtenerSolicitudesAreas(sedeRequest, request.IdAreasIncluidas), fechaRegistro);
                relacionExistente.EsGeneral = false;
                relacionesActualizadas.Add(relacionExistente);
                continue;
            }

            var entidad = new DiaInhabilSedeEntity
            {
                IdDiaInhabil = idDiaInhabil,
                IdSede = sedeRequest.IdSede,
                EsGeneral = sedeRequest.EsGeneral,
                Activo = true,
                FechaRegistro = fechaRegistro
            };

            if (!sedeRequest.EsGeneral || request.IdAreasIncluidas.Count > 0)
            {
                var areasAplicables = await ObtenerExcepcionesValidasAsync(
                    sedeRequest.IdSede, ObtenerSolicitudesAreas(sedeRequest,
                        request.IdAreasIncluidas));
                foreach (var areaRequest in areasAplicables)
                {
                    entidad.DiaInhabilAreas.Add(new Models.DiaInhabilArea
                    {
                        IdArea = areaRequest.IdArea,
                        TipoAplicacion = areaRequest.TipoAplicacion,
                        Activo = true,
                        FechaRegistro = fechaRegistro
                    });
                }
            }
            entidades.Add(entidad);
        }

        await diaInhabilSedeRepository.CrearVariosAsync(entidades);
        if (relacionesActualizadas.Count > 0)
            await diaInhabilSedeRepository.GuardarCambiosAsync();
        var numeroSedesPorDia = await diaInhabilSedeRepository.ContarSedesPorDiaAsync(
            [idDiaInhabil]);
        var resultado = new List<DiaInhabilSedeResponse>();
        foreach (var entidad in entidades)
        {
            var creada = await diaInhabilSedeRepository.ObtenerPorIdAsync(entidad.Id)
                ?? throw new InvalidOperationException("No fue posible recuperar la relacion creada.");
            resultado.Add(MapearDiaInhabilSede(creada,
                numeroSedesPorDia.GetValueOrDefault(idDiaInhabil)));
        }
        foreach (var entidad in relacionesActualizadas)
        {
            var actualizada = await diaInhabilSedeRepository.ObtenerPorIdAsync(entidad.Id)
                ?? throw new InvalidOperationException("No fue posible recuperar la relacion actualizada.");
            resultado.Add(MapearDiaInhabilSede(actualizada,
                numeroSedesPorDia.GetValueOrDefault(idDiaInhabil)));
        }
        return resultado;
    }

    private async Task RegistrarCambiosDeAreasAsync(DiaInhabilSedeEntity relacion,
        int idSede, IReadOnlyCollection<DiaInhabilAreaRequest> solicitudes,
        DateTime fechaRegistro)
    {
        if (solicitudes.GroupBy(area => area.IdArea).Any(grupo => grupo.Count() > 1))
            throw new ArgumentException("No se puede enviar la misma area mas de una vez.");

        var idsArea = solicitudes.Select(area => area.IdArea).ToArray();
        var areas = await sedeRepository.ObtenerAreasAsync(idsArea);
        if (areas.Count != idsArea.Length)
            throw new KeyNotFoundException("Una o mas areas no existen.");

        var sedePorArea = areas.ToDictionary(area => area.IdArea, area => area.IdSede);
        foreach (var solicitud in solicitudes)
        {
            var anterior = relacion.DiaInhabilAreas
                .Where(area => area.IdArea == solicitud.IdArea && area.Activo)
                .OrderByDescending(area => area.Id)
                .FirstOrDefault();
            var incluidaPorDefecto = sedePorArea[solicitud.IdArea] == idSede;
            var incluidaActualmente = anterior is null
                ? incluidaPorDefecto
                : anterior.TipoAplicacion == 1;
            var incluir = solicitud.TipoAplicacion == 1;

            if (incluidaActualmente == incluir)
                continue;

            if (anterior is not null)
            {
                anterior.Activo = false;

                // Al desactivar la excepción se recupera la pertenencia base de la sede.
                // Sólo se crea otra fila si el estado solicitado aún difiere de esa base.
                if (incluir == incluidaPorDefecto)
                    continue;
            }

            relacion.DiaInhabilAreas.Add(new Models.DiaInhabilArea
            {
                IdArea = solicitud.IdArea,
                TipoAplicacion = solicitud.TipoAplicacion,
                Activo = true,
                FechaRegistro = fechaRegistro
            });
        }
    }

    private static IReadOnlyCollection<DiaInhabilAreaRequest> ObtenerSolicitudesAreas(
        SedeDiaInhabilRequest request,
        IReadOnlyCollection<int> idsAreasIncluidas) =>
        request.IdAreasExcluidas.Select(idArea => new DiaInhabilAreaRequest
        {
            IdArea = idArea,
            TipoAplicacion = 0
        }).Concat(idsAreasIncluidas.Select(idArea => new DiaInhabilAreaRequest
        {
            IdArea = idArea,
            TipoAplicacion = 1
        })).ToArray();

    public async Task<DiaInhabilSedeResponse?> ActualizarAsync(
        int id, ActualizarDiaInhabilSedeRequest request)
    {
        var entidad = await diaInhabilSedeRepository.ObtenerParaActualizarAsync(id);
        if (entidad is null)
            return null;

        var idDiaInhabil = request.IdDiaInhabil ?? entidad.IdDiaInhabil;
        var idSede = request.IdSede ?? entidad.IdSede;
        var esGeneral = request.EsGeneral ?? entidad.EsGeneral;

        if (request.IdDiaInhabil.HasValue &&
            await diaInhabilRepository.ObtenerPorIdAsync(idDiaInhabil) is null)
            throw new KeyNotFoundException("No existe el día inhábil.");
        if (request.IdSede.HasValue &&
            await sedeRepository.ObtenerPorIdAsync(idSede) is null)
            throw new KeyNotFoundException("No existe la sede.");

        if ((idDiaInhabil != entidad.IdDiaInhabil || idSede != entidad.IdSede) &&
            await diaInhabilSedeRepository.ExisteRelacionAsync(idDiaInhabil, idSede))
            throw new InvalidOperationException("La relación ya existe.");

        var reemplazarAreas = request.DiaInhabilAreas is not null ||
                              request.EsGeneral == true ||
                              idSede != entidad.IdSede;
        var areasSolicitadas = request.DiaInhabilAreas ?? entidad.DiaInhabilAreas
            .Select(area => new DiaInhabilAreaRequest
            {
                IdArea = area.IdArea,
                TipoAplicacion = area.TipoAplicacion
            }).ToList();
        var areas = areasSolicitadas;

        if (areas.GroupBy(area => area.IdArea).Any(grupo => grupo.Count() > 1))
            throw new ArgumentException("No se puede enviar la misma área más de una vez.");
        if (!esGeneral && areas.Count == 0)
            throw new ArgumentException("Debe especificar al menos un área cuando el día inhábil no es general.");

        if (!esGeneral && !await diaInhabilSedeRepository.AreasPertenecenASedeAsync(
                idSede, areas.Select(area => area.IdArea).ToArray()))
            throw new KeyNotFoundException(
                "Una o más áreas no existen o no pertenecen a la sede seleccionada.");

        var areasAplicables = !esGeneral && reemplazarAreas
            ? await ObtenerExcepcionesValidasAsync(idSede, areasSolicitadas)
            : [];

        entidad.IdDiaInhabil = idDiaInhabil;
        entidad.IdSede = idSede;
        entidad.EsGeneral = esGeneral;
        if (request.Activo.HasValue)
            entidad.Activo = request.Activo.Value;

        if (reemplazarAreas)
        {
            var areasAnteriores = entidad.DiaInhabilAreas.ToList();
            diaInhabilSedeRepository.EliminarAreas(areasAnteriores);
            foreach (var area in areasAnteriores)
                entidad.DiaInhabilAreas.Remove(area);

            if (!esGeneral)
            {
                foreach (var area in areasAplicables)
                {
                    entidad.DiaInhabilAreas.Add(new Models.DiaInhabilArea
                    {
                        IdArea = area.IdArea,
                        TipoAplicacion = area.TipoAplicacion,
                        Activo = true,
                        FechaRegistro = MexicoCityDateTime.Ahora()
                    });
                }
            }
        }

        await diaInhabilSedeRepository.GuardarCambiosAsync();
        var actualizada = await diaInhabilSedeRepository.ObtenerPorIdAsync(id);
        if (actualizada is null)
            return null;

        var numeroSedesPorDia = await diaInhabilSedeRepository.ContarSedesPorDiaAsync(
            [actualizada.IdDiaInhabil]);
        return MapearDiaInhabilSede(actualizada,
            numeroSedesPorDia.GetValueOrDefault(actualizada.IdDiaInhabil));
    }
    
    private async Task<IReadOnlyCollection<DiaInhabilAreaRequest>>
        ObtenerExcepcionesValidasAsync(int idSede,
            IReadOnlyCollection<DiaInhabilAreaRequest> areasSolicitadas)
    {
        if (areasSolicitadas.GroupBy(area => area.IdArea)
            .Any(grupo => grupo.Count() > 1))
        {
            throw new ArgumentException("No se puede enviar la misma area mas de una vez.");
        }

        var idsArea = areasSolicitadas.Select(area => area.IdArea).ToArray();
        var areas = await sedeRepository.ObtenerAreasAsync(idsArea);
        if (areas.Count != idsArea.Length)
            throw new KeyNotFoundException("Una o mas areas no existen.");

        var sedePorArea = areas.ToDictionary(area => area.IdArea, area => area.IdSede);
        var resultado = new List<DiaInhabilAreaRequest>();

        foreach (var area in areasSolicitadas)
        {
            var yaIncluidaEnSede = sedePorArea[area.IdArea] == idSede;

            if (area.TipoAplicacion == 1 && yaIncluidaEnSede)
            {
                throw new ArgumentException(
                    $"El area {area.IdArea} ya esta incluida en la sede y solo puede excluirse.");
            }

            if (area.TipoAplicacion == 0 && !yaIncluidaEnSede)
            {
                throw new ArgumentException(
                    $"El area {area.IdArea} no pertenece a la sede y solo puede incluirse.");
            }

            if ((area.TipoAplicacion == 0 && yaIncluidaEnSede) ||
                (area.TipoAplicacion == 1 && !yaIncluidaEnSede))
            {
                resultado.Add(area);
            }
        }

        return resultado;
    }

    private static DiaInhabilSedeResponse MapearDiaInhabilSede(
        DiaInhabilSedeEntity diaSede, int numeroSedes) => new()
    {
        Id = diaSede.Id,
        EsGeneral = diaSede.EsGeneral,
        Activo = diaSede.Activo,
        FechaRegistro = diaSede.FechaRegistro,
        NumeroSedes = numeroSedes,

        DiaInhabil = new DiaInhabilResponse
        {
            Id = diaSede.DiaInhabil!.Id,
            Nombre = diaSede.DiaInhabil.Nombre,
            Descripcion = diaSede.DiaInhabil.Descripcion,
            FechaInicio = diaSede.DiaInhabil.FechaInicio,
            FechaFin = diaSede.DiaInhabil.FechaFin,
            Activo = diaSede.DiaInhabil.Activo
        },

        Sede = new SedeResponse
        {
            Id = diaSede.Sede!.Id,
            Nombre = diaSede.Sede.Nombre,
            Descripcion = diaSede.Sede.Descripcion,
            Activo = diaSede.Sede.Activo,
            NumeroAreas = diaSede.Sede.Areas.Count,
            Areas = diaSede.Sede.Areas.Select(area => new AreaResponse
            {
                IdArea = area.IdArea,
                Nombre = area.Nombre,
                Activo = area.Activo,
                IdSede = area.IdSede
            }).ToList(),
            AreasExcluidas = diaSede.DiaInhabilAreas
                .Where(area => area.Activo && area.TipoAplicacion == 0)
                .Select(area => new DiaInhabilAreaResponse
                {
                    Id = area.Id,
                    IdArea = area.IdArea,
                    NombreArea = area.Area?.Nombre,
                    TipoAplicacion = area.TipoAplicacion,
                    Activo = area.Activo
                })
                .ToList()
        }
    };

    private static DiaInhabilResponse MapearDiaInhabil(DiaInhabil dia) => new()
    {
        Id = dia.Id,
        Nombre = dia.Nombre,
        Descripcion = dia.Descripcion,
        FechaInicio = dia.FechaInicio,
        FechaFin = dia.FechaFin,
        Activo = dia.Activo
    };

    private static SedeDiaInhabilResponse MapearSedeDiaInhabil(
        DiaInhabilSedeEntity relacion)
    {
        var respuesta = MapearDiaInhabilSede(relacion, 0);
        return new SedeDiaInhabilResponse
        {
            Id = respuesta.Id,
            EsGeneral = respuesta.EsGeneral,
            Activo = respuesta.Activo,
            FechaRegistro = respuesta.FechaRegistro,
            IdAreasIncluidas = relacion.DiaInhabilAreas
                .Where(area => area.Activo && area.TipoAplicacion == 1)
                .Select(area => area.IdArea)
                .ToList(),
            Sede = respuesta.Sede
        };
    }

}
