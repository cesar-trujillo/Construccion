using Entidades;
using LogicaNegocio;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ClubNaval
{
    public partial class FRMActualizarPersona : Form
    {
        private VOPersona personaOriginal;
        private string rutaFoto;

        public FRMActualizarPersona(VOPersona persona)
        {
            InitializeComponent();
            this.personaOriginal = persona;
            this.Load += FRMActualizarPersona_Load;
            this.btnSeleccionar.Click += btnSeleccionar_Click;
            this.btnGuardar.Click += btnGuardar_Click;
        }

        private void FRMActualizarPersona_Load(object sender, EventArgs e)
        {
            Enumeradores.EnumToListBox(typeof(Enumeradores.CargoPersona), cbxCargo, false);

            txtIdPersona.Text = personaOriginal.IdPersona.ToString();
            txtNombre.Text = personaOriginal.Nombre;
            txtTelefono.Text = personaOriginal.Telefono;
            txtDireccion.Text = personaOriginal.Direccion;
            txtCorreo.Text = personaOriginal.Correo;

            if (personaOriginal.Cargo.HasValue)
            {
                cbxCargo.SelectedItem = (Enumeradores.CargoPersona)personaOriginal.Cargo.Value;
            }

            if (!string.IsNullOrEmpty(personaOriginal.UrlFoto))
            {
                try
                {
                    pictureBox1.Image = Image.FromFile(personaOriginal.UrlFoto);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    rutaFoto = personaOriginal.UrlFoto;
                }
                catch { }
            }
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

            VOPersona persona = new VOPersona(
                personaOriginal.IdPersona,
                txtTelefono.Text,
                txtDireccion.Text,
                txtNombre.Text,
                txtCorreo.Text,
                (int)(Enumeradores.CargoPersona)cbxCargo.SelectedItem,
                personaOriginal.Disponibilidad,
                rutaFoto ?? ""
            );

            try
            {
                BLLPersona.Actualizar(persona);
                MessageBox.Show("Persona actualizada correctamente");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo ocurrió: " + ex.Message);
            }
        }
    }
}
