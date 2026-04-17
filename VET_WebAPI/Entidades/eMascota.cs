using System.ComponentModel.DataAnnotations;

namespace VET_WebAPI.Entidades
{
    // Representa la tabla TVET_MASCOTAS
    public class eMascota
    {
        public int TN_IdMascota { get; set; }

        [Required(ErrorMessage = "El nombre de la mascota es requerido")]
        [MaxLength(100)]
        public string TC_NomMascota { get; set; } = null!;

        [Required(ErrorMessage = "El cliente es requerido")]
        public int TN_IdCliente { get; set; }

        // Solo lectura: nombre del dueño para mostrar en listados
        public string? TC_NombreCliente { get; set; }
    }
}
