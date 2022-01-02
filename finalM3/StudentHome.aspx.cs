using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace finalM3
{
    public partial class StudentHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = "ID: " + WelcomePage.Identity;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddPhone.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudentProfile.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudentThesis.aspx");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudentCourse.aspx");
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddProgressReport.aspx");
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            Response.Redirect("FillProgressReport.aspx");
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddPublication.aspx");
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            Response.Redirect("LinkPublication.aspx");
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("WelcomePage.aspx");
        }
    }
}