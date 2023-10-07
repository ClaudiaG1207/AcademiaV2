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
    public  class DIdioma
    {
        CD_Conexion Conect = new CD_Conexion();
        public void Insertaridioma(int idnivelcurso, string Idioma, double costo)
        {
            Conect.Abrir();
            SqlCommand AggUser = new SqlCommand("AgregarIdiomas",CD_Conexion.conectar);
            AggUser.CommandType = CommandType.StoredProcedure;
            AggUser.Parameters.AddWithValue("@IdNivelCurso", idnivelcurso);
            AggUser.Parameters.AddWithValue("@Idioma", Idioma);    
            AggUser.Parameters.AddWithValue("@Costo", costo);
            AggUser.ExecuteNonQuery();
            Conect.cerrar();
        }
        public void MostrarNivelCurso(ComboBox combo)
        {
            try
            {
                //Crear la colección para el autocompletado
                AutoCompleteStringCollection lista = new AutoCompleteStringCollection();

                //Abrir la conexión a la base de datos
                Conect.Abrir();

                //Crear el comando SQL para el procedimiento almacenado
                SqlCommand da = new SqlCommand("MostrarNivelCurso", CD_Conexion.conectar);
                da.CommandType = CommandType.StoredProcedure;

                //Crear el adapatador para ejecuatr el comando y llenar los datos en el DataTable
                SqlDataAdapter cb = new SqlDataAdapter(da);
                DataTable dt = new DataTable();
                cb.Fill(dt);

                //Configurar el combobox para mostrar los datos
                combo.ValueMember = "IDNivelCurso";
                combo.DisplayMember = "NivelCurso";
                combo.DataSource = dt;

                //Agregar los datos a la colección de autocompletado
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lista.Add(dt.Rows[i]["NivelCurso"].ToString());
                }

                //Asignar la colección de autocompletado al combobox
                combo.AutoCompleteCustomSource = lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Cerrar la conexión a la base de datos
                Conect.cerrar();
                //cambio
            }
        }
        public void MostarIdioma(ref DataTable dt)
        {
            try
            {
                Conect.Abrir();
                SqlDataAdapter da = new SqlDataAdapter("ListarIdiomas", CD_Conexion.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
            finally { Conect.cerrar(); }
        }
        public void editarIdioma(int idIdioma, string NomIdioma, double costo)
        {
            try
            {
                Conect.Abrir();
                SqlCommand cmd = new SqlCommand("EditarIdioma", CD_Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdIdioma", idIdioma);
                cmd.Parameters.AddWithValue("@NomIdioma", NomIdioma);
                cmd.Parameters.AddWithValue("@Costo", costo);
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
        public void EliminarIdioma(int id)
        {
            try
            {
                Conect.Abrir();
                SqlCommand cmd = new SqlCommand("EliminarIdioma", CD_Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idIdioma", id);
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
