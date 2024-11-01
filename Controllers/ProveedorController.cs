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
    public class ProveedorController : ControllerBase
    {
        private readonly IProveedorRepositorio _proveedorRepositorio;

        public ProveedorController(IProveedorRepositorio proveedorRepositorio)
        {
            _proveedorRepositorio = proveedorRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proveedor>>> GetProveedores()
        {
            try
            {
                var proveedores = await _proveedorRepositorio.GetAllProveedoresAsync();
                return Ok(proveedores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error al obtener proveedores: {ex.Message}" });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Proveedor>> GetProveedor(int id)
        {
            var proveedor = await _proveedorRepositorio.GetProveedorByIdAsync(id);
            if (proveedor == null)
            {
                return NotFound();
            }
            return proveedor;
        }

        [HttpPost]
        public async Task<ActionResult<Proveedor>> CreateProveedor(Proveedor proveedor)
        {
            try
            {
                var createdProveedor = await _proveedorRepositorio.CreateProveedorAsync(proveedor);
                return CreatedAtAction(
                    nameof(GetProveedor), 
                    new { id = createdProveedor.Id }, 
                    createdProveedor
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error al crear proveedor: {ex.Message}" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProveedor(int id, Proveedor proveedor)
        {
            if (id != proveedor.Id)
            {
                return BadRequest();
            }

            try
            {
                await _proveedorRepositorio.UpdateProveedorAsync(proveedor);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error al actualizar proveedor: {ex.Message}" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProveedor(int id)
        {
            try
            {
                await _proveedorRepositorio.DeleteProveedorAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error al eliminar proveedor: {ex.Message}" });
            }
        }
    }
}