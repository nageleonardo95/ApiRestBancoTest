using Microsoft.AspNetCore.Mvc;

namespace ApiRestBancoTest.Models
{
    public class Cliente : Persona
    {
        public int intClienteId { get; set; }
        public int intContraseña { get; set;}
        public bool blEstado { get; set; }

    }
}
