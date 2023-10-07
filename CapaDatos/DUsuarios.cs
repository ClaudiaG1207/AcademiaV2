using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaDatos
{
    public class DUsuarios
    {
        CD_Conexion Conect = new CD_Conexion();
        public void Insertar(string Usuario, string Password)
        {
            Conect.Abrir();
            SqlCommand AggUser = new SqlCommand("Agregar_Usuario", CD_Conexion.conectar);
            AggUser.CommandType = CommandType.StoredProcedure;
            AggUser.Parameters.AddWithValue("@Usuario",Usuario);
            AggUser.Parameters.AddWithValue("@Clave", Password);
            AggUser.ExecuteNonQuery();
            Conect.cerrar();
        }
        public void MostarUsuarios(ref DataTable dt)
        {
            try
            {
                Conect.Abrir();
                SqlDataAdapter da = new SqlDataAdapter("ListarUsuarios", CD_Conexion.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
            finally { Conect.cerrar(); }
        }
        public string Login(string User)
        {
            string ContraseñaHash = "";
            Conect.Abrir();
            SqlCommand SearchUser = new SqlCommand("BuscarUsuario", CD_Conexion.conectar)
            {
                CommandType = CommandType.StoredProcedure
            };
            SearchUser.Parameters.AddWithValue("@Buscador", User);
            SqlDataReader LeerDatos = SearchUser.ExecuteReader();
            while (LeerDatos.Read())
            {
                ContraseñaHash = (string)LeerDatos["Clave"];
            }
            Conect.cerrar();
            return ContraseñaHash;
        }
        public void editarUsuario(int idUS, string us, string clave )
        {
            try
            {
                Conect.Abrir();
                SqlCommand cmd = new SqlCommand("EditarUsuario", CD_Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdUsuario", idUS);
                cmd.Parameters.AddWithValue("@Usuario", us);
                cmd.Parameters.AddWithValue("@Clave", clave);
                cmd.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                Conect.cerrar();
            }
        }
        public void EliminarUsuario(int id)
        {
            try
            {
                Conect.Abrir();
                SqlCommand cmd = new SqlCommand("EliminarUsuario", CD_Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdUsuario", id);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);

            }
            finally
            {
                Conect.cerrar();
            }
        }
    }
}
