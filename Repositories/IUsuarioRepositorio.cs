using GestionInventario.Models; // Entrega 2
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionInventario.Repositories
{
    public interface IUsuarioRepositorio
    {
    Task<Usuario> CrearUsuario(Usuario usuario);
    Task ModificarUsuario(Usuario usuario);
    Task<Usuario> ObtenerUsuarioPorCorreo(string correo);
    Task<List<Usuario>> ObtenerListadoUsuarios();
    Task ActivarUsuario(int id);
    Task InactivarUsuario(int id);
    Task<Usuario> ObtenerUsuarioPorId(int id);
    }
}