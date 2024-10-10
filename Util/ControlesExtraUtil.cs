using ApiRestBancoTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiRestBancoTest.Util
{
    public class ControlesExtraUtil 
    {
        public static string Control_Data(dtoMovimientoId movimiento)
        {
            string result = string.Empty;


            if (!(movimiento.TipoMovimiento == "Retiro" || movimiento.TipoMovimiento == "Deposito"))
            {
                result = "Error de parámetros: 'tipoMovimiento' debe ser 'Retiro' o 'Deposito'";
            }

            if (!decimal.TryParse(movimiento.Valor.ToString(), out decimal valor))
            {
                result = "Error de parámetros: 'Valor' debe ser un numero";
            } else if (movimiento.Valor == 0)
            {
                result = "Error de parámetros: 'Valor' debe ser distinto a 0";

            }

            if (movimiento.intCuentaId <= 0)
            {
                result = "Error de parámetros: 'intCuentaId' debe ser un número positivo válido";
            }

            return result;
        }

        public static string Control_Data_Cuenta(int idCliente, string InicioMes, string FinMes)
        {
            string result = string.Empty;

            if (!DateTime.TryParse(InicioMes, out DateTime inicio) || !DateTime.TryParse(FinMes, out DateTime fin))
            {
                result = "El formato de las fechas es incorrecto. Use el formato 'YYYY-MM-DD'";
            }

            return result;
        }

        public static string Control_Data_Cuenta_Generar(dtoCuentaIn dtoCuentaIn)
        {
            string result = string.Empty;

            if (!(dtoCuentaIn.TipoCuenta == "Ahorro" || dtoCuentaIn.TipoCuenta == "Corriente"))
            {
                result = "Error de parámetros: 'TipoCuenta' debe ser 'Ahorro' o 'Corriente'";
            }
            return result;
        }
    }
}
