using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace finalM3
{
    public partial class WelcomePage : System.Web.UI.Page
    {
        public static Decimal Identity = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void register_click(object sender, EventArgs e)
        {
            Response.Redirect("RegisterPage.aspx");
        }
        protected void login_click(object sender, EventArgs e)
        {
            Response.Redirect("LoginPage.aspx");
        }
    }
}