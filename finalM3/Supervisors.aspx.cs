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
    public partial class Supervisors : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["postGrad"].ToString();

            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand ListSup = new SqlCommand("AdminListSup", conn);
            ListSup.CommandType = System.Data.CommandType.StoredProcedure;

            DataTable t = new DataTable();
            t.Columns.AddRange(new DataColumn[4] { new DataColumn("id", typeof(int)),
                    new DataColumn("Name", typeof(string)),
                    new DataColumn("Email",typeof(string)),
                    new DataColumn("faculty",typeof(string))});

            
            conn.Open();            
            SqlDataReader rdr = ListSup.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                int id = rdr.GetInt32(rdr.GetOrdinal("id"));
                String email = rdr.GetString(rdr.GetOrdinal("email"));
                String name = rdr.GetString(rdr.GetOrdinal("name"));
                String faculty = rdr.GetString(rdr.GetOrdinal("faculty"));
                t.Rows.Add(id, name, email, faculty);
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
            Response.Redirect("Admin.aspx");
        }
    }
}