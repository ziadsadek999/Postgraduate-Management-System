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
    public partial class AddPublication : System.Web.UI.Page
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

                String connStr = WebConfigurationManager.ConnectionStrings["postGrad"].ToString();

                SqlConnection conn = new SqlConnection(connStr);

                String title = TextBox1.Text;
                String date = datepicker.SelectedDate.ToLongDateString();
                String host = TextBox3.Text;
                String place = TextBox4.Text;
            bool f = true;
            if(title.Replace(" ", "") == "")
            {
                Label1.Text = "This field is required";
                f = false;
            }
            if(date== "Monday, January 1, 0001")
            {
                Label2.Text = "Please choose a date";
                f = false;
            }
            if (host.Replace(" ", "") == "")
            {
                Label3.Text = "This field is required";
                f = false;
            }
             if(place.Replace(" ", "") == "")
            {
                Label4.Text = "This field is required";
                f = false;
            }
            if (!RadioButton1.Checked && !RadioButton2.Checked)
            {
                Label5.Text = "Please choose an option";
                f = false;
            }
            if (!f)
                return;
            String acc = "False";
                if (RadioButton1.Checked)
                    acc = "True";
                SqlCommand addProc = new SqlCommand("addPublication", conn);
                addProc.CommandType = CommandType.StoredProcedure;
                
                    addProc.Parameters.Add(new SqlParameter("@title", SqlDbType.VarChar)).Value = title;
                    addProc.Parameters.Add(new SqlParameter("@pubDate", SqlDbType.Date)).Value = date;
                    addProc.Parameters.Add(new SqlParameter("@host", SqlDbType.VarChar)).Value = host;
                    addProc.Parameters.Add(new SqlParameter("@place", SqlDbType.VarChar)).Value = place;
                    addProc.Parameters.Add(new SqlParameter("@accepted", SqlDbType.Bit)).Value = acc;
                    conn.Open();
                    addProc.ExecuteNonQuery();
                    conn.Close();

            SqlCommand lastId = new SqlCommand("SELECT IDENT_CURRENT('Publication') as 'ID'", conn);
            conn.Open();
            SqlDataReader rdr = lastId.ExecuteReader(CommandBehavior.CloseConnection);
            decimal a = 0;
            while (rdr.Read())
            {
                a = rdr.GetDecimal(rdr.GetOrdinal("ID"));
            }
            Label6.Text="Publication added successfully with ID: "+a;
                
               
            
           

        }

        protected void Home(object sender, EventArgs e)
        {
            Response.Redirect("StudentHome.aspx");
        }
    }
}