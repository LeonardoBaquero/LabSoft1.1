using System;
using System.ComponentModel.DataAnnotations;

/* Descripción
La clase `Usuario` representa un modelo de usuario en el sistema de gestión de inventario. 
Esta clase contiene propiedades que describen las características y la información personal de un usuario.

*/ 

namespace GestionInventario.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es requerido")]
        [StringLength(50)]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El tipo de documento es requerido")]
        public string TipoDocumento { get; set; }

        [Required(ErrorMessage = "El número de documento es requerido")]
        public string NumeroDocumento { get; set; }

        [Required(ErrorMessage = "La dirección es requerida")]
        public string Direccion { get; set; }

        [Phone]
        public string Telefono { get; set; }

        public bool EstadoActivo { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre 6 y 100 caracteres")]
        public string Contrasena { get; set; }

        [Required(ErrorMessage = "El correo es requerido")]
        [EmailAddress(ErrorMessage = "El formato del correo no es válido")]
        public string Correo { get; set; }
    }
}

