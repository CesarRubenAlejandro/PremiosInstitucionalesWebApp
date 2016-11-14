using PremiosInstitucionales.DBServices.InformacionPersonalCandidato;
using PremiosInstitucionales.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PremiosInstitucionales.WebForms
{
    public partial class InformacionPersonalCandidato : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MostrarCampos();
            }
        }

        private void MostrarCampos()
        {
            Tuple<string, string> nombres = InformacionPersonalCandidatoService.GetNombre(Session[StringValues.CorreoSesion].ToString());
            NombresTextBox.Text = nombres.Item1;
            ApellidosTextBox.Text = nombres.Item2;
            CorreoTextBox.Text = Session[StringValues.CorreoSesion].ToString();
        }

        protected void EnviarBtn_Click(object sender, EventArgs e)
        {
            string nombres = NombresTextBox.Text;
            string apellidos = ApellidosTextBox.Text;
            string correo = CorreoTextBox.Text;
            string domicilio = DomicilioTextBox.Text.ToString();
            string ciudad = CiudadTextBox.Text.ToString();
            string estado = EstadoTextBox.Text.ToString();
            string cp = CodigoPostalTextBox.Text.ToString();
            string telefono = TelefonoTextBox.Text.ToString();
            string puesto = PuestoTextBox.Text.ToString();
            string institucion = InstitucionTextBox.Text.ToString();
            if (InformacionPersonalCandidatoService.Set(Session[StringValues.CorreoSesion].ToString(), nombres, apellidos, correo))
            {
                Mensaje.Text = "Tus cambios han sido guardados";
                Session[StringValues.CorreoSesion] = correo;
            }
            else
            {
                Mensaje.Text = "Hubo un error al guardar tus cambios";
                MostrarCampos();
            }
            EditarBtn_Click(sender, e);
        }

        protected void EditarBtn_Click(object sender, EventArgs e)
        {
            if (EditarBtn.Text.ToString() == "Editar datos")
            {
                EditarBtn.Text = "Cancelar cambios";
            }
            else
            {
                EditarBtn.Text = "Editar datos";
                MostrarCampos();
            }

            EnviarBtn.Enabled = !EnviarBtn.Enabled;
            NombresTextBox.Enabled = !NombresTextBox.Enabled;
            ApellidosTextBox.Enabled = !ApellidosTextBox.Enabled;
            CorreoTextBox.Enabled = !CorreoTextBox.Enabled;
            DomicilioTextBox.Enabled = !DomicilioTextBox.Enabled;
            CiudadTextBox.Enabled = !CiudadTextBox.Enabled;
            EstadoTextBox.Enabled = !EstadoTextBox.Enabled;
            CodigoPostalTextBox.Enabled = !CodigoPostalTextBox.Enabled;
            TelefonoTextBox.Enabled = !TelefonoTextBox.Enabled;
            PuestoTextBox.Enabled = !PuestoTextBox.Enabled;
            InstitucionTextBox.Enabled = !InstitucionTextBox.Enabled;
        }
    }
}