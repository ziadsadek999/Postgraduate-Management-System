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
    public partial class FillProgressReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Label1.Text = "";
            Label2.Text = "";
            Label3.Text = "";
            Label4.Text = "";
            Label5.Text = "";
            Label6.Text = "";
            Boolean f = true;
            if (TextBox1.Text == "")
            {
                Label1.Text = "This field is missing";
                f = false;
            }
            if (TextBox2.Text == "")
            {
                Label2.Text = "This field is missing";
                f = false;
            }
            if (TextBox3.Text == "")
            {
                Label3.Text = "This field is missing";
                f = false;
            }
            if (TextBox4.Text == "")
            {
                Label4.Text = "This field is missing";
                f = false;
            }
            for(int i = 0; i < TextBox1.Text.Length; i++)
            {
                if (TextBox1.Text[i] < '0' || TextBox1.Text[i] > '9')
                {
                    Label1.Text = "Thesis serial number must consist of digits only";
                    f = false;
                    break;
                }
            }
            for (int i = 0; i < TextBox2.Text.Length; i++)
            {
                if (TextBox2.Text[i] < '0' || TextBox2.Text[i] > '9')
                {
                    Label2.Text = "Progress report number must consist of digits only";
                    f = false;
                    break;
                }
            }
            for (int i = 0; i < TextBox3.Text.Length; i++)
            {
                if (TextBox3.Text[i] < '0' || TextBox3.Text[i] > '9')
                {
                    Label3.Text = "State must consist of digits only";
                    f = false;
                    break;
                }
            }
            if (!f)
                return;



            String connStr = WebConfigurationManager.ConnectionStrings["postGrad"].ToString();

            SqlConnection conn = new SqlConnection(connStr);


            SqlCommand testOngoing = new SqlCommand("checkOnGoing", conn);
            testOngoing.CommandType = CommandType.StoredProcedure;
            testOngoing.Parameters.Add(new SqlParameter("@serial", SqlDbType.Int)).Value = TextBox1.Text;
            try
            {
                conn.Open();
                testOngoing.ExecuteNonQuery();
                conn.Close();
            }
            catch (SqlException ex)
            {
                Label1.Text = ex.Message;
                return;
            }


            String thesisSerialNo = TextBox1.Text;
            String progressReportNo = TextBox2.Text;
            String state = TextBox3.Text;
            String description = TextBox4.Text;

            SqlCommand test = new SqlCommand("checkProgressReportIsMine", conn);
            test.CommandType = CommandType.StoredProcedure;
            test.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = WelcomePage.Identity;
            test.Parameters.Add(new SqlParameter("@serialNo", SqlDbType.Int)).Value = thesisSerialNo;
            SqlParameter res = test.Parameters.Add("@res", SqlDbType.Bit);
            res.Direction = System.Data.ParameterDirection.Output;

            conn.Open();
            test.ExecuteNonQuery();
            conn.Close();

            if (res.Value.ToString() == "True")
            {
                SqlCommand addProc = new SqlCommand("FillProgressReport", conn);
                addProc.CommandType = CommandType.StoredProcedure;

                addProc.Parameters.Add(new SqlParameter("@thesisSerialNo", SqlDbType.Int)).Value = thesisSerialNo;
                addProc.Parameters.Add(new SqlParameter("@progressReportNo", SqlDbType.Int)).Value = progressReportNo;
                addProc.Parameters.Add(new SqlParameter("@state", SqlDbType.Int)).Value = state;
                addProc.Parameters.Add(new SqlParameter("@description", SqlDbType.VarChar)).Value = description;
                try
                {
                    conn.Open();
                    addProc.ExecuteNonQuery();
                    conn.Close();
                   Label5.Text="Progress report filled successfully!";
                }
                catch (SqlException ex)
                {
                    Label6.Text="There is not any progress report with this progress report number and thesis serial number";
                }
            }
            else
            {
                Label6.Text = "You are not registered to a thesis with this serial number";
            }
        }

        protected void Home(object sender, EventArgs e)
        {
            Response.Redirect("StudentHome.aspx");
        }
    }
}