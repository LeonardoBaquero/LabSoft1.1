using Microsoft.AspNetCore.Mvc;
using GestionInventario.Models;
using GestionInventario.Services;
using System;
using System.Threading.Tasks;

namespace GestionInventario.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutenticarController : ControllerBase
    {
        private readonly UsuarioServicio _usuarioServicio;

        // Constructor modificado para usar inyección de dependencias
        public AutenticarController(UsuarioServicio usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
        }

        // Método modificado para ser asíncrono
        [HttpPost("validar")]
        public async Task<IActionResult> ValidarUsuario([FromBody] Usuario credenciales)
        {
            // Crea un objeto Usuario a partir de las credenciales proporcionadas
            var usuario = new Usuario 
            { 
                Correo = credenciales.Correo, 
                Contrasena = credenciales.Contrasena 
            }; 

            // Valida las credenciales del usuario de manera asíncrona
            bool esValido = await _usuarioServicio.ValidarUsuario(usuario);

            if (esValido)
            {
                // Si las credenciales son válidas, obtiene el usuario autenticado
                var usuarioAutenticado = await _usuarioServicio.ObtenerUsuarioPorCorreo(credenciales.Correo);
                return Ok(new
                {
                    AutenticacionExitosa = true,
                    Jwt = Guid.NewGuid().ToString(), // Genera un nuevo JWT (simulado)
                    Mensaje = usuarioAutenticado != null 
                        ? $"Bienvenido {usuarioAutenticado.Nombre} {usuarioAutenticado.Apellido}"
                        : "Bienvenido"
                });
            }
            else
            {
                // Retorna un error 401 si la autenticación falla
                return Unauthorized(new
                {
                    AutenticacionExitosa = false,
                    Jwt = null as string,
                    Mensaje = "Error al autenticar el usuario"
                });
            }
        }
    }
}


    // Clase para representar las credenciales del usuario

    /* Se toma usuario y contrasena de Medels-Ususario 
    public class CredencialesUsuario
    {
        public string Correo { get; set; } // Correo electrónico del usuario
        public string Contrasena { get; set; } // Contraseña del usuario
    }
    */


/*
La clase AutenticarController proporciona un endpoint para autenticar usuarios mediante sus credenciales. 
Utiliza el servicio UsuarioServicio para validar estas credenciales y manejar la autenticación.
*/

/*
SON Web Token (JWT) es un estándar abierto basado en JSON para crear un token 
que sirva para enviar datos entre aplicaciones o servicios y garantizar que sean válidos y seguros.
*/