using System.ComponentModel.DataAnnotations;

namespace VET_MVC.Models
{
    // ── USUARIO (para el login) ──────────────────────────────
    public class mUsuario
    {
        [Display(Name = "Usuario:")]
        [Required(ErrorMessage = "El usuario es requerido")]
        public string usuario { get; set; } = null!;

        [Display(Name = "Contraseña:")]
        [Required(ErrorMessage = "La contraseña es requerida")]
        public string contrasena { get; set; } = null!;
    }

    // Para guardar el token que devuelve la API
    public class mResultadoToken
    {
        public string? token { get; set; }
    }

    // ── CLIENTE ──────────────────────────────────────────────
    public class mCliente
    {
        public int TN_IdCliente { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre es requerido")]
        public string TC_Nombre { get; set; } = null!;

        [Display(Name = "Primer apellido")]
        [Required(ErrorMessage = "El primer apellido es requerido")]
        public string TC_Ap1 { get; set; } = null!;

        [Display(Name = "Segundo apellido")]
        [Required(ErrorMessage = "El segundo apellido es requerido")]
        public string TC_Ap2 { get; set; } = null!;

        [Display(Name = "Teléfono")]
        [Required(ErrorMessage = "El teléfono es requerido")]
        public string TC_NumTelefono { get; set; } = null!;

        [Display(Name = "Correo electrónico")]
        [Required(ErrorMessage = "El correo es requerido")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido")]
        public string TC_CorreoElectronico { get; set; } = null!;
    }

    // ── MASCOTA ──────────────────────────────────────────────
    public class mMascota
    {
        public int TN_IdMascota { get; set; }

        [Display(Name = "Nombre mascota")]
        [Required(ErrorMessage = "El nombre de la mascota es requerido")]
        public string TC_NomMascota { get; set; } = null!;

        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "El cliente es requerido")]
        public int TN_IdCliente { get; set; }

        public string? TC_NombreCliente { get; set; }
    }

    // ── DETALLE MASCOTA ──────────────────────────────────────
    public class mDetalleMascota
    {
        [Display(Name = "Mascota")]
        [Required]
        public int TN_IdMascota { get; set; }

        [Display(Name = "Raza")]
        [Required(ErrorMessage = "La raza es requerida")]
        public string TC_Raza { get; set; } = null!;

        [Display(Name = "Peso (kg)")]
        [Required(ErrorMessage = "El peso es requerido")]
        public decimal TN_Peso { get; set; }

        [Display(Name = "Color")]
        [Required(ErrorMessage = "El color es requerido")]
        public string TC_Color { get; set; } = null!;

        public string? TC_NomMascota { get; set; }
    }

    // ── CITA ─────────────────────────────────────────────────
    public class mCita
    {
        public int TN_IdCita { get; set; }

        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "El cliente es requerido")]
        public int TN_IdCliente { get; set; }

        [Display(Name = "Mascota")]
        [Required(ErrorMessage = "La mascota es requerida")]
        public int TN_IdMascota { get; set; }

        [Display(Name = "Fecha de cita")]
        [Required(ErrorMessage = "La fecha es requerida")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime TF_FecCita { get; set; }

        public string? TC_NombreCliente { get; set; }
        public string? TC_NomMascota { get; set; }
    }

    // ── DIAGNÓSTICO ──────────────────────────────────────────
    public class mDiagnostico
    {
        public int TN_IdDiagnostico { get; set; }

        [Display(Name = "Cita")]
        [Required(ErrorMessage = "La cita es requerida")]
        public int TN_IdCita { get; set; }

        [Display(Name = "Diagnóstico")]
        [Required(ErrorMessage = "El diagnóstico es requerido")]
        [MinLength(5, ErrorMessage = "Mínimo 5 caracteres")]
        [MaxLength(500, ErrorMessage = "Máximo 500 caracteres")]
        public string TC_DscDiagnostico { get; set; } = null!;

        public DateTime? TF_FecCita { get; set; }
        public string? TC_NomMascota { get; set; }
    }
}
