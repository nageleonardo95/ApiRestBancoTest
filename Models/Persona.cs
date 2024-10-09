using Microsoft.AspNetCore.Mvc;

namespace ApiRestBancoTest.Models
{
    public class Persona 
    {
        public string Nombres { get; set; }
        public string? Genero { get; set; }
        public int? Edad { get; set; }
        public int?  Identificacion { get; set;}
        public string Direccion { get; set; }
        public string? Telefono { get; set;}

    }
}
