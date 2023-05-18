using System.ComponentModel.DataAnnotations;

namespace LoginAPI.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Rol { get; set; }
        public int Estado { get; set; }
        public Usuario()
        {

        }

        public Usuario(UsuarioInputModel usuario)
        {
            this.Correo = usuario.Correo;
            this.Clave = usuario.Clave;
            this.Nombre = usuario.Nombre;  
            this.Apellido = usuario.Apellido;
        }
    }
    public class UsuarioInputModel
    {
        [Required(ErrorMessage = "El {0} es requerido")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Correo no valido")]
        public string Correo { get; set; }

        
        [Required(ErrorMessage = "La {0} es requerida")]
        [StringLength(50, ErrorMessage = "La {0} debe tener entre {2} y {1} caracteres.", MinimumLength = 6)]
        public string Clave { get; set; }

        
        [Required(ErrorMessage = "La confirmacion de clave es requerida")]
        [StringLength(50, ErrorMessage = "La {0} debe tener entre {2} y {1} caracteres.", MinimumLength = 6)]
        public string ConfirmacionClave { get; set; }


        [Required(ErrorMessage = "El {0} es requerido")]
        [StringLength(50, ErrorMessage = "El {0} debe tener entre {2} y {1} caracteres.", MinimumLength = 1)]
        public string Nombre { get; set; }


        [StringLength(50, ErrorMessage = "El {0} debe tener entre {2} y {1} caracteres.", MinimumLength = 0)]
        public string Apellido { get; set; }
    }

    public class LoginInputModel
    {
        [Required]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Correo no valido")]
        public string Correo { get; set; }
        [Required(ErrorMessage = "La clave es requerida")]
        public string Clave { get; set; }
    }

    public class ActualizarClaveInputModel
    {
        [Required]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Correo no valido")]
        public string Correo { get; set; }


        [Required(ErrorMessage = "La {0} es requerida")]
        public string ClaveActual { get; set; }


        [Required(ErrorMessage = "La {0} es requerida")]
        public string NuevaClave { get; set; }


        [Required(ErrorMessage = "La {0} de clave es requerida")]
        public string ConfirmacionClave { get; set; }
    }
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public string Correo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Rol { get; set; }
        public int Estado { get; set; }

        public UsuarioViewModel(Usuario usuario)
        {
            Id = usuario.Id;
            Correo = usuario.Correo;
            Nombre = usuario.Nombre;
            Apellido = usuario.Apellido;
            Rol = usuario.Rol;
            Estado = usuario.Estado; 
        }
    }
}
