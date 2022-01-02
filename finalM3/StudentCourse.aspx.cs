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
    public partial class StudentCourse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["postGrad"].ToString();

            SqlConnection conn = new SqlConnection(connStr);


            try
            {

                SqlCommand viewCourse = new SqlCommand("ViewCoursesGrades", conn);
                viewCourse.CommandType = CommandType.StoredProcedure;
                viewCourse.Parameters.Add(new SqlParameter("@studentID", SqlDbType.Int)).Value = WelcomePage.Identity;
                DataTable t = new DataTable();
                t.Columns.AddRange(new DataColumn[3] { new DataColumn("Course code", typeof(string)),
                    new DataColumn("Course ID", typeof(int)),
                    new DataColumn("Grade",typeof(decimal))});
                conn.Open();
                SqlDataReader rdr = viewCourse.ExecuteReader(CommandBehavior.CloseConnection);
                while (rdr.Read())
                {
                    int id = rdr.GetInt32(rdr.GetOrdinal("id"));
                    String code = rdr.GetString(rdr.GetOrdinal("code"));
                    decimal grade = rdr.GetDecimal(rdr.GetOrdinal("grade"));
                    
                    t.Rows.Add(code,id,grade);
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
           catch(SqlException ex)
            {
                Label1.Text="You are a GUCian student you cannot enroll for courses";
            }
        }

        protected void Home(object sender, EventArgs e)
        {
            Response.Redirect("StudentHome.aspx");
        }
    }
}