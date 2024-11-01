using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GestionInventario.Models
{
    public class Proveedor
    {
        public Proveedor()
        {
            // Inicializar la colección solo si es necesario
            Productos = new List<Producto>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es requerido")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El tipo de documento es requerido")]
        public string TipoDocumento { get; set; }

        [Required(ErrorMessage = "El número de documento es requerido")]
        public string NumeroDocumento { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public string TipoMercancia { get; set; }

        public bool EstadoActivo { get; set; }

        [Required(ErrorMessage = "El correo es requerido")]
        [EmailAddress(ErrorMessage = "El formato del correo no es válido")]
        public string Correo { get; set; }

        [JsonIgnore] // Evita la serialización cíclica
        public virtual ICollection<Producto> Productos { get; set; }

        
    }
}