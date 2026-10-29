using Microsoft.AspNetCore.Mvc;
using DSW_3P_PR01.Services;
using DSW_3P_PR01.Models;

namespace DSW_3P_PR01.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MisionesController : ControllerBase
{
    private readonly IMisionService _service;

    public MisionesController(IMisionService service)
    {
        _service = service;
    }

    //GET - Obtener todas las misiones
    [HttpGet]
    [ProducesResponseType(typeof(List<Mision>),200)]
    public IActionResult ObtenerTodas()
    {
        return Ok(_service.ObtenerTodas());
    }


    //GET - Obtener por id
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Mision),200)]
    [ProducesResponseType(404)]
    public IActionResult ObtenerPorId(int id)
    {
        var mision = _service.ObtenerPorId(id);

        if (mision == null)
            return NotFound();
        
        return Ok(mision);
    }


    //POST - Agregar canción
    [HttpPost]
    [ProducesResponseType(typeof(Mision),200)]
    [ProducesResponseType(400)]
    public IActionResult Agregar([FromBody] Mision mision)
    {
        try
        {
            var creada = _service.Agregar(mision);

            return CreatedAtAction(
                nameof(ObtenerPorId),
                new {id= creada.Id},
                creada
            );
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new {mensaje = ex.Message});
        }
    }

    //PUT - Actualizar Canción
    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public IActionResult Actualizar(int id, [FromBody] Mision mision)
    {
        try
        {
            if(!_service.Actualizar(id, mision))
                return NotFound();

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new {mensaje = ex.Message});
        }
    }

    //DELETE - Eliminar canción
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult Eliminar(int id)
    {
        if(!_service.Eliminar(id))
            return NotFound();
        
        return NoContent();
    }
}