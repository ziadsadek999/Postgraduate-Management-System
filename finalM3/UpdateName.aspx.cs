using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace finalM3
{
    public partial class UpdateName : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Home(object sender, EventArgs e)
        {
            Response.Redirect("Examiner.aspx");
        }

        protected void Update(object sender, EventArgs e)
        {
            newNameWarning.Text = "";
            suc.Text = "";
            string connStr = WebConfigurationManager.ConnectionStrings["postGrad"].ToString();

            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand update_name = new SqlCommand("updateExamName", conn);
            update_name.CommandType = System.Data.CommandType.StoredProcedure;

            string newname = newName.Text;
            if (newname.Length == 0)
            {
                newNameWarning.Text = "This field is missing";
                return;
            }
            update_name.Parameters.Add(new SqlParameter("@ExamId", WelcomePage.Identity));
            update_name.Parameters.Add(new SqlParameter("@newName", newname));

            conn.Open();
            try
            {
                update_name.ExecuteNonQuery();
                suc.Text = "Name Updated Successfully";
                suc.ForeColor = System.Drawing.Color.Green; 
            }
            catch
            {
                suc.Text = "Error";
                suc.ForeColor = System.Drawing.Color.Red;
            }
            conn.Close();
        }
    }
}