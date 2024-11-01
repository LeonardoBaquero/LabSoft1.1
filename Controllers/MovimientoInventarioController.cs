using GestionInventario.Models;
using GestionInventario.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionInventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientoInventarioController : ControllerBase
    {
        private readonly IMovimientoInventarioRepositorio _movimientoRepositorio;

        public MovimientoInventarioController(IMovimientoInventarioRepositorio movimientoRepositorio)
        {
            _movimientoRepositorio = movimientoRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovimientoInventario>>> GetMovimientos()
        {
            try
            {
                var movimientos = await _movimientoRepositorio.GetAllMovimientosAsync();
                return Ok(movimientos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error al obtener movimientos: {ex.Message}" });
            }
        }

        [HttpGet("producto/{productoId}")]
        public async Task<ActionResult<IEnumerable<MovimientoInventario>>> GetMovimientosByProducto(int productoId)
        {
            try
            {
                var movimientos = await _movimientoRepositorio.GetMovimientosByProductoAsync(productoId);
                return Ok(movimientos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error al obtener movimientos: {ex.Message}" });
            }
        }

        [HttpPost]
        public async Task<ActionResult<MovimientoInventario>> CreateMovimiento([FromBody] MovimientoInventario movimiento)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new 
                    { 
                        Mensaje = "Datos inválidos",
                        Errores = ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage)
                            .ToList()
                    });
                }

                // No es necesario establecer el ID ni la fecha, se manejan automáticamente
                movimiento.Id = 0;
                movimiento.Fecha = DateTime.Now;

                var createdMovimiento = await _movimientoRepositorio.CreateMovimientoAsync(movimiento);
                return CreatedAtAction(
                    nameof(GetMovimientosByProducto), 
                    new { productoId = movimiento.ProductoId }, 
                    createdMovimiento);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error al crear movimiento: {ex.Message}" });
            }
        }
    }
}