using GestionInventario.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionInventario.Repositories
{
    public interface IMovimientoInventarioRepositorio
    {
        Task<IEnumerable<MovimientoInventario>> GetMovimientosByProductoAsync(int productoId);
        Task<MovimientoInventario> CreateMovimientoAsync(MovimientoInventario movimiento);
        Task<IEnumerable<MovimientoInventario>> GetAllMovimientosAsync();
    }
}