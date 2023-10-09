using CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ProyecAcademiaEuropea
{
    public partial class frmUsuarios : Form
    {
        public frmUsuarios()
        {
            InitializeComponent();
        }
        NUsuarios Usuario= new NUsuarios();

        public string USUARIO;
        public string CLAVE;
        public string FNom;
        public int IdCargo;
        public string FClave;
        int idUsa;
        private void INSERTAR()
        {
            USUARIO= TxtUsuario.Text;
            CLAVE = TxtContra.Text;
            IdCargo=int.Parse(cbCargo.SelectedValue.ToString());
            Usuario.AgregarUsuario(USUARIO, CLAVE, IdCargo);
            TxtContra.Clear();
            TxtUsuario.Clear();
            MostrarUsuarios();
            MessageBox.Show("Se realizo el regitro con exito");
        }
        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                INSERTAR();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        private void ValidarCampos()
        {
            var vr = !string.IsNullOrEmpty(TxtUsuario.Text) && 
            !string.IsNullOrEmpty(TxtContra.Text);
            BtnGuardar.Enabled = vr;
            BtnEditar.Enabled = vr;
        }
        private void Usuarios_Load(object sender, EventArgs e)
        {
            MostrarUsuarios();
            BtnGuardar.Enabled=false;
        }
        private void MostrarUsuarios()
        {
            DataTable dt = new DataTable();
            NUsuarios funcion = new NUsuarios();
            funcion.MostarUsuarios(dt);
            dtUsuarios.DataSource = dt;
            Bases.DiseñoDtv(ref dtUsuarios);
           dtUsuarios.Columns[3].Visible= false;
           dtUsuarios.Columns[4].Visible= false;
            
            
            MostrarCargo();
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void BtnMini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormPrincipal FP = new FormPrincipal();
            FP.Show();
        }
        private void CapturarDatos()
        {

            idUsa = int.Parse(dtUsuarios.SelectedCells[3].Value.ToString());
            TxtUsuario.Text = dtUsuarios.SelectedCells[5].Value.ToString();
            TxtContra.Text = dtUsuarios.SelectedCells[6].Value.ToString();
            IdCargo= int.Parse(dtUsuarios.SelectedCells[4].Value.ToString());
        }
        private void EditarUsuario()

        {

            USUARIO = TxtUsuario.Text;
            CLAVE = TxtContra.Text;
            IdCargo = int.Parse(cbCargo.SelectedValue.ToString());
            TxtContra.Clear();
            TxtUsuario.Clear();
            Usuario.EditarUS(idUsa,USUARIO,CLAVE, IdCargo);
            MostrarUsuarios();
            MessageBox.Show("Se actualizo el regitro con exito", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void EliminarUsuario()
        {
            idUsa= int.Parse(dtUsuarios.SelectedCells[3].Value.ToString());
            Usuario.EliminarUsuario(idUsa);
        }
        private void dtUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dtUsuarios.Columns["Eliminar"].Index)
            {
                DialogResult result = MessageBox.Show("¿Desea eliminar este usuario?", "Eliminando registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    EliminarUsuario();
                    MostrarUsuarios();
                }

            }
            if (e.ColumnIndex == dtUsuarios.Columns["Editar"].Index)
            {
                CapturarDatos();
                BtnEditar.Visible = true;
                BtnGuardar.Visible = false;
            }


        }
        
        private void BtnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                EditarUsuario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void TxtUsuario_TextChanged(object sender, EventArgs e)
        {
            ValidarCampos();
        }
        private void TxtContra_TextChanged(object sender, EventArgs e)
        {
            ValidarCampos();
        }
        private void MostrarCargo()
        {
            
            Usuario.MostarCargos(cbCargo);
        }
    }
}
