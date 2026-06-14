using Entidades;
using LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ClubNaval
{
    public partial class FRMConsultaSalidas : Form
    {
        public FRMConsultaSalidas()
        {
            InitializeComponent();
            this.Load += FRMConsultaSalidas_Load;
            this.btnBuscar.Click += btnBuscar_Click;
            this.btnFinalizar.Click += btnFinalizar_Click;
            this.btnRefrescar.Click += btnRefrescar_Click;
            this.dgvSalidas.SelectionChanged += dgvSalidas_SelectionChanged;
        }

        private void FRMConsultaSalidas_Load(object sender, EventArgs e)
        {
            cbxEstadoFiltro.Items.Clear();
            cbxEstadoFiltro.Items.Add("Todos");
            cbxEstadoFiltro.Items.Add("EN_PROCESO");
            cbxEstadoFiltro.Items.Add("FINALIZADA");
            cbxEstadoFiltro.SelectedIndex = 0;

            CargarSalidas(null);
        }

        private void CargarSalidas(string estado)
        {
            List<VOSalidaExtendida> lista;

            if (estado == null)
            {
                lista = new List<VOSalidaExtendida>();
                lista.AddRange(BLLSalida.ConsultarSalidaPorEstadoExtendida("EN_PROCESO"));
                lista.AddRange(BLLSalida.ConsultarSalidaPorEstadoExtendida("FINALIZADA"));
            }
            else
            {
                lista = BLLSalida.ConsultarSalidaPorEstadoExtendida(estado);
            }

            dgvSalidas.DataSource = null;
            dgvSalidas.DataSource = lista.Select(s => new
            {
                s.IdSalida,
                Fecha = s.FechaHoraSalida.ToString("yyyy-MM-dd HH:mm"),
                s.Destino,
                s.Estado,
                Barco = s.NombreBarco,
                Capitan = s.NombreCapitan
            }).ToList();

            dgvSalidas.Columns["IdSalida"].HeaderText = "ID";
            dgvSalidas.Columns["Fecha"].HeaderText = "Fecha y Hora";
            dgvSalidas.Columns["Destino"].HeaderText = "Destino";
            dgvSalidas.Columns["Estado"].HeaderText = "Estado";
            dgvSalidas.Columns["Barco"].HeaderText = "Barco";
            dgvSalidas.Columns["Capitan"].HeaderText = "Capitán";
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string estado = cbxEstadoFiltro.SelectedItem.ToString();
            if (estado == "Todos")
                CargarSalidas(null);
            else
                CargarSalidas(estado);
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            cbxEstadoFiltro.SelectedIndex = 0;
            CargarSalidas(null);
        }

        private void dgvSalidas_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSalidas.SelectedRows.Count > 0)
            {
                string estado = dgvSalidas.SelectedRows[0].Cells["Estado"].Value?.ToString();
                btnFinalizar.Enabled = (estado == "EN_PROCESO");
            }
            else
            {
                btnFinalizar.Enabled = false;
            }
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            if (dgvSalidas.SelectedRows.Count == 0) return;

            int idSalida = (int)dgvSalidas.SelectedRows[0].Cells["IdSalida"].Value;

            DialogResult result = MessageBox.Show(
                "¿Finalizar esta salida?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    BLLSalida.FinalizarSalida(idSalida.ToString());
                    MessageBox.Show("Salida finalizada correctamente");
                    CargarSalidas(cbxEstadoFiltro.SelectedItem.ToString() == "Todos" ? null : cbxEstadoFiltro.SelectedItem.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al finalizar: " + ex.Message);
                }
            }
        }
    }
}
