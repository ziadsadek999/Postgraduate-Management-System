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
    public partial class SupervisorRegister : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           
                String connStr = WebConfigurationManager.ConnectionStrings["postGrad"].ToString();

                SqlConnection conn = new SqlConnection(connStr);

                String firstName = TextBox1.Text;
                String lastName = TextBox2.Text;
                String pass = TextBox3.Text;
                String faculty = TextBox4.Text;
                String email = TextBox5.Text;
                Label1.Text = " ";
                Label2.Text = " ";
                Label3.Text = " ";
                Label4.Text = " ";
                Label5.Text = " ";
                Boolean f = true;
                if (firstName.Replace(" ","") == "")
                {
                    Label1.Text = "This feild is missing";
                    f = false;
                }
                if (lastName.Replace(" ", "") == "")
                {
                    Label2.Text = "This feild is missing";
                    f = false;
                }
                if (pass == "")
                {
                    Label3.Text = "This feild is missing";
                    f = false;
                }
                if (faculty.Replace(" ", "") == "")
                {
                    Label4.Text = "This feild is missing";
                    f = false;
                }
                if (email.Replace(" ", "") == "")
                {
                    Label5.Text = "This feild is missing";
                    f = false;
                }
                if (!f)
                    return;
                
                    SqlCommand loginProc = new SqlCommand("SupervisorRegister", conn);
                    loginProc.CommandType = CommandType.StoredProcedure;

                    loginProc.Parameters.Add(new SqlParameter("@first_name", SqlDbType.VarChar)).Value = firstName;
                    loginProc.Parameters.Add(new SqlParameter("@last_name", SqlDbType.VarChar)).Value = lastName;
                    loginProc.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar)).Value = pass;
                    loginProc.Parameters.Add(new SqlParameter("@faculty", SqlDbType.VarChar)).Value = faculty;
                    loginProc.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar)).Value = email;
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
                    Response.Redirect("SupervisorHome.aspx");

                
           
            

        }
        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("WelcomePage.aspx");
        }
    }
}