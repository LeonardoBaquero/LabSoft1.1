using GestionInventario.Data;
using GestionInventario.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionInventario.Repositories
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly AppDbContext _context;

        public UsuarioRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> CrearUsuario(Usuario usuario)
        {
            try
            {
                await _context.Usuarios.AddAsync(usuario);
                await _context.SaveChangesAsync();
                return usuario;
            }
            catch (Exception)
            {
                throw; // Re-lanza la excepción para manejarla en el controlador
            }
        }

        public async Task ModificarUsuario(Usuario usuario)
        {
            try
            {
                var usuarioExistente = await _context.Usuarios.FindAsync(usuario.Id);
                if (usuarioExistente != null)
                {
                    usuarioExistente.Nombre = usuario.Nombre;
                    usuarioExistente.Apellido = usuario.Apellido;
                    usuarioExistente.TipoDocumento = usuario.TipoDocumento;
                    usuarioExistente.NumeroDocumento = usuario.NumeroDocumento;
                    usuarioExistente.Direccion = usuario.Direccion;
                    usuarioExistente.Telefono = usuario.Telefono;
                    usuarioExistente.EstadoActivo = usuario.EstadoActivo;
                    usuarioExistente.Contrasena = usuario.Contrasena;
                    usuarioExistente.Correo = usuario.Correo;

                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Usuario> ObtenerUsuarioPorCorreo(string correo)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == correo);
        }

        public async Task<List<Usuario>> ObtenerListadoUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task ActivarUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                usuario.EstadoActivo = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task InactivarUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                usuario.EstadoActivo = false;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Usuario> ObtenerUsuarioPorId(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }
    }
}