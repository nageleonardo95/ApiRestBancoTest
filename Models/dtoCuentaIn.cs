using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiRestBancoTest.Models
{
    public class dtoCuentaIn 
    {
        public string TipoCuenta { get; set; }

        public int intClienteId { get; set; }

    }
}
