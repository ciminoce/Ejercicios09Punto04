using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Ejercicios09Punto.Dl;
using Ejercicios09Punto4.Bl;

namespace Ejercicios09Punto04.Windows
{
    public partial class FrmMenuPrincipal : Form
    {
        public FrmMenuPrincipal()
        {
            InitializeComponent();
        }

        private RepositorioCircuferencias _repositorio;
        private List<Circunferencia> _lista;
        private void FrmMenuPrincipal_Load(object sender, EventArgs e)
        {
            _repositorio=new RepositorioCircuferencias();
            
            var cantidad = _repositorio.GetCantidad();
            ActualizarGrilla();
            RegistrosTextBox.Enabled = false;
            RegistrosTextBox.Text = cantidad.ToString();

        }

        private void ActualizarGrilla()
        {
            _lista = _repositorio.GetLista();
            MostrarListaEnGrilla();
            
        }

        private void MostrarListaEnGrilla()
        {
            DatosDataGridView.Rows.Clear();
            foreach (var circunferencia in _lista)
            {
                DataGridViewRow r = ConstruirFila();
                SetearFila(r, circunferencia);
                AgregarFila(r);
            }
        }

        private void AgregarFila(DataGridViewRow r)
        {
            DatosDataGridView.Rows.Add(r);
        }

        private void SetearFila(DataGridViewRow r, Circunferencia circunferencia)
        {
            r.Cells[colRadio.Index].Value = circunferencia.Radio;
            r.Cells[colPerimetro.Index].Value = circunferencia.GetPerimetro().ToString("N2");
            r.Cells[colArea.Index].Value = circunferencia.GetArea().ToString("N2");

            r.Tag = circunferencia;
        }

        private DataGridViewRow ConstruirFila()
        {
            DataGridViewRow r=new DataGridViewRow();
            r.CreateCells(DatosDataGridView);
            return r;
        }

        private void SalirToolStripButton_Click(object sender, EventArgs e)
        {

            if (_repositorio.EstaModificado)
            {
                _repositorio.GuardarDatosEnArchivo();

            } 
            Application.Exit();
        }

        private void NuevoToolStripButton_Click(object sender, EventArgs e)
        {
            FrmCircunferenciaAE frm=new FrmCircunferenciaAE();
            frm.Text = "Agregar una Circuferencia";
            DialogResult dr = frm.ShowDialog(this);
            if (dr==DialogResult.OK)
            {
                Circunferencia circunferencia = frm.GetCircunferencia();
                if (!_repositorio.ExisteCircunferencia(circunferencia))
                {
                    _repositorio.Agregar(circunferencia);
                    DataGridViewRow r = ConstruirFila();
                    SetearFila(r, circunferencia);
                    AgregarFila(r);

                    RegistrosTextBox.Text = _repositorio.GetCantidad().ToString();
                    MessageBox.Show("Registro agregado", "Mensaje",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Registro existente", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void BorrarToolStripButton_Click(object sender, EventArgs e)
        {
            if (DatosDataGridView.SelectedRows.Count==0)
            {
                return;
            }

            DataGridViewRow r = DatosDataGridView.SelectedRows[0];
            Circunferencia circunferencia = r.Tag as Circunferencia;
            DialogResult dr = MessageBox.Show("Desea dar de baja la circunferencia seleccionada?",
                "Confirmar Baja",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr==DialogResult.Yes)
            {
                _repositorio.Borrar(circunferencia);
                DatosDataGridView.Rows.Remove(r);
                RegistrosTextBox.Text = _repositorio.GetCantidad().ToString();
                MessageBox.Show("Registro Borrado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void EditarToolStripButton_Click(object sender, EventArgs e)
        {
            if (DatosDataGridView.SelectedRows.Count == 0)
            {
                return;
            }

            DataGridViewRow r = DatosDataGridView.SelectedRows[0];
            Circunferencia circunferencia = r.Tag as Circunferencia;
            Circunferencia cirAuxiliar=circunferencia.Clone() as Circunferencia;
            
            FrmCircunferenciaAE frm=new FrmCircunferenciaAE();
            frm.Text = "Edición de Circunferencia";
            frm.SetCircunferencia(circunferencia);
            DialogResult dr = frm.ShowDialog(this);
            if (dr==DialogResult.OK)
            {
                circunferencia = frm.GetCircunferencia();
                if (!_repositorio.ExisteCircunferencia(circunferencia))
                {
                    _repositorio.EstaModificado = true;
                    SetearFila(r, circunferencia);
                    MessageBox.Show("Registro editado", "Mensaje", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                }
                else
                {
                    SetearFila(r, cirAuxiliar);
                    MessageBox.Show("Registro existente", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void MenuPrincipalToolStrip_Click(object sender, EventArgs e)
        {

        }

        private void OrdenarToolStripButton_Click(object sender, EventArgs e)
        {
            _lista = _repositorio.GetListaOrdenada();
            MostrarListaEnGrilla();
        }

        private void ActualizarToolStripButton_Click(object sender, EventArgs e)
        {
           ActualizarGrilla();
        }

        private void FiltrarToolStripButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ValorRadioToolStripTextBox.Text))
            {
                return;
            }

            if (!int.TryParse(ValorRadioToolStripTextBox.Text, out int radio))
            {
                MessageBox.Show("No se pudo convertir a entero"
                                + Environment.NewLine
                                + "No se puede aplicar el filtro", "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                ValorRadioToolStripTextBox.Clear();
                ValorRadioToolStripTextBox.Focus();
            }

            _lista = _repositorio.GetListaFiltrada(radio);
            MostrarListaEnGrilla();
            ValorRadioToolStripTextBox.Clear();
        }
    }
}
