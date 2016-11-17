using System;
using System.Web.UI.WebControls;

namespace PremiosInstitucionales.WebForms
{
    public partial class CorrigeAplicacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // crear botones de prueba
            Button btn1 = new Button();
            btn1.ID = "btn1";
            btn1.Text = "abrir popup";
            btn1.OnClientClick = "return ShowModalPopup(\"abc\")";
            panel.Controls.Add(btn1);
        }

        protected void bttnEnviarRechazo_Click(object sender, EventArgs e)
        {
            String s = IdAppHidden.Value.ToString();
            // USAR EL VALOR DEL STRING
        }
    }
}