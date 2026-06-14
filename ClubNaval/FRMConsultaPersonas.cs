using Entidades;
using LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ClubNaval
{
    public partial class FRMConsultaPersonas : Form
    {
        public FRMConsultaPersonas()
        {
            InitializeComponent();
            this.Load += FRMConsultaPersonas_Load;
            this.btnBuscar.Click += btnBuscar_Click;
            this.btnActualizar.Click += btnActualizar_Click;
            this.btnEliminar.Click += btnEliminar_Click;
        }

        private void FRMConsultaPersonas_Load(object sender, EventArgs e)
        {
            CargarFiltroCargo();
            CargarPersonas(BLLPersona.ConsultarPersonas(null));
        }

        private void CargarFiltroCargo()
        {
            cbxCargoFiltro.Items.Clear();
            cbxCargoFiltro.Items.Add("Todos");
            foreach (var cargo in Enum.GetValues(typeof(Enumeradores.CargoPersona)))
            {
                cbxCargoFiltro.Items.Add(cargo);
            }
            cbxCargoFiltro.SelectedIndex = 0;
        }

        private void CargarPersonas(List<VOPersona> lista)
        {
            dgvPersonas.DataSource = null;
            dgvPersonas.DataSource = lista.Select(p => new
            {
                p.IdPersona,
                p.Nombre,
                p.Telefono,
                Cargo = ((Enumeradores.CargoPersona)(p.Cargo ?? 0)).ToString(),
                p.Disponibilidad
            }).ToList();

            dgvPersonas.Columns["IdPersona"].HeaderText = "ID";
            dgvPersonas.Columns["Nombre"].HeaderText = "Nombre";
            dgvPersonas.Columns["Telefono"].HeaderText = "Teléfono";
            dgvPersonas.Columns["Cargo"].HeaderText = "Cargo";
            dgvPersonas.Columns["Disponibilidad"].HeaderText = "Disponible";

            if (dgvPersonas.Columns["Disponibilidad"] != null)
            {
                dgvPersonas.Columns["Disponibilidad"].Visible = false;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string idTexto = txtIdFiltro.Text.Trim();

            if (!string.IsNullOrEmpty(idTexto))
            {
                if (int.TryParse(idTexto, out int id))
                {
                    VOPersona persona = BLLPersona.ConsultarPersonaPorId(idTexto);
                    if (persona != null)
                    {
                        CargarPersonas(new List<VOPersona> { persona });
                    }
                    else
                    {
                        MessageBox.Show("No se encontró persona con ese ID");
                        CargarPersonas(new List<VOPersona>());
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese un ID válido");
                }
                return;
            }

            if (cbxCargoFiltro.SelectedItem != null && cbxCargoFiltro.SelectedIndex > 0)
            {
                Enumeradores.CargoPersona cargoEnum = (Enumeradores.CargoPersona)cbxCargoFiltro.SelectedItem;
                int cargo = (int)cargoEnum;
                List<VOPersona> lista = BLLPersona.ConsultarPersonasPorCargo(cargo.ToString(), null);
                CargarPersonas(lista);
                return;
            }

            CargarPersonas(BLLPersona.ConsultarPersonas(null));
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (dgvPersonas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una persona");
                return;
            }

            int idPersona = (int)dgvPersonas.SelectedRows[0].Cells["IdPersona"].Value;
            VOPersona persona = BLLPersona.ConsultarPersonaPorId(idPersona.ToString());

            if (persona == null)
            {
                MessageBox.Show("No se encontró la persona");
                return;
            }

            FRMActualizarPersona formActualizar = new FRMActualizarPersona(persona);
            formActualizar.FormClosed += (s, args) =>
            {
                btnBuscar_Click(null, null);
            };
            formActualizar.ShowDialog();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvPersonas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una persona");
                return;
            }

            DialogResult result = MessageBox.Show(
                "¿Está seguro de eliminar esta persona?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                int idPersona = (int)dgvPersonas.SelectedRows[0].Cells["IdPersona"].Value;
                try
                {
                    BLLPersona.Eliminar(idPersona.ToString());
                    MessageBox.Show("Persona eliminada correctamente");
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
