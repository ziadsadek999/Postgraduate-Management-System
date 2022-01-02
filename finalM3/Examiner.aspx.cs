using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace finalM3
{
    public partial class Examiner : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            EID.Text = "ID: " + WelcomePage.Identity;
        }

        protected void Update_name(object sender, EventArgs e)
        {
            Response.Redirect("UpdateName.aspx");
        }

        protected void Update_fieldOfWork(object sender, EventArgs e)
        {
            Response.Redirect("UpdateFieldOfWork.aspx");
        }

        protected void List_ThesisSupDefense(object sender, EventArgs e)
        {
            Response.Redirect("ThesesSupStu.aspx");
        }

        protected void Add_comment(object sender, EventArgs e)
        {
            Response.Redirect("Comment.aspx");
        }

        protected void Add_Grade(object sender, EventArgs e)
        {
            Response.Redirect("AddDefenseGrade.aspx");
        }

        protected void Search_thesis(object sender, EventArgs e)
        {
            Response.Redirect("SearchThesis.aspx");
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            Response.Redirect("WelcomePage.aspx");
        }
    }
}