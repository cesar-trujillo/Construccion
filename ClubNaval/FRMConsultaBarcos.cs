using Entidades;
using LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ClubNaval
{
    public partial class FRMConsultaBarcos : Form
    {
        private Dictionary<int, string> diccionarioPersonas;

        public FRMConsultaBarcos()
        {
            InitializeComponent();
            this.Load += FRMConsultaBarcos_Load;
            this.btnBuscar.Click += btnBuscar_Click;
            this.btnActualizar.Click += btnActualizar_Click;
            this.btnEliminar.Click += btnEliminar_Click;
        }

        private void FRMConsultaBarcos_Load(object sender, EventArgs e)
        {
            CargarDiccionarioPersonas();
            CargarFiltroOwner();
            CargarBarcos(BLLBarco.ConsultarBarcos(null));
        }

        private void CargarDiccionarioPersonas()
        {
            diccionarioPersonas = new Dictionary<int, string>();
            var personas = BLLPersona.ConsultarPersonas(null);
            foreach (var p in personas)
            {
                if (p.IdPersona > 0 && !diccionarioPersonas.ContainsKey(p.IdPersona))
                {
                    diccionarioPersonas[p.IdPersona] = p.Nombre;
                }
            }
        }

        private void CargarFiltroOwner()
        {
            cbxIdOwnerFiltro.Items.Clear();
            cbxIdOwnerFiltro.Items.Add("Todos");
            var personas = BLLPersona.ConsultarPersonas(null);
            foreach (var p in personas)
            {
                cbxIdOwnerFiltro.Items.Add(new { Text = p.Nombre, Value = p.IdPersona });
            }
            cbxIdOwnerFiltro.DisplayMember = "Text";
            cbxIdOwnerFiltro.ValueMember = "Value";
            cbxIdOwnerFiltro.SelectedIndex = 0;
        }

        private void CargarBarcos(List<VOBarco> lista)
        {
            dgvBarcos.DataSource = null;
            dgvBarcos.DataSource = lista.Select(b => new
            {
                b.IdBarco,
                b.Matricula,
                b.NoAmarre,
                b.Nombre,
                b.Cuota,
                Owner = b.IdPersona.HasValue && diccionarioPersonas.ContainsKey(b.IdPersona.Value)
                    ? diccionarioPersonas[b.IdPersona.Value]
                    : "Sin asignar"
            }).ToList();

            dgvBarcos.Columns["IdBarco"].HeaderText = "ID";
            dgvBarcos.Columns["Matricula"].HeaderText = "Matrícula";
            dgvBarcos.Columns["NoAmarre"].HeaderText = "No. Amarre";
            dgvBarcos.Columns["Nombre"].HeaderText = "Nombre";
            dgvBarcos.Columns["Cuota"].HeaderText = "Cuota";
            dgvBarcos.Columns["Owner"].HeaderText = "Propietario";
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string idTexto = txtIdFiltro.Text.Trim();

            if (!string.IsNullOrEmpty(idTexto))
            {
                if (int.TryParse(idTexto, out int id))
                {
                    try
                    {
                        VOBarco barco = BLLBarco.ConsultarBarco(idTexto);
                        if (barco != null)
                        {
                            CargarBarcos(new List<VOBarco> { barco });
                        }
                        else
                        {
                            MessageBox.Show("No se encontró barco con ese ID");
                            CargarBarcos(new List<VOBarco>());
                        }
                    }
                    catch
                    {
                        MessageBox.Show("No se encontró barco con ese ID");
                        CargarBarcos(new List<VOBarco>());
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese un ID válido");
                }
                return;
            }

            if (cbxIdOwnerFiltro.SelectedIndex > 0)
            {
                var item = cbxIdOwnerFiltro.SelectedItem;
                int idOwner = (int)item.GetType().GetProperty("Value").GetValue(item, null);
                List<VOBarco> lista = BLLBarco.ConsultarBarcosPorOwner(idOwner.ToString(), null);
                CargarBarcos(lista);
                return;
            }

            CargarBarcos(BLLBarco.ConsultarBarcos(null));
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (dgvBarcos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un barco");
                return;
            }

            int idBarco = (int)dgvBarcos.SelectedRows[0].Cells["IdBarco"].Value;
            VOBarco barco = BLLBarco.ConsultarBarco(idBarco.ToString());

            if (barco == null)
            {
                MessageBox.Show("No se encontró el barco");
                return;
            }

            FRMActualizarBarco formActualizar = new FRMActualizarBarco(barco);
            formActualizar.FormClosed += (s, args) => btnBuscar_Click(null, null);
            formActualizar.ShowDialog();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvBarcos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un barco");
                return;
            }

            DialogResult result = MessageBox.Show(
                "¿Está seguro de eliminar este barco?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                int idBarco = (int)dgvBarcos.SelectedRows[0].Cells["IdBarco"].Value;
                try
                {
                    BLLBarco.Eliminar(idBarco.ToString());
                    MessageBox.Show("Barco eliminado correctamente");
                    btnBuscar_Click(null, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar: " + ex.Message);
                }
            }
        }
    }
}
