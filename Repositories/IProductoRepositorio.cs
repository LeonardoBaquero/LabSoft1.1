using GestionInventario.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IProductoRepositorio
{
    Task<IEnumerable<Producto>> GetAllProductosAsync();
    Task<Producto> GetProductoByIdAsync(int id);
    Task<Producto> CreateProductoAsync(Producto producto);
    Task UpdateProductoAsync(Producto producto);
    Task DeleteProductoAsync(int id);
    Task<Proveedor> GetProveedorByIdAsync(int id);  // Agregado este método
}