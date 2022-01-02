using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace finalM3
{
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = "ID: " + WelcomePage.Identity;
        }

        protected void List_all_supervisors(object sender, EventArgs e)
        {
            Response.Redirect("Supervisors.aspx");
        }

        protected void List_all_Thesis(object sender, EventArgs e)
        {
            Response.Redirect("Thesis.aspx");
        }

        protected void Issue_Payment(object sender, EventArgs e)
        {
            Response.Redirect("IssuePayment.aspx");
        }

        protected void Issue_Installment(object sender, EventArgs e)
        {
            Response.Redirect("IssueInstallment.aspx");
        }

        protected void Update_ExtensionNO(object sender, EventArgs e)
        {
            Response.Redirect("UpdateExtension.aspx");
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            Response.Redirect("WelcomePage.aspx");
        }
    }
}