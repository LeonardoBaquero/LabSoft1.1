using System;
using System.Threading.Tasks;
using GestionInventario.Models;
using GestionInventario.Repositories;

/*
La clase `UsuarioServicio` actúa como intermediario entre la lógica de negocio y el repositorio de usuarios. 
Proporciona métodos para validar usuarios y gestionar la información relacionada con ellos.
*/

namespace GestionInventario.Services
{
    public class UsuarioServicio
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        // Constructor que recibe el repositorio mediante inyección de dependencias
        public UsuarioServicio(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        // Método para validar un usuario
        public async Task<bool> ValidarUsuario(Usuario usuario)
        {
            // Obtener el usuario por correo
            var usuarioAlmacenado = await _usuarioRepositorio.ObtenerUsuarioPorCorreo(usuario.Correo);

            // Verificar si el usuario existe y si la contraseña coincide
            if (usuarioAlmacenado != null)
            {
                // TODO: Implementar una comparación segura de contraseñas
                // Por ahora usamos una comparación simple
                return usuarioAlmacenado.Contrasena == usuario.Contrasena;
            }

            return false;
        }

        // Método para obtener usuario por correo
        public async Task<Usuario> ObtenerUsuarioPorCorreo(string correo)
        {
            return await _usuarioRepositorio.ObtenerUsuarioPorCorreo(correo);
        }

        // Método para crear un nuevo usuario
        public async Task<Usuario> CrearUsuario(Usuario usuario)
        {
            // Aquí podrías agregar validaciones adicionales antes de crear el usuario
            return await _usuarioRepositorio.CrearUsuario(usuario);
        }

        // Método para modificar un usuario existente
        public async Task ModificarUsuario(Usuario usuario)
        {
            await _usuarioRepositorio.ModificarUsuario(usuario);
        }

        // Método para activar un usuario
        public async Task ActivarUsuario(int id)
        {
            await _usuarioRepositorio.ActivarUsuario(id);
        }

        // Método para inactivar un usuario
        public async Task InactivarUsuario(int id)
        {
            await _usuarioRepositorio.InactivarUsuario(id);
        }
    }
}