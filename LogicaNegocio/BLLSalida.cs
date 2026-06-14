using AccesoDatos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    public class BLLSalida
    {
        public static bool InsertarSalida(VOSalida salida)
        {
            try
            {
                VOPersona persona = new VOPersona(salida.IdCapitan, null, null, null, null, null, false, null);
                //BLLPersona.Actualizar(parsona);
                VOBarco barco = new VOBarco(salida.IdBarco, null, null, null, null, null, null, false);
                BLLBarco.Actualizar(barco);
                return DALSalida.InsertarSalida(salida);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Ocurrio un error. " + ex.Message);
            }
        }
        public static bool FinalizarSalida(string idSalida)
        {
            try
            {
                return DALSalida.FinalizarSalida(int.Parse(idSalida),
                    Enum.GetName(typeof(EstadoSalida), EstadoSalida.FINALIZADA));
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Ocurrio un error. " + ex.Message);
            }
        }
        public static List<VOSalida> ConsultarSalidaPorEstado(string estado)
        {
            try
            {
                return DALSalida.ConsultarSalidaPorEstado(estado);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Ocurrio un error. " + ex.Message);
            }
        }
        public static List<VOSalidaExtendida> ConsultarSalidaPorEstadoExtendida(string estado)
        {
            try
            {
                return DALSalida.ConsultarSalidaPorEstadoExtendida(estado);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Ocurrio un error. " + ex.Message);
            }
        }
        public static VOSalida ConsultarSalidaPorId(string idSalida)
        {
            try
            {
                return DALSalida.ConsultarSalidaPorId(int.Parse(idSalida));
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Ocurrio un error. " + ex.Message);
            }
        }
        public static VOSalidaExtendida ConsultarSalidaPorIdExtendida(string idSalida)
        {
            try
            {
                return DALSalida.ConsultarSalidaPorIdExtendida(int.Parse(idSalida));
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Ocurrio un error. " + ex.Message);
            }
        }
        public enum EstadoSalida
        {
            EN_PROCESO,
            FINALIZADA
        }
    }
}
