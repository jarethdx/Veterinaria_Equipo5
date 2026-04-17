using System.ComponentModel.DataAnnotations;

namespace VET_WebAPI.Entidades
{
    // Representa la tabla TVET_DETALLEMASCOTA
    public class eDetalleMascota
    {
        [Required]
        public int TN_IdMascota { get; set; }

        [Required(ErrorMessage = "La raza es requerida")]
        [MaxLength(100)]
        public string TC_Raza { get; set; } = null!;

        [Required(ErrorMessage = "El peso es requerido")]
        public decimal TN_Peso { get; set; }

        [Required(ErrorMessage = "El color es requerido")]
        [MaxLength(50)]
        public string TC_Color { get; set; } = null!;

        // Solo lectura: nombre de la mascota para mostrar en listados
        public string? TC_NomMascota { get; set; }
    }
}
