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
    public partial class ViewStudentPub : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Label2.Text = "";
            ltTable.Text = "";
            try
            {
                String connStr = WebConfigurationManager.ConnectionStrings["postGrad"].ToString();

                SqlConnection conn = new SqlConnection(connStr);

                SqlCommand viewPub = new SqlCommand("ViewAStudentPublications", conn);
                viewPub.CommandType = CommandType.StoredProcedure;
                viewPub.Parameters.Add(new SqlParameter("@studentID", SqlDbType.Int)).Value = TextBox1.Text;
                DataTable t = new DataTable();
                t.Columns.AddRange(new DataColumn[6] { new DataColumn("Publication ID", typeof(int)),
                    new DataColumn("Title", typeof(string)),
                    new DataColumn("Date",typeof(DateTime)),
             new DataColumn("Place",typeof(String)),
             new DataColumn("Accepted",typeof(bool)),
             new DataColumn("Host",typeof(string))});
                conn.Open();
                SqlDataReader rdr = viewPub.ExecuteReader(CommandBehavior.CloseConnection);
                while (rdr.Read())
                {
                    int id = rdr.GetInt32(rdr.GetOrdinal("id"));
                    String title = rdr.GetString(rdr.GetOrdinal("title"));
                    DateTime date = rdr.GetDateTime(rdr.GetOrdinal("date"));
                    String place = rdr.GetString(rdr.GetOrdinal("place"));
                    bool acc = rdr.GetBoolean(rdr.GetOrdinal("accepted"));
                    String host = rdr.GetString(rdr.GetOrdinal("host"));

                    t.Rows.Add(id, title, date, place, acc, host);
                }
                StringBuilder sb = new StringBuilder();
                //Table start.
                sb.Append("<table cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-size: 9pt;font-family:Arial'>");

                //Adding HeaderRow.
                sb.Append("<tr>");
                foreach (DataColumn column in t.Columns)
                {
                    sb.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>" + column.ColumnName + "</th>");
                }
                sb.Append("</tr>");


                //Adding DataRow.
                foreach (DataRow row in t.Rows)
                {
                    sb.Append("<tr>");
                    foreach (DataColumn column in t.Columns)
                    {
                        sb.Append("<td style='width:100px;border: 1px solid #ccc'>" + row[column.ColumnName].ToString() + "</td>");
                    }
                    sb.Append("</tr>");
                }

                //Table end.
                sb.Append("</table>");
                ltTable.Text = sb.ToString();
            }
           catch(Exception ex)
            {
                if (TextBox1.Text == "")
                {
                    Label2.Text = "This field is required";
                }
                else
                {
                    Label2.Text = "The student ID must consist of digits only";
                }
            }
        }

        protected void Home(object sender, EventArgs e)
        {
            Response.Redirect("SupervisorHome.aspx");
        }
    }
}