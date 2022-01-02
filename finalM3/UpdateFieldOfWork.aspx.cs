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
    public partial class UpdateFieldOfWork : System.Web.UI.Page
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
            newFieldWarning.Text = "";
            suc.Text = "";
            string connStr = WebConfigurationManager.ConnectionStrings["postGrad"].ToString();

            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand update_Field = new SqlCommand("updateExamFieldOfWork", conn);
            update_Field.CommandType = System.Data.CommandType.StoredProcedure;

            string newfield = newField.Text;
            if(newfield.Length == 0)
            {
                newFieldWarning.Text = "This Field is missing";
                return;
            }

            update_Field.Parameters.Add(new SqlParameter("@ExamId", WelcomePage.Identity));
            update_Field.Parameters.Add(new SqlParameter("@newField", newfield));

            conn.Open();
            try
            {
                update_Field.ExecuteNonQuery();
                suc.Text = "Field Updated Successfully";
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