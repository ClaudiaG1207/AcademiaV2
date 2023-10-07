﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaDatos;

namespace CapaNegocio
{
    public class NIdioma
    {
        DIdioma Idioma = new DIdioma();
        public void AgregarIdioma(int idnivelcurso,string NombreIdioma, double Costo)
        {
            Idioma.Insertaridioma(idnivelcurso ,NombreIdioma, Costo);
        }
        public void EliminarIdioma(int id)
        {
            Idioma.EliminarIdioma(id);
        }
        public void EditarIdioma(int id, string idioma, double costo)
        {
            Idioma.editarIdioma(id, idioma, costo);
        }
        public void MostarIdioma(DataTable dt)
        {
            Idioma.MostarIdioma(ref dt);
        }
        public void MostarNivelCurso(ComboBox combo)
        {
            Idioma.MostrarNivelCurso(combo);
        }
    }
}
