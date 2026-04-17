using System.ComponentModel.DataAnnotations;

namespace VET_WebAPI.Entidades
{
    // Representa la tabla TVET_CLIENTES
    public class eCliente
    {
        public int TN_IdCliente { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(60)]
        public string TC_Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El primer apellido es requerido")]
        [MaxLength(60)]
        public string TC_Ap1 { get; set; } = null!;

        [Required(ErrorMessage = "El segundo apellido es requerido")]
        [MaxLength(60)]
        public string TC_Ap2 { get; set; } = null!;

        [Required(ErrorMessage = "El teléfono es requerido")]
        [MaxLength(15)]
        public string TC_NumTelefono { get; set; } = null!;

        [Required(ErrorMessage = "El correo es requerido")]
        [MaxLength(100)]
        public string TC_CorreoElectronico { get; set; } = null!;
    }
}
