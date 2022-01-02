using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace finalM3
{
    public partial class StudentLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Label1.Text = "";
            Label2.Text = "";
            Label3.Text = "";

            if (TextBox1.Text.Replace(" ", "") == "")
            {
                Label2.Text = "This feild is required";
                if (TextBox2.Text == "")
                {
                    Label3.Text = "This feild is required";
                }
                return;
            }
            if (TextBox2.Text == "")
            {
                Label3.Text = "This feild is required";
                return;
            }
            Boolean f = true;
            for(int i = 0; i < TextBox1.Text.Length; i++)
            {
                if(TextBox1.Text[i]>'9'|| TextBox1.Text[i] < '0')
                {
                    f = false;
                    break;
                }
            }
            if (!f)
            {
                Label2.Text = "The ID must consist of numbers only";
                return;
            }
            
                String connStr = WebConfigurationManager.ConnectionStrings["postGrad"].ToString();

                SqlConnection conn = new SqlConnection(connStr);

                String id = TextBox1.Text;
                String pass = TextBox2.Text;

                SqlCommand loginProc = new SqlCommand("userLogin", conn);
                loginProc.CommandType = CommandType.StoredProcedure;

                loginProc.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = id;
                loginProc.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar)).Value = pass;


                SqlParameter sucess = loginProc.Parameters.Add("@success", SqlDbType.Bit);
                sucess.Direction = System.Data.ParameterDirection.Output;
                SqlParameter type = loginProc.Parameters.Add("@type", SqlDbType.Int);
                type.Direction = System.Data.ParameterDirection.Output;
                WelcomePage.Identity = Decimal.Parse(id);
                conn.Open();
                loginProc.ExecuteNonQuery();
                conn.Close();
                if (sucess.Value.ToString() == "True")
                {
                    if (type.Value.ToString() == "0")
                    {
                        Response.Redirect("StudentHome.aspx");
                    }
                    else
                    {
                        if (type.Value.ToString() == "1")
                        {
                            Response.Redirect("Admin.aspx");
                        }
                        else
                        {
                            if (type.Value.ToString() == "2")
                            {
                                Response.Redirect("SupervisorHome.aspx");
                            }
                            else
                            {
                                Response.Redirect("Examiner.aspx");
                            }
                        }
                    }
                }
                else
                {
                    Label1.Text="The id or the password is not correct";
                }
            
          
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("WelcomePage.aspx");
        }
    }
}