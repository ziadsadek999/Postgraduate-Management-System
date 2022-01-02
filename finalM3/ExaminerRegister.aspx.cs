using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace finalM3
{
    public partial class ExaminerRegister : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Login_Click(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["postGrad"].ToString();

            SqlConnection conn = new SqlConnection(connStr);
            Label1.Text = " ";
            Label2.Text = " ";
            Label3.Text = " ";
            Label4.Text = " ";
            
            Label7.Text = " ";
            String name = TextBox1.Text;
            String pass = TextBox2.Text;
            String field = TextBox3.Text;
            String email = TextBox4.Text;
           
            Boolean f = true;
            if (name.Replace(" ", "") == "")
            {
                Label1.Text = "This feild is missing";
                f = false;
            }
            if (pass == "")
            {
                Label2.Text = "This feild is missing";
                f = false;
            }
            if (field.Replace(" ","") == "")
            {
                Label3.Text = "This feild is missing";
                f = false;
            }
            if (email.Replace(" ", "") == "")
            {
                Label4.Text = "This feild is missing";
                f = false;
            }
            if (!RadioButton1.Checked && !RadioButton2.Checked)
            {
                Label7.Text = "Please choose an option";
                f = false;
            }
            if (!f)
                return;
            SqlCommand loginProc = new SqlCommand("examinerRegister", conn);
            loginProc.CommandType = CommandType.StoredProcedure;

            loginProc.Parameters.Add(new SqlParameter("@name", SqlDbType.VarChar)).Value = name;
           
            loginProc.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar)).Value = pass;
            loginProc.Parameters.Add(new SqlParameter("@field", SqlDbType.VarChar)).Value = field;
            loginProc.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar)).Value = email;
           
            if (RadioButton1.Checked)
            {
                loginProc.Parameters.Add(new SqlParameter("@isN", SqlDbType.Bit)).Value = true;
            }
            else
            {
                loginProc.Parameters.Add(new SqlParameter("@isN", SqlDbType.Bit)).Value = false;
            }
            conn.Open();
            loginProc.ExecuteNonQuery();
            conn.Close();
            SqlCommand lastId = new SqlCommand("SELECT IDENT_CURRENT('PostGradUser') as 'ID'", conn);
            conn.Open();
            SqlDataReader rdr = lastId.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                WelcomePage.Identity = rdr.GetDecimal(rdr.GetOrdinal("ID"));
            }
            Response.Redirect("Examiner.aspx");
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegisterPage.aspx");
        }
    }
}