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
   
    public partial class AddProgressReport : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Label1.Text = "";
            Label2.Text = "";
            Label3.Text = "";
            bool f = true;
            if (TextBox1.Text == "")
            {
                Label2.Text = "This field is required";

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
            if(!f)
                return;



            String connStr = WebConfigurationManager.ConnectionStrings["postGrad"].ToString();

            SqlConnection conn = new SqlConnection(connStr);


            SqlCommand testDate = new SqlCommand("checkThesisDate", conn);
            testDate.CommandType = CommandType.StoredProcedure;
            testDate.Parameters.Add(new SqlParameter("@date", SqlDbType.DateTime)).Value = datepicker.SelectedDate.ToLongDateString();
            testDate.Parameters.Add(new SqlParameter("@serialNo", SqlDbType.Int)).Value = TextBox1.Text;
            SqlParameter resDate = testDate.Parameters.Add("@res", SqlDbType.Bit);
            resDate.Direction = System.Data.ParameterDirection.Output;

            conn.Open();
            testDate.ExecuteNonQuery();
            conn.Close();

            if (resDate.Value.ToString() == "False")
            {
                Label3.Text = "The progress report date must be within the start and the end of the thesis";
                return;
            }


            SqlCommand testOngoing = new SqlCommand("checkOnGoing", conn);
            testOngoing.CommandType = CommandType.StoredProcedure;
            testOngoing.Parameters.Add(new SqlParameter("@serial", SqlDbType.Int)).Value = TextBox1.Text;
            try
            {
                conn.Open();
                testOngoing.ExecuteNonQuery();
                conn.Close();
            }
            catch(SqlException ex)
            {
                Label2.Text = ex.Message;
                return;
            }

            SqlCommand lastId = new SqlCommand("SELECT IDENT_CURRENT('GUCianProgressReport') as 'ID'", conn);
            conn.Open();
            SqlDataReader rdr = lastId.ExecuteReader(CommandBehavior.CloseConnection);
            decimal a = 0;
            while (rdr.Read())
            {
               a = rdr.GetDecimal(rdr.GetOrdinal("ID"));
            }
            conn.Close();
            SqlCommand lastId2 = new SqlCommand("SELECT IDENT_CURRENT('NonGUCianProgressReport') as 'ID'", conn);
            conn.Open();
            SqlDataReader rdr2 = lastId2.ExecuteReader(CommandBehavior.CloseConnection);
            decimal b = 0;
            while (rdr2.Read())
            {
                b = rdr2.GetDecimal(rdr2.GetOrdinal("ID"));
            }
            conn.Close();

            String serialNo = TextBox1.Text;
            string date = datepicker.SelectedDate.ToLongDateString();
            SqlCommand test = new SqlCommand("checkProgressReportIsMine", conn);
            test.CommandType = CommandType.StoredProcedure;
            test.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = WelcomePage.Identity;
            test.Parameters.Add(new SqlParameter("@serialNo", SqlDbType.Int)).Value = serialNo;
            SqlParameter res = test.Parameters.Add("@res", SqlDbType.Bit);
            res.Direction = System.Data.ParameterDirection.Output;
          
            conn.Open();
            test.ExecuteNonQuery();
            conn.Close();
            if (res.Value.ToString() == "True")
            {
                SqlCommand addProc = new SqlCommand("AddProgressReport", conn);
                addProc.CommandType = CommandType.StoredProcedure;

                addProc.Parameters.Add(new SqlParameter("@thesisSerialNo", SqlDbType.Int)).Value = serialNo;
                addProc.Parameters.Add(new SqlParameter("@progressReportDate", SqlDbType.Date)).Value = date;
               
                conn.Open();
                    addProc.ExecuteNonQuery();
                conn.Close();

                SqlCommand lastId3 = new SqlCommand("SELECT IDENT_CURRENT('GUCianProgressReport') as 'ID'", conn);
                conn.Open();
                SqlDataReader rdr3 = lastId3.ExecuteReader(CommandBehavior.CloseConnection);
                decimal c = 0;
                while (rdr3.Read())
                {
                    c = rdr3.GetDecimal(rdr3.GetOrdinal("ID"));
                }
                conn.Close();
                SqlCommand lastId4 = new SqlCommand("SELECT IDENT_CURRENT('NonGUCianProgressReport') as 'ID'", conn);
                conn.Open();
                SqlDataReader rdr4 = lastId4.ExecuteReader(CommandBehavior.CloseConnection);
                decimal d = 0;
                while (rdr4.Read())
                {
                    d = rdr4.GetDecimal(rdr4.GetOrdinal("ID"));
                }
                conn.Close();
                if (a == c)
                {
                    Label1.Text = "Progress report added successfully with ID: "+d;
                }
                else
                {
                    Label1.Text = "Progress report added successfully with ID: "+c;
                }
               
            }
            else
            {
                Label2.Text= "You are not registered to a thesis with this serial number.";
            }
           
               
            
            
        }

        protected void Home(object sender, EventArgs e)
        {
            Response.Redirect("StudentHome.aspx");
        }
        
       
    }
}