using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace API_NASAT.Models
{
    [Index(nameof(Username), IsUnique = true)]
    public class DbUsers
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Se necesita el Nombre de Usuario")]
        [StringLength(100)]


        public string Fullname { get; set; }
        [Required(ErrorMessage = "Se necesita Email de Usuario")]
        [StringLength(50)]

        public string Username { get; set; }
        [StringLength(25)]
        public string Dataset { get; set; }
        [Required(ErrorMessage = "Se necesita el tipo de Usuario")]
        [StringLength(13)]
        public string Typeuser { get; set; }
        [Required(ErrorMessage = "Se necesita una contraseña")]
        [StringLength(100)]
        public string Password { get; set; }

    }
}
