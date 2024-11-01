using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GestionInventario.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La categoría es requerida")]
        public string Categoria { get; set; }

        [Required(ErrorMessage = "La cantidad es requerida")]
        [Range(0, int.MaxValue)]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El precio unitario es requerido")]
        [Range(0, double.MaxValue)]
        public decimal PrecioUnitario { get; set; }

        public DateTime? FechaExpiracion { get; set; }

        // Cambiado para que sea requerido
        [Required(ErrorMessage = "El ID del proveedor es requerido")]
        public int ProveedorId { get; set; }

        // Cambiado para ignorar en la serialización
        [JsonIgnore]
        public virtual Proveedor? Proveedor { get; set; }
    }
}



