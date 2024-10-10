using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiRestBancoTest.Models
{
    public class Cliente : Persona
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int intClienteId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Contrasena { get; set;}
       
        public bool Estado { get; set; }


    }
}
