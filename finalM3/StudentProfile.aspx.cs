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
    public partial class StudentProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["postGrad"].ToString();

            SqlConnection conn = new SqlConnection(connStr);



            SqlCommand info = new SqlCommand("viewMyProfile", conn);
            info.CommandType = CommandType.StoredProcedure;
            info.Parameters.Add(new SqlParameter("@studentId", SqlDbType.Int)).Value = WelcomePage.Identity;
            conn.Open();
           
            SqlDataReader rdr = info.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                if (Label1.Text.Replace(" ","").Length == 0)
                {
                    Label1.Text= rdr["id"].ToString();

                    Label2.Text = rdr["firstName"].ToString();

                    Label3.Text = rdr["lastName"].ToString();

                    Label4.Text = rdr["email"].ToString();

                    Label5.Text = rdr["password"].ToString();

                    Label6.Text = rdr["type"].ToString();

                    Label7.Text = rdr["faculty"].ToString();

                    Label8.Text = rdr["Address"].ToString();

                    Label9.Text = rdr["GPA"].ToString();

                    Label10.Text = rdr["phone"].ToString();
                    try
                    {
                        String x = rdr["undergradID"].ToString();
                        Label9.Text += "<br/>";
                        Label9.Text += "<br/>";
                        Label9.Text += "<b>Undergraduate ID:</b>";
                        Label9.Text += "<br/>";
                        Label9.Text += x;

                    }
                    catch
                    {
                        
                    }
                }
                else
                {
                    
                    Label10.Text += "<br/>";
                    Label10.Text += rdr["phone"].ToString();
                }
            }
           
            
        }

        protected void Home(object sender, EventArgs e)
        {
            Response.Redirect("StudentHome.aspx");
        }
    }
}