using Microsoft.AspNetCore.Mvc;

namespace ApiRestBancoTest.Models
{
    public class Cliente : Persona
    {
        public int intClienteId { get; set; }
        public string Contrasena { get; set;}
        public bool Estado { get; set; }

    }
}
