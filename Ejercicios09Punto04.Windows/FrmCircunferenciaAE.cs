using System;
using System.Windows.Forms;
using Ejercicios09Punto4.Bl;

namespace Ejercicios09Punto04.Windows
{
    public partial class FrmCircunferenciaAE : Form
    {
        public FrmCircunferenciaAE()
        {
            InitializeComponent();
        }

        private Circunferencia _circunferencia;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (_circunferencia!=null)
            {
                RadioTextBox.Text = _circunferencia.Radio.ToString();
            }

        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if (_circunferencia==null)
                {
                    _circunferencia=new Circunferencia();
                }

                _circunferencia.Radio = int.Parse(RadioTextBox.Text);
                DialogResult = DialogResult.OK;
            }
        }

        private bool ValidarDatos()
        {
            bool valido = true;
            errorProvider1.Clear();
            if (int.TryParse(RadioTextBox.Text, out int radio))
            {
                if (radio<=0)
                {
                    valido = false;
                    errorProvider1.SetError(RadioTextBox,"Radio no válido");
                }
            }
            else
            {
                valido = false;
                errorProvider1.SetError(RadioTextBox,"Radio mal ingresado");
            }

            return valido;
        }

        public Circunferencia GetCircunferencia()
        {
            return _circunferencia;
        }

        public void SetCircunferencia(Circunferencia circunferencia)
        {
            _circunferencia = circunferencia;
        }
    }
}
