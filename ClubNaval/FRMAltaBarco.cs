using Entidades;
using LogicaNegocio;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ClubNaval
{
    public partial class FRMAltaBarco : Form
    {
        private string rutaFoto;

        public FRMAltaBarco()
        {
            InitializeComponent();
            this.Load += FRMAltaBarco_Load;
            this.btnSeleccionar.Click += btnSeleccionar_Click;
        }

        private void FRMAltaBarco_Load(object sender, EventArgs e)
        {
            var personas = BLLPersona.ConsultarPersonas(null);
            cbxIdOwner.DataSource = personas;
            cbxIdOwner.DisplayMember = "Nombre";
            cbxIdOwner.ValueMember = "IdPersona";
            cbxIdOwner.SelectedIndex = -1;
            cbxIdOwner.Text = "(Seleccione un dueño)";
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
            if (cbxIdOwner.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un dueño");
                return;
            }

            if (!double.TryParse(txtCuota.Text, out double cuota))
            {
                MessageBox.Show("Cuota inválida");
                return;
            }

            VOBarco barco = new VOBarco(
                txtMatricula.Text,
                txtNoAmarre.Text,
                txtNombre.Text,
                cuota,
                (int)cbxIdOwner.SelectedValue,
                rutaFoto ?? "",
                null
            );

            try
            {
                BLLBarco.Insertar(barco);
                MessageBox.Show("Barco registrado correctamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo ocurrió: " + ex.Message);
            }
        }
    }
}
