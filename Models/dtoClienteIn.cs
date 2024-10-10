using System.ComponentModel.DataAnnotations;

namespace ApiRestBancoTest.Models
{
    public class dtoClienteIn
    {
        public string Nombres { get; set; }
        public string? Genero { get; set; }
        public int? Edad { get; set; }
        public int? Identificacion { get; set; }
        public string Direccion { get; set; }
        public string? Telefono { get; set; }
        public string Contrasena { get; set; }
    }
}
