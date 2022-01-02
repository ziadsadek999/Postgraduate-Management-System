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
    public partial class AddPhone : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudentHome.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Label1.Text = "";
            Label2.Text = "";

            String connStr = WebConfigurationManager.ConnectionStrings["postGrad"].ToString();

            SqlConnection conn = new SqlConnection(connStr);

            String number = TextBox1.Text;
            if (number == "")
            {
                Label1.Text = "Phone number cannot be blank";
                return;
            }
            Boolean f = true;
            for(int i = 0; i < number.Length; i++)
            {
                if (number[i] < '0' || number[i] > '9')
                {
                    Label1.Text = "Phone number must consist of digits only";
                    f = false;
                    break;
                }
            }
            if (!f)
                return;
           
               
                    SqlCommand addPhoneProc = new SqlCommand("addMobile", conn);
                    addPhoneProc.CommandType = CommandType.StoredProcedure;

                    addPhoneProc.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = WelcomePage.Identity;
                    addPhoneProc.Parameters.Add(new SqlParameter("@mobile_number", SqlDbType.VarChar)).Value = number;

                    conn.Open();
                    addPhoneProc.ExecuteNonQuery();
                    conn.Close();
                    Label2.Text="Phone number added successfully!";
            TextBox1.Text = "";
                


            
            
            
        }
    }
}