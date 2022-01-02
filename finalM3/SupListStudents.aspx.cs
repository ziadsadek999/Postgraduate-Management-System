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
    public partial class SupListStudents : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["postGrad"].ToString();

            SqlConnection conn = new SqlConnection(connStr);


           

                SqlCommand viewSt = new SqlCommand("ViewSupStudentsYears", conn);
                viewSt.CommandType = CommandType.StoredProcedure;
                viewSt.Parameters.Add(new SqlParameter("@supervisorID", SqlDbType.Int)).Value = WelcomePage.Identity;
                DataTable t = new DataTable();
                t.Columns.AddRange(new DataColumn[3] { new DataColumn("First name", typeof(string)),
                    new DataColumn("Last name", typeof(string)),
                    new DataColumn("Years",typeof(int))});
                conn.Open();
                SqlDataReader rdr = viewSt.ExecuteReader(CommandBehavior.CloseConnection);
                while (rdr.Read())
                {
                    String fn = rdr.GetString(rdr.GetOrdinal("first name"));
                    String ln = rdr.GetString(rdr.GetOrdinal("last name"));
                    int y = rdr.GetInt32(rdr.GetOrdinal("Years"));

                    t.Rows.Add(fn, ln, y);
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

        protected void Home(object sender, EventArgs e)
        {
            Response.Redirect("SupervisorHome.aspx");
        }
    }
}