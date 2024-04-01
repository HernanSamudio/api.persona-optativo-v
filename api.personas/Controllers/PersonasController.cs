using Microsoft.AspNetCore.Mvc;
using Repository.Data;
using System;
using System.Collections.Generic;

namespace api.personas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonasController : ControllerBase
    {
        private readonly PersonaRepository _personaRepository;
        private readonly IConfiguration _configuration;

        public PersonasController(IConfiguration configuration)
        {
            _configuration = configuration;
            _personaRepository = new PersonaRepository(_configuration);
        }

        [HttpPost]
        public IActionResult AgregarPersona([FromBody] PersonaModel persona)
        {
            try
            {
                bool resultado = _personaRepository.add(persona);
                if (resultado)
                {
                    return Ok("Persona agregada exitosamente");
                }
                else
                {
                    return BadRequest("No se pudo agregar la persona");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarPersona(int id)
        {
            try
            {
                PersonaModel persona = _personaRepository.get(id);
                if (persona != null)
                {
                    bool resultado = _personaRepository.remove(persona);
                    if (resultado)
                    {
                        return Ok("Persona eliminada exitosamente");
                    }
                    else
                    {
                        return BadRequest("No se pudo eliminar la persona");
                    }
                }
                else
                {
                    return NotFound("No se encontró la persona");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarPersona(int id, [FromBody] PersonaModel persona)
        {
            try
            {
                persona.Id = id;
                bool resultado = _personaRepository.update(persona);
                if (resultado)
                {
                    return Ok("Persona actualizada exitosamente");
                }
                else
                {
                    return BadRequest("No se pudo actualizar la persona");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerPersona(int id)
        {
            try
            {
                PersonaModel persona = _personaRepository.get(id);

                if (persona != null)
                {
                    return Ok(persona);
                }
                else
                {
                    return NotFound("No se encontró la persona");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult ListarPersonas()
        {
            try
            {
                IEnumerable<PersonaModel> personas = _personaRepository.list();
                return Ok(personas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
