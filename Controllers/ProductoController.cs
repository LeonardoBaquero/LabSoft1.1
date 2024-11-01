using GestionInventario.Models;
using GestionInventario.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace GestionInventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoRepositorio _productoRepositorio;

        public ProductoController(IProductoRepositorio productoRepositorio)
        {
            _productoRepositorio = productoRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            try
            {
                var productos = await _productoRepositorio.GetAllProductosAsync();
                return Ok(productos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error al obtener productos: {ex.Message}" });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            try
            {
                var producto = await _productoRepositorio.GetProductoByIdAsync(id);
                if (producto == null)
                {
                    return NotFound(new { mensaje = $"No se encontró el producto con ID {id}" });
                }
                Console.WriteLine($"Producto encontrado: {JsonSerializer.Serialize(producto)}");
                return Ok(producto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, new { mensaje = $"Error al obtener el producto: {ex.Message}" });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Producto>> CreateProducto([FromBody] Producto producto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errores = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    return BadRequest(new { Errores = errores });
                }

                // Validar que el proveedor exista
                var proveedor = await _productoRepositorio.GetProveedorByIdAsync(producto.ProveedorId);
                if (proveedor == null)
                {
                    return BadRequest(new { Mensaje = $"El proveedor con ID {producto.ProveedorId} no existe." });
                }

                producto.Id = 0;
                var createdProducto = await _productoRepositorio.CreateProductoAsync(producto);
                
                return CreatedAtAction(
                    nameof(GetProducto),
                    new { id = createdProducto.Id },
                    createdProducto
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    Mensaje = "Error al crear el producto",
                    Error = ex.Message 
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProducto(int id, Producto producto)
        {
            try
            {
                if (id != producto.Id)
                {
                    return BadRequest(new { mensaje = "El ID del producto no coincide" });
                }

                var existingProduct = await _productoRepositorio.GetProductoByIdAsync(id);
                if (existingProduct == null)
                {
                    return NotFound(new { mensaje = $"No se encontró el producto con ID {id}" });
                }

                await _productoRepositorio.UpdateProductoAsync(producto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error al actualizar el producto: {ex.Message}" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            try
            {
                var producto = await _productoRepositorio.GetProductoByIdAsync(id);
                if (producto == null)
                {
                    return NotFound(new { mensaje = $"No se encontró el producto con ID {id}" });
                }

                await _productoRepositorio.DeleteProductoAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error al eliminar el producto: {ex.Message}" });
            }
        }
    }
}