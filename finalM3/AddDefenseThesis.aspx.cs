using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace finalM3
{
    public partial class AddDefenseThesis : System.Web.UI.Page
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
            Boolean f = true;
            if (TextBox1.Text == "")
            {
                Label2.Text = "This field is required";
                f = false;
            }
            if (TextBox3.Text.Replace(" ","") == "")
            {
                Label4.Text = "This field is required";
                f = false;
            }
            if (datepicker.SelectedDate.ToLongDateString() == "Monday, January 1, 0001")
            {
                Label3.Text = "Please pick a date";
                f = false;
            }
            for (int i = 0; i < TextBox1.Text.Length; i++)
            {
                if (TextBox1.Text[i] < '0' || TextBox1.Text[i] > '9')
                {
                    Label2.Text = "Thesis serial number must consist of digits only";

                    f = false;
                }
            }
            if (!f)
                return;
           
                String connStr = WebConfigurationManager.ConnectionStrings["postGrad"].ToString();

                SqlConnection conn = new SqlConnection(connStr);

                String serialNo = TextBox1.Text;
                String date = datepicker.SelectedDate.ToLongDateString();
                String loc = TextBox3.Text;


            SqlCommand test1 = new SqlCommand("Select * from Thesis where serialNumber = " + serialNo, conn);
            conn.Open();
            String tmp1 = "";
            SqlDataReader rdr1 = test1.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr1.Read())
            {
                tmp1 += rdr1["serialNumber"].ToString();
            }
            conn.Close();
            if (tmp1.Length == 0)
            {
                Label2.Text = "There is not any thesis with this serial number";
                return;
            }


            SqlCommand test = new SqlCommand("Select * from GUCianStudentRegisterThesis where serial_no = " + serialNo, conn);
                conn.Open();
                String tmp = "";
                SqlDataReader rdr = test.ExecuteReader(CommandBehavior.CloseConnection);
                while (rdr.Read())
                {
                    tmp += rdr["sid"].ToString();
                }
            conn.Close();
                if (tmp.Length == 0)
                {
                    SqlCommand addProc = new SqlCommand("AddDefenseNonGucian", conn);
                    addProc.CommandType = CommandType.StoredProcedure;

                    addProc.Parameters.Add(new SqlParameter("@ThesisSerialNo", SqlDbType.Int)).Value = serialNo;
                    addProc.Parameters.Add(new SqlParameter("@DefenseDate", SqlDbType.Date)).Value = date;
                    addProc.Parameters.Add(new SqlParameter("@DefenseLocation", SqlDbType.VarChar)).Value = loc;
                    try
                    {
                    conn.Open();
                        addProc.ExecuteNonQuery();
                        conn.Close();
                        Label1.Text="Defense added successfully!";
                    }
                    catch(SqlException ex)
                    {
                        Label5.Text="Cannot add defense because the student Did not Pass all his/her Courses";
                    }
                   

                }
                else
                {
                    SqlCommand addProc = new SqlCommand("AddDefenseGucian", conn);
                    addProc.CommandType = CommandType.StoredProcedure;

                    addProc.Parameters.Add(new SqlParameter("@ThesisSerialNo", SqlDbType.Int)).Value = serialNo;
                    addProc.Parameters.Add(new SqlParameter("@DefenseDate", SqlDbType.Date)).Value = date;
                    addProc.Parameters.Add(new SqlParameter("@DefenseLocation", SqlDbType.VarChar)).Value = loc;

                conn.Open();
                        addProc.ExecuteNonQuery();
                        conn.Close();
                Label1.Text = "Defense added successfully!";


            }

            



        }

        protected void Home(object sender, EventArgs e)
        {
            Response.Redirect("SupervisorHome.aspx");
        }
    }
}