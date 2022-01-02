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
    public partial class Thesis : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["postGrad"].ToString();

            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand ListThesis = new SqlCommand("AdminViewAllTheses", conn);
            ListThesis.CommandType = System.Data.CommandType.StoredProcedure;

            SqlCommand onGoingThesis = new SqlCommand("AdminViewOnGoingTheses", conn);
            onGoingThesis.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter count = onGoingThesis.Parameters.Add("@thesesCount", SqlDbType.Int);
            count.Direction = ParameterDirection.Output;

            DataTable t = new DataTable();
            t.Columns.AddRange(new DataColumn[11] { new DataColumn("serialNumber", typeof(int)),
                    new DataColumn("field", typeof(string)),
                    new DataColumn("type",typeof(string)),
                    new DataColumn("title",typeof(string)),
                    new DataColumn("startDate",typeof(DateTime)),
                    new DataColumn("endDate",typeof(DateTime)),
                    new DataColumn("defenseDate",typeof(DateTime)),
                    new DataColumn("years",typeof(int)),
                    new DataColumn("grade",typeof(string)),
                    new DataColumn("payment_id",typeof(string)),
                    new DataColumn("noExtension",typeof(int))});
            conn.Open();
            onGoingThesis.ExecuteNonQuery();
            SqlDataReader rdr = ListThesis.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                int serialNumber = rdr.GetInt32(rdr.GetOrdinal("serialNumber"));
                string field = rdr.GetString(rdr.GetOrdinal("field"));
                string type = rdr.GetString(rdr.GetOrdinal("type"));
                string title = rdr.GetString(rdr.GetOrdinal("title"));
                DateTime start = rdr.GetDateTime(rdr.GetOrdinal("startDate"));
                DateTime endDate = rdr.GetDateTime(rdr.GetOrdinal("endDate"));
                DateTime defenseDate = rdr.GetDateTime(rdr.GetOrdinal("defenseDate"));
                int years = rdr.GetInt32(rdr.GetOrdinal("years"));
                String grade;
                try
                {
                   grade = rdr.GetDecimal(rdr.GetOrdinal("grade")) + "";
                }
                catch
                {
                    grade = "Not finished yet";
                }
                string payid = "";
                try
                {
                   payid = rdr.GetInt32(rdr.GetOrdinal("payment_id"))+"";
                }
                catch
                {
                    payid = "Not issued yet";
                }
                int noex = rdr.GetInt32(rdr.GetOrdinal("noExtension"));
                t.Rows.Add(serialNumber, field, type, title, start, endDate, defenseDate, years, grade, payid, noex);

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
            ThesisTable.Text = sb.ToString();

            onGoing.Text = "Number of on going thesis: " + count.Value.ToString();
        }

        protected void Home(object sender, EventArgs e)
        {
            Response.Redirect("Admin.aspx");
        }
    }
}