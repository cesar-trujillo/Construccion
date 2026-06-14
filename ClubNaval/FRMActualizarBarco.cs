using Entidades;
using LogicaNegocio;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ClubNaval
{
    public partial class FRMActualizarBarco : Form
    {
        private VOBarco barcoOriginal;
        private string rutaFoto;

        public FRMActualizarBarco(VOBarco barco)
        {
            InitializeComponent();
            this.barcoOriginal = barco;
            this.Load += FRMActualizarBarco_Load;
            this.btnSeleccionar.Click += btnSeleccionar_Click;
        }

        private void FRMActualizarBarco_Load(object sender, EventArgs e)
        {
            var personas = BLLPersona.ConsultarPersonas(null);
            cbxIdOwner.DataSource = personas;
            cbxIdOwner.DisplayMember = "Nombre";
            cbxIdOwner.ValueMember = "IdPersona";

            txtIdBarco.Text = barcoOriginal.IdBarco.ToString();
            txtMatricula.Text = barcoOriginal.Matricula;
            txtNoAmarre.Text = barcoOriginal.NoAmarre;
            txtNombre.Text = barcoOriginal.Nombre;
            txtCuota.Text = barcoOriginal.Cuota?.ToString() ?? "";

            if (barcoOriginal.IdPersona.HasValue)
            {
                foreach (var item in cbxIdOwner.Items)
                {
                    if (item is VOPersona p && p.IdPersona == barcoOriginal.IdPersona.Value)
                    {
                        cbxIdOwner.SelectedItem = item;
                        break;
                    }
                }
            }

            if (!string.IsNullOrEmpty(barcoOriginal.UrlFoto))
            {
                try
                {
                    pictureBox1.Image = Image.FromFile(barcoOriginal.UrlFoto);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    rutaFoto = barcoOriginal.UrlFoto;
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
                barcoOriginal.IdBarco,
                txtMatricula.Text,
                txtNoAmarre.Text,
                txtNombre.Text,
                cuota,
                (int)((VOPersona)cbxIdOwner.SelectedItem).IdPersona,
                rutaFoto ?? "",
                barcoOriginal.Disponibilidad
            );

            try
            {
                BLLBarco.Actualizar(barco);
                MessageBox.Show("Barco actualizado correctamente");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo ocurrió: " + ex.Message);
            }
        }
    }
}
