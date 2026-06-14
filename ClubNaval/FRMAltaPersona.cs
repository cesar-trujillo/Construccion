using Entidades;
using LogicaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClubNaval
{
    public partial class FRMAltaPersona : Form
    {
        private string rutaFoto;

        public FRMAltaPersona()
        {
            InitializeComponent();
            this.Load += FRMAltaPersona_Load;
            this.btnSeleccionar.Click += btnSeleccionar_Click;
        }

        private void FRMAltaPersona_Load(object sender, EventArgs e)
        {
            Enumeradores.EnumToListBox(typeof(Enumeradores.CargoPersona), cbxCargo, false);
            cbxCargo.SelectedIndex = -1;
            cbxCargo.Text = "(Seleccione un cargo)";
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            OFD.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            OFD.Title = "Seleccionar foto";
            OFD.CheckFileExists = true;
            OFD.CheckPathExists = true;

            if (OFD.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(OFD.FileName);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                rutaFoto = OFD.FileName;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cbxCargo.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un cargo");
                return;
            }

            VOPersona persona = new VOPersona(txtTelefono.Text, txtDireccion.Text, txtNombre.Text, txtCorreo.Text, (int)(Enumeradores.CargoPersona)cbxCargo.SelectedItem, null, rutaFoto ?? "");

            try
            {
                BLLPersona.Insertar(persona);
                MessageBox.Show("Usuario registrado en la base de datos");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo ocurrió: " + ex.Message);
            }
        }
    }
}
