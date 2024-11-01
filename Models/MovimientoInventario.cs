using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GestionInventario.Models
{
    public class MovimientoInventario
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "El ID del producto es requerido")]
        public int ProductoId { get; set; }

        [JsonIgnore]
        public virtual Producto? Producto { get; set; }

        [Required(ErrorMessage = "El tipo de movimiento es requerido")]
        [Range(0, 1, ErrorMessage = "El tipo de movimiento debe ser 0 (Entrada) o 1 (Salida)")]
        public int TipoMovimiento { get; set; }

        [Required(ErrorMessage = "La cantidad es requerida")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que 0")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "La razón es requerida")]
        public string Razon { get; set; } = string.Empty;
    }
}