using GestionInventario.Data;
using GestionInventario.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionInventario.Repositories
{
    public class ProductoRepositorio : IProductoRepositorio
    {
        private readonly AppDbContext _context;

        public ProductoRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Producto>> GetAllProductosAsync()
        {
            try
            {
                return await _context.Productos
                    .Include(p => p.Proveedor)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener los productos: {ex.Message}", ex);
            }
        }

        public async Task<Producto> GetProductoByIdAsync(int id)
        {
            try
            {
                return await _context.Productos
                    .Include(p => p.Proveedor)
                    .FirstOrDefaultAsync(p => p.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el producto: {ex.Message}", ex);
            }
        }

        // Método agregado para obtener proveedor
        public async Task<Proveedor> GetProveedorByIdAsync(int id)
        {
            return await _context.Proveedores.FindAsync(id);
        }

        public async Task<Producto> CreateProductoAsync(Producto producto)
        {
            try
            {
                // Verificar si existe el proveedor
                if (producto.ProveedorId > 0) // Cambiado HasValue por comparación directa
                {
                    var proveedorExists = await _context.Proveedores
                        .AnyAsync(p => p.Id == producto.ProveedorId);

                    if (!proveedorExists)
                    {
                        throw new InvalidOperationException(
                            $"El proveedor con ID {producto.ProveedorId} no existe."
                        );
                    }
                }

                await _context.Productos.AddAsync(producto);
                await _context.SaveChangesAsync();

                // Recarga el producto con sus relaciones
                return await _context.Productos
                    .Include(p => p.Proveedor)
                    .FirstOrDefaultAsync(p => p.Id == producto.Id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear el producto: {ex.Message}", ex);
            }
        }

        public async Task UpdateProductoAsync(Producto producto)
        {
            try
            {
                var productoExistente = await _context.Productos.FindAsync(producto.Id);
                if (productoExistente != null)
                {
                    // Verificar si existe el proveedor al actualizar
                    if (producto.ProveedorId > 0) // Cambiado HasValue por comparación directa
                    {
                        var proveedorExists = await _context.Proveedores
                            .AnyAsync(p => p.Id == producto.ProveedorId);

                        if (!proveedorExists)
                        {
                            throw new InvalidOperationException(
                                $"El proveedor con ID {producto.ProveedorId} no existe."
                            );
                        }
                    }

                    _context.Entry(productoExistente).CurrentValues.SetValues(producto);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el producto: {ex.Message}", ex);
            }
        }

        public async Task DeleteProductoAsync(int id)
        {
            try
            {
                var producto = await _context.Productos.FindAsync(id);
                if (producto != null)
                {
                    _context.Productos.Remove(producto);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el producto: {ex.Message}", ex);
            }
        }
    }
}