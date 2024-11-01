using GestionInventario.Data;
using GestionInventario.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionInventario.Repositories
{
    public class MovimientoInventarioRepositorio : IMovimientoInventarioRepositorio
    {
        private readonly AppDbContext _context;

        public MovimientoInventarioRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MovimientoInventario>> GetAllMovimientosAsync()
        {
            return await _context.MovimientosInventario
                .Include(m => m.Producto)
                .OrderByDescending(m => m.Fecha)
                .ToListAsync();
        }

        public async Task<IEnumerable<MovimientoInventario>> GetMovimientosByProductoAsync(int productoId)
        {
            return await _context.MovimientosInventario
                .Include(m => m.Producto)
                .Where(m => m.ProductoId == productoId)
                .OrderByDescending(m => m.Fecha)
                .ToListAsync();
        }

        public async Task<MovimientoInventario> CreateMovimientoAsync(MovimientoInventario movimiento)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var producto = await _context.Productos
                .FirstOrDefaultAsync(p => p.Id == movimiento.ProductoId);

                if (producto == null)
                {
                    throw new InvalidOperationException($"El producto con ID {movimiento.ProductoId} no existe.");
                }

                // Actualizar el inventario del producto
                if (movimiento.TipoMovimiento == 0) // Entrada
                {
                    producto.Cantidad += movimiento.Cantidad;
                }
                else if (movimiento.TipoMovimiento == 1) // Salida
                {
                    if (producto.Cantidad < movimiento.Cantidad)
                    {
                        throw new InvalidOperationException($"No hay suficiente stock disponible. Stock actual: {producto.Cantidad}");
                    }
                    producto.Cantidad -= movimiento.Cantidad;
                }

                // Guardar el movimiento
                //movimiento.Fecha = DateTime.Now;
                await _context.MovimientosInventario.AddAsync(movimiento);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return movimiento;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}