using Microsoft.AspNetCore.Mvc;

namespace ApiRestBancoTest.Models
{
    public class Persona 
    {
        public string strNombre { get; set; }
        public string strGenero { get; set; }
        public int intEdad { get; set; }
        public int  intIdentificacion { get; set;}
        public string strDireccion { get; set; }
        public int intelefono { get; set;}

    }
}
