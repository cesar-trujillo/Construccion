using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class DALBarco
    {
        public static bool Insertar(VOBarco barco)
        {
            bool resultado = false;
            try
            {
                List<Parametros> parametros = new List<Parametros>();
                parametros.Add(new Parametros("@Matricula", SqlDbType.VarChar, barco.Matricula));
                parametros.Add(new Parametros("@NoAmarre", SqlDbType.VarChar, barco.NoAmarre));
                parametros.Add(new Parametros("@Nombre", SqlDbType.VarChar, barco.Nombre));
                parametros.Add(new Parametros("@Cuota", SqlDbType.Decimal, barco.Cuota));
                parametros.Add(new Parametros("@IdOwner", SqlDbType.Int, barco.IdPersona));
                parametros.Add(new Parametros("@UrlFoto", SqlDbType.VarChar, barco.UrlFoto));
                int rows = Consultas.EjecuciónSinConsulta("SP_InsertarBarco", parametros);
                if (rows == 1)
                    resultado = true;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("No se pudo insertar en la base de datos");
            }
            return resultado;
        }
        public static bool Actualizar(VOBarco barco)
        {
            int rows = 0;
            try
            {
                List<Parametros> parametros = new List<Parametros>();
                parametros.Add(new Parametros("@IdBarco", SqlDbType.Int, barco.IdBarco));
                parametros.Add(new Parametros("@Matricula", SqlDbType.VarChar, barco.Matricula));
                parametros.Add(new Parametros("@NoAmarre", SqlDbType.VarChar, barco.NoAmarre));
                parametros.Add(new Parametros("@Nombre", SqlDbType.VarChar, barco.Nombre));
                parametros.Add(new Parametros("@Cuota", SqlDbType.Decimal, barco.Cuota));
                parametros.Add(new Parametros("@IdOwner", SqlDbType.Int, barco.IdPersona));
                parametros.Add(new Parametros("@UrlFoto", SqlDbType.VarChar, barco.UrlFoto));
                parametros.Add(new Parametros("@Disponibilidad", SqlDbType.Bit, barco.Disponibilidad));
                rows = Consultas.EjecuciónSinConsulta("SP_ActualizarBarco", parametros);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("No se pudo actualizar en la base de datos");
            }
            if (rows == 1)
                return true;
            else
                return false;
        }
        public static bool Eliminar(int idBarco)
        {
            int rows = 0;
            try
            {
                List<Parametros> parametros = new List<Parametros>();
                parametros.Add(new Parametros("@IdBarco", SqlDbType.Int, idBarco));
                rows = Consultas.EjecuciónSinConsulta("SP_EliminarBarco", parametros);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("No se pudo eliminar en la base de datos");
            }
            if (rows == 1)
                return true;
            else
                return false;
        }
        public static VOBarco ConsultarBarco(int idBarco)
        {
            VOBarco barco = null;
            try
            {
                List<Parametros> parametros = new List<Parametros>();
                parametros.Add(new Parametros("@IdBarco", SqlDbType.Int, idBarco));
                List<object> datos = Consultas.EjecucionConLectura("SP_ConsultarBarcoPorId", parametros);
                barco = new VOBarco(datos);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error al consultar en la base de datos");
            }
            return barco;
        }
        public static List<VOBarco> ConsultarBarcos(bool? disponibilidad)
        {
            List<VOBarco> lista = new List<VOBarco>();
            try
            {
                List<Parametros> parametros = new List<Parametros>();
                parametros.Add(new Parametros("@Disponibilidad", SqlDbType.Bit, disponibilidad));
                DataTable barcos = Consultas.EjecucionConLlenado("SP_ConsultarBarcos", parametros);
                foreach (DataRow registro in barcos.Rows)
                {
                    lista.Add(new VOBarco(registro));
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error al consultar en la base de datos");
            }
            return lista;
        }
        public static List<VOBarco> ConsultarBarcosPorOwner(int idOwner, bool? disponibilidad)
        {
            List<VOBarco> lista = new List<VOBarco>();
            try
            {
                List<Parametros> parametros = new List<Parametros>();
                parametros.Add(new Parametros("@IdOwner", SqlDbType.Int, idOwner));
                parametros.Add(new Parametros("@Disponibilidad", SqlDbType.Bit, disponibilidad));
                DataTable barcos = Consultas.EjecucionConLlenado("SP_ConsultarBarcosPorOwner", parametros);
                foreach (DataRow registro in barcos.Rows)
                {
                    lista.Add(new VOBarco(registro));
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error al consultar en la base de datos");
            }
            return lista;
        }
    }
}
