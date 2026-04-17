using System.ComponentModel.DataAnnotations;

namespace VET_WebAPI.Entidades
{
    // Representa la tabla TVET_DIAGNOSTICO
    public class eDiagnostico
    {
        public int TN_IdDiagnostico { get; set; }

        [Required(ErrorMessage = "La cita es requerida")]
        public int TN_IdCita { get; set; }

        [Required(ErrorMessage = "La descripción del diagnóstico es requerida")]
        [MaxLength(500)]
        public string TC_DscDiagnostico { get; set; } = null!;

        // Solo lectura: para mostrar en listados
        public DateTime? TF_FecCita { get; set; }
        public string? TC_NomMascota { get; set; }
    }
}
