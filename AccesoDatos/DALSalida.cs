using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class DALSalida
    {
        public static bool InsertarSalida(VOSalida salida)
        {
            int rows = 0;
            try
            {
                List<Parametros> parametros = new List<Parametros>();
                parametros.Add(new Parametros("@FechaHoraSalida", SqlDbType.DateTime, salida.FechaHoraSalida));
                parametros.Add(new Parametros("@Destino", SqlDbType.VarChar, salida.Destino));
                parametros.Add(new Parametros("@Estado", SqlDbType.VarChar, salida.Estado));
                parametros.Add(new Parametros("@IdBarco", SqlDbType.Int, salida.IdBarco));
                parametros.Add(new Parametros("@IdCapitan", SqlDbType.Int, salida.IdCapitan));
                rows = Consultas.EjecuciónSinConsulta("SP_InsertarSalida", parametros);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("No se pudo insertar el dato en la base de datos " + ex.Message);
            }
            if (rows == 1)
                return true;
            else
                return false;
        }
        public static bool FinalizarSalida(int idSalida, string estado)
        {
            int rows = 0;
            try
            {
                List<Parametros> parametros = new List<Parametros>();
                parametros.Add(new Parametros("@IdSalida", SqlDbType.Int, idSalida));
                parametros.Add(new Parametros("@Estado", SqlDbType.VarChar, estado));
                rows = Consultas.EjecuciónSinConsulta("SP_FinalizarSalida", parametros);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error al finalizar el registro de salida: " + ex.Message);
            }
            if (rows != 0)
                return true;
            else
                return false;
        }
        public static List<VOSalida> ConsultarSalidaPorEstado(string estado)
        {
            List<VOSalida> lista = new List<VOSalida>();
            try
            {
                List<Parametros> parametros = new List<Parametros>();
                parametros.Add(new Parametros("@Estado", SqlDbType.VarChar, estado));
                DataTable resultados = Consultas.EjecucionConLlenado("SP_ConsultarSalidasPorEstado", parametros);
                foreach (DataRow registro in resultados.Rows)
                {
                    lista.Add(new VOSalida(registro));
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error al consultar el registro de salida: " + ex.Message);
            }
        }
        public static List<VOSalidaExtendida> ConsultarSalidaPorEstadoExtendida(string estado)
        {
            List<VOSalidaExtendida> lista = new List<VOSalidaExtendida>();
            try
            {
                List<Parametros> parametros = new List<Parametros>();
                parametros.Add(new Parametros("@Estado", SqlDbType.VarChar, estado));
                DataTable resultados = Consultas.EjecucionConLlenado("SP_ConsultarSalidasPorEstadoExtendida", parametros);
                foreach (DataRow registro in resultados.Rows)
                {
                    lista.Add(new VOSalidaExtendida(registro));
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error al consultar el registro de salida: " + ex.Message);
            }
        }
        public static VOSalidaExtendida ConsultarSalidaPorIdExtendida(int idSalida)
        {
            try
            {
                List<Parametros> parametros = new List<Parametros>();
                parametros.Add(new Parametros("@IdSalida", SqlDbType.Int, idSalida));
                DataTable resultados = Consultas.EjecucionConLlenado("SP_ConsultarSalidasPorIdExtendida", parametros);
                return new VOSalidaExtendida(resultados.Rows[0]);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error al consultar el registro de salida: " + ex.Message);
            }
        }
        public static VOSalida ConsultarSalidaPorId(int idSalida)
        {
            try
            {
                List<Parametros> parametros = new List<Parametros>();
                parametros.Add(new Parametros("@IdSalida", SqlDbType.Int, idSalida));
                DataTable resultados = Consultas.EjecucionConLlenado("SP_ConsultarSalidasPorId", parametros);
                return new VOSalida(resultados.Rows[0]);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error al consultar el registro de salida: " + ex.Message);
            }
        }
    }
}
