using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
using Entidades;

namespace LogicaNegocio
{
    public class BLLBarco
    {
        public static bool Insertar(VOBarco barco)
        {
            try
            {
                return DALBarco.Insertar(barco);
            }
            catch(Exception ex)
            {
                throw new ArgumentException("Ocurrio un error. "+ex.Message);
            }
        }
        public static bool Actualizar(VOBarco barco)
        {
            try
            {
                return DALBarco.Actualizar(barco);
            }
            catch(Exception ex)
            {
                throw new ArgumentException("Ocurrio un error. " + ex.Message);
            }
        }
        public static bool Eliminar(string idBarco)
        {
            try
            {
                return DALBarco.Eliminar(int.Parse(idBarco));
            }
            catch(Exception ex)
            {
                throw new ArgumentException("Ocurrio un error. " + ex.Message);
            }
        }
        public static VOBarco ConsultarBarco(string idBarco)
        {
            VOBarco barco = null;
            try
            {
                barco = DALBarco.ConsultarBarco(int.Parse(idBarco));
                if (barco.Equals(null))
                    throw new ArgumentException("El id buscado no existe en la base de datos");
            }
            catch(Exception ex)
            {
                throw new ArgumentException("Ocurrio un error. " + ex.Message);
            }
            return barco;
        }
        public static List<VOBarco> ConsultarBarcos(bool? disponibilidad)
        {
            List<VOBarco> barcos = null;
            try
            {
                barcos = DALBarco.ConsultarBarcos(disponibilidad);
            }
            catch(Exception ex)
            {
                throw new ArgumentException("Ocurrio un error. " + ex.Message);
            }
            return barcos;
        }
        public static List<VOBarco> ConsultarBarcosPorOwner(string idOwner, bool? disponibilidad)
        {
            List<VOBarco> barcos = null;
            try
            {
                barcos = DALBarco.ConsultarBarcosPorOwner(int.Parse(idOwner), disponibilidad);
            }
            catch(Exception ex)
            {
                throw new ArgumentException("Ocurrio un error. "+ex.Message);
            }
            return barcos;
        }
    }
}
