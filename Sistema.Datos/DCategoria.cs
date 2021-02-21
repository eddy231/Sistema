using System;
using System.Data;
using Sistema.Entidades;
using System.Data.SqlClient;

namespace Sistema.Datos
{
    public class DCategoria
    {
        public DataTable Listar()
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection con = new SqlConnection();
            try
            {
                con = Conexion.getInstancia().crearConexion();
                SqlCommand comando = new SqlCommand("sp_categoria_listar", con);
                comando.CommandType = CommandType.StoredProcedure;
                con.Open();
                Resultado = comando.ExecuteReader();
                Tabla.Load(Resultado);
                return Tabla;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        public DataTable Bucar(string valor)
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection con = new SqlConnection();
            try
            {
                con = Conexion.getInstancia().crearConexion();
                SqlCommand comando = new SqlCommand("sp_categoria_buscar", con);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@valor", SqlDbType.VarChar).Value = valor;
                con.Open();
                Resultado = comando.ExecuteReader();
                Tabla.Load(Resultado);
                return Tabla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        public string Existe(string valor)
        {
            string respuesta = "";
            SqlConnection con = new SqlConnection();
            try
            {
                con = Conexion.getInstancia().crearConexion();
                SqlCommand comando = new SqlCommand("sp_categoria_existe", con);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@valor", SqlDbType.VarChar).Value = valor;
                SqlParameter parExiste = new SqlParameter();
                parExiste.ParameterName = "@existe";
                parExiste.SqlDbType = SqlDbType.Int;
                parExiste.Direction = ParameterDirection.Output;
                comando.Parameters.Add(parExiste);
                con.Open();
                comando.ExecuteNonQuery();
                respuesta = Convert.ToString(parExiste.Value);
            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            return respuesta;
        }

        public string Insertar(Categoria obj)
        {
            string respuesta = "";
            SqlConnection con = new SqlConnection();
            try
            {
                con = Conexion.getInstancia().crearConexion();
                SqlCommand comando = new SqlCommand("sp_categoria_insertar", con);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value = obj.Nombre;
                comando.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = obj.Descripcion;
                con.Open();
                respuesta = comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo ingresar el registro correctamente";
            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            return respuesta;
        }

        public string Actualizar(Categoria obj)
        {
            string respuesta = "";
            SqlConnection con = new SqlConnection();
            try
            {
                con = Conexion.getInstancia().crearConexion();
                SqlCommand comando = new SqlCommand("sp_categoria_actualizar", con);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@idcategoria", SqlDbType.Int).Value = obj.IdCategoria;
                comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value = obj.Nombre;
                comando.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = obj.Descripcion;
                con.Open();
                respuesta = comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo actualizar el registro correctamente";
            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            return respuesta;
        }

        public string Activar(int id)
        {
            string respuesta = "";
            SqlConnection con = new SqlConnection();
            try
            {
                con = Conexion.getInstancia().crearConexion();
                SqlCommand comando = new SqlCommand("sp_categoria_activar", con);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@idcategoria", SqlDbType.Int).Value = id;
                con.Open();
                respuesta = comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo activar el registro correctamente";
            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            return respuesta;
        }

        public string Descativar(int id)
        {
            string respuesta = "";
            SqlConnection con = new SqlConnection();
            try
            {
                con = Conexion.getInstancia().crearConexion();
                SqlCommand comando = new SqlCommand("sp_categoria_desactivar", con);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@idcategoria", SqlDbType.Int).Value = id;
                con.Open();
                respuesta = comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo desactivar el registro correctamente";
            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            return respuesta;
        }

        public string Eliminar(int id)
        {
            string respuesta = "";
            SqlConnection con = new SqlConnection();
            try
            {
                con = Conexion.getInstancia().crearConexion();
                SqlCommand comando = new SqlCommand("sp_categoria_eliminar", con);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@idcategoria", SqlDbType.Int).Value = id;
                con.Open();
                respuesta = comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo eliminar el registro correctamente";
            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            return respuesta;
        }
    }
}
