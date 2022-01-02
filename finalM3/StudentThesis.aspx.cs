using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace finalM3
{
    public partial class StudentThesis : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["postGrad"].ToString();

            SqlConnection conn = new SqlConnection(connStr);


           
                SqlCommand viewTh = new SqlCommand("studentViewAllThesis", conn);
                viewTh.CommandType = CommandType.StoredProcedure;
                viewTh.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = WelcomePage.Identity;
                DataTable t = new DataTable();
                t.Columns.AddRange(new DataColumn[11] { new DataColumn("Serial number", typeof(int)),
                    new DataColumn("Feild", typeof(string)),
                    new DataColumn("Type",typeof(string)),
                new DataColumn("Title",typeof(string)),
                new DataColumn("Start",typeof(DateTime)),
                new DataColumn("End",typeof(DateTime)),
                new DataColumn("Defense date",typeof(DateTime)),
                new DataColumn("Years",typeof(int)),
                new DataColumn("Grade",typeof(string)),
                new DataColumn("Payment ID",typeof(string)),
                new DataColumn("Number of extensions",typeof(int))});
                conn.Open();
                SqlDataReader rdr = viewTh.ExecuteReader(CommandBehavior.CloseConnection);
                while (rdr.Read())
                {
                    int ser = rdr.GetInt32(rdr.GetOrdinal("serialNumber"));
                    string field = rdr.GetString(rdr.GetOrdinal("field"));
                string type = rdr.GetString(rdr.GetOrdinal("type"));
                string title = rdr.GetString(rdr.GetOrdinal("title"));
                DateTime start = rdr.GetDateTime(rdr.GetOrdinal("startDate"));
                DateTime end = rdr.GetDateTime(rdr.GetOrdinal("endDate"));
                DateTime def = rdr.GetDateTime(rdr.GetOrdinal("defenseDate"));
                int years = rdr.GetInt32(rdr.GetOrdinal("years"));
                string grade = "Not assigned";
                try
                {
                    grade = rdr.GetDecimal(rdr.GetOrdinal("grade"))+"";
                }
                catch
                {

                }
                string pay = "Not assigned";
                try
                {
                    pay = rdr.GetInt32(rdr.GetOrdinal("payment_id")) + "";
                }
                catch
                {

                }
               
                int ex = rdr.GetInt32(rdr.GetOrdinal("noExtension"));

                t.Rows.Add(ser,field,type,title,start,end,def,years,grade,pay,ex);
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
            Response.Redirect("StudentHome.aspx");
        }
    }
}