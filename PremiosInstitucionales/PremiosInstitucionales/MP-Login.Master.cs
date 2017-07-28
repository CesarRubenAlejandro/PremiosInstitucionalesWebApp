using System;
using System.Web.UI;

namespace PremiosInstitucionales
{
    public partial class MP_Login : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Do nothing
        }

        public void showErrorMsg(string tipoErrorTitulo, string sMensaje)
        {
            // Creamos el titutlo del Modal
            modalMensajeTitulo.Controls.Add(new LiteralControl(TituloModal(tipoErrorTitulo)));

            // Mensaje del Modal
            Mensaje.Text = sMensaje;

            // Mostramos el Modal
            string showMsg_JS = "$('#modalMensaje').modal('show')";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "showE", showMsg_JS, true);
        }

        public string TituloModal(string tipoTitulo)
        {
            switch (tipoTitulo)
            {
                case "Error":
                    return "<h4 class=\"modal-title\"> <img src=\"../Resources/svg/warning.svg\" class=\"error-icon\"/> Advertencia </h4>";
                case "Aviso":
                    return "<h4 class=\"modal-title\"> <img src=\"../Resources/svg/done.svg\" class=\"error-icon\"/> Listo </h4>";
                default:
                    return "";
            }
        }
    }
}