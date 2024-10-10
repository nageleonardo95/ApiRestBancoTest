using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ApiRestBancoTest.Models
{
    public class Persona 
    {
        [Required]
        [MaxLength(100)]
        public string Nombres { get; set; }
        [MaxLength(10)]
        public string? Genero { get; set; }
        public int? Edad { get; set; }
        public int?  Identificacion { get; set;}
        [MaxLength(200)]
        public string Direccion { get; set; }
        [MaxLength(20)]
        public string? Telefono { get; set;}

    }
}
