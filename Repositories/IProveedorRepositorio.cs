using GestionInventario.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionInventario.Repositories
{
    public interface IProveedorRepositorio
    {
        Task<IEnumerable<Proveedor>> GetAllProveedoresAsync();
        Task<Proveedor> GetProveedorByIdAsync(int id);
        Task<Proveedor> CreateProveedorAsync(Proveedor proveedor);
        Task UpdateProveedorAsync(Proveedor proveedor);
        Task DeleteProveedorAsync(int id);
    }
}