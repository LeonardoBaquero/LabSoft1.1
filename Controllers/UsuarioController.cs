using Microsoft.AspNetCore.Mvc;
using GestionInventario.Models;
using GestionInventario.Repositories;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GestionInventario.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        // Constructor modificado para usar inyección de dependencias
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<Usuario>> GetByEmail(string email)
        {
            var usuario = await _usuarioRepositorio.ObtenerUsuarioPorCorreo(email);
            if (usuario == null)
                return NotFound();
            return Ok(usuario);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetAllUsers()
        {
            var usuarios = await _usuarioRepositorio.ObtenerListadoUsuarios();
            return Ok(usuarios);
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> CrearUsuario(Usuario usuario)
        {
            try
            {
                var usuarioCreado = await _usuarioRepositorio.CrearUsuario(usuario);
                return CreatedAtAction(
                    nameof(GetByEmail), 
                    new { email = usuarioCreado.Correo }, 
                    usuarioCreado
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error al crear usuario: {ex.Message}" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ModificarUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Id)
                return BadRequest();

            try
            {
                await _usuarioRepositorio.ModificarUsuario(usuario);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error al modificar usuario: {ex.Message}" });
            }
        }

        [HttpPut("{id}/activar")]
        public async Task<IActionResult> ActivarUsuario(int id)
        {
            try
            {
                await _usuarioRepositorio.ActivarUsuario(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error al activar usuario: {ex.Message}" });
            }
        }

        [HttpPut("{id}/inactivar")]
        public async Task<IActionResult> InactivarUsuario(int id)
        {
            try
            {
                await _usuarioRepositorio.InactivarUsuario(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error al inactivar usuario: {ex.Message}" });
            }
        }
    }
}





/*
La clase UsuarioController proporciona endpoints para gestionar operaciones relacionadas con los usuarios, 
como obtener, crear, modificar, activar e inactivar usuarios.
Controlador para administrar operaciones relacionadas de los usuarios de la clase controllers.
*/


