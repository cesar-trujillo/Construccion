using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public struct Parametros
    {
        private string nombre;
        private SqlDbType tipo;
        private object valor;

        public Parametros(string nombre, SqlDbType tipo, object valor)
        {
            this.nombre = nombre;
            this.tipo = tipo;
            this.valor = valor;
        }

        public string Nombre { get => nombre; set => nombre = value; }
        public SqlDbType Tipo { get => tipo; set => tipo = value; }
        public object Valor { get => valor; set => valor = value; }
    }

    public class Consultas
    {
        public static int EjecuciónSinConsulta(string spNombre, List<Parametros> parametros)
        {
            int rows = 0;
            Conexion conexion = new Conexion();
            SqlConnection cnn = new SqlConnection(conexion.CadenaConexion);
            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand(spNombre, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (Parametros parametro in parametros)
                {
                    cmd.Parameters.Add(parametro.Nombre, parametro.Tipo).Value = parametro.Valor;
                }
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error al ejecutar la consulta");
            }
            finally
            {
                cnn.Close();
            }
            return rows;
        }
        public static List<object> EjecucionConLectura(string spNombre, List<Parametros> parametros)
        {
            Conexion conexion = new Conexion();
            SqlConnection cnn = new SqlConnection(conexion.CadenaConexion);
            SqlDataReader datos;
            List<object> registro = new List<object>();
            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand(spNombre, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (Parametros parametro in parametros)
                {
                    cmd.Parameters.Add(parametro.Nombre, parametro.Tipo).Value = parametro.Valor;
                }
                datos = cmd.ExecuteReader();
                while (datos.Read())
                {
                    for (int i = 0; i < datos.FieldCount; i++)
                    {
                        registro.Add(datos.GetValue(i));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error al ejecutar consulta");
            }
            finally
            {
                cnn.Close();
            }
            return registro;
        }
        public static DataTable EjecucionConLlenado(string spNombre, List<Parametros> parametros)
        {
            Conexion conexion = new Conexion();
            SqlConnection cnn = new SqlConnection(conexion.CadenaConexion);
            DataSet ds = new DataSet();
            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand(spNombre, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (Parametros parametro in parametros)
                {
                    cmd.Parameters.Add(parametro.Nombre, parametro.Tipo).Value = parametro.Valor;
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Resultados");
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error al ejecutar consulta");
            }
            finally
            {
                cnn.Close();
            }
            return ds.Tables["Resultados"];
        }

    }
}
