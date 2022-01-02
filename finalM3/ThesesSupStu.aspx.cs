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
    public partial class ThesesSupStu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["postGrad"].ToString();

            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand Thesisinfo = new SqlCommand("ViewExamSupStu", conn);
            Thesisinfo.CommandType = System.Data.CommandType.StoredProcedure;

            Thesisinfo.Parameters.Add(new SqlParameter("@ExamID",WelcomePage.Identity));

            DataTable t = new DataTable();
            t.Columns.AddRange(new DataColumn[3] { new DataColumn("Title", typeof(string)),
                    new DataColumn("Supervisor_Name", typeof(string)),
                    new DataColumn("Student_Name",typeof(string))});

            conn.Open();
            SqlDataReader rdr = Thesisinfo.ExecuteReader(CommandBehavior.CloseConnection);

            while (rdr.Read())
            {
                string title = rdr.GetString(rdr.GetOrdinal("Title"));
                string supName = rdr.GetString(rdr.GetOrdinal("Supervisor_Name"));
                string stuName = rdr.GetString(rdr.GetOrdinal("Student_Name"));
                t.Rows.Add(title, supName, stuName);
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
                    sb.Append("<td style='width:200px;border: 1px solid #ccc'>" + row[column.ColumnName].ToString() + "</td>");
                }
                sb.Append("</tr>");
            }

            //Table end.
            sb.Append("</table>");
            Result.Text = sb.ToString();

        }

        protected void Home(object sender, EventArgs e)
        {
            Response.Redirect("Examiner.aspx");
        }
    }
}