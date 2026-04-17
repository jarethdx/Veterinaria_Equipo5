using System.ComponentModel.DataAnnotations;

namespace VET_WebAPI.Entidades
{
    // Representa la tabla TVET_CITAS
    public class eCita
    {
        public int TN_IdCita { get; set; }

        [Required(ErrorMessage = "El cliente es requerido")]
        public int TN_IdCliente { get; set; }

        [Required(ErrorMessage = "La mascota es requerida")]
        public int TN_IdMascota { get; set; }

        [Required(ErrorMessage = "La fecha de cita es requerida")]
        public DateTime TF_FecCita { get; set; }

        // Solo lectura: para mostrar en listados
        public string? TC_NombreCliente { get; set; }
        public string? TC_NomMascota { get; set; }
    }
}
