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

        public void ShowMessage(String MessageType, String Message)
        {
            // Creamos el titutlo del Modal
            modalMensajeTitulo.Controls.Add(new LiteralControl(ModalTitle(MessageType)));

            // Mensaje del Modal
            Mensaje.Text = Message;

            // Mostramos el Modal
            String showMsg_JS = "$('#modalMensaje').modal('show')";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "showE", showMsg_JS, true);
        }

        public String ModalTitle(String Title)
        {
            switch (Title)
            {
                case "Error":
                    return "<h4 class=\"modal-title\"> <img src=\"../../Resources/svg/warning.svg\" class=\"error-icon\"/> Advertencia </h4>";
                case "Aviso":
                    return "<h4 class=\"modal-title\"> <img src=\"../../Resources/svg/done.svg\" class=\"error-icon\"/> Listo </h4>";
                default:
                    return "";
            }
        }
    }
}