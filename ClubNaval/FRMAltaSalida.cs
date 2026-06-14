using Entidades;
using LogicaNegocio;
using System;
using System.Windows.Forms;

namespace ClubNaval
{
    public partial class FRMAltaSalida : Form
    {
        public FRMAltaSalida()
        {
            InitializeComponent();
            this.Load += FRMAltaSalida_Load;
        }

        private void FRMAltaSalida_Load(object sender, EventArgs e)
        {
            txtFechaHora.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            var barcos = BLLBarco.ConsultarBarcos(null);
            cbxIdBarco.DataSource = barcos;
            cbxIdBarco.DisplayMember = "Nombre";
            cbxIdBarco.ValueMember = "IdBarco";
            cbxIdBarco.SelectedIndex = -1;

            var personas = BLLPersona.ConsultarPersonas(null);
            cbxIdCapitan.DataSource = personas;
            cbxIdCapitan.DisplayMember = "Nombre";
            cbxIdCapitan.ValueMember = "IdPersona";
            cbxIdCapitan.SelectedIndex = -1;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cbxIdBarco.SelectedItem == null || cbxIdCapitan.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un barco y un capitán");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDestino.Text))
            {
                MessageBox.Show("Debe ingresar un destino");
                return;
            }

            VOSalida salida = new VOSalida(
                DateTime.Parse(txtFechaHora.Text),
                txtDestino.Text,
                "EN_PROCESO",
                (int)cbxIdBarco.SelectedValue,
                (int)cbxIdCapitan.SelectedValue
            );

            try
            {
                BLLSalida.InsertarSalida(salida);
                MessageBox.Show("Salida registrada correctamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo ocurrió: " + ex.Message);
            }
        }
    }
}
