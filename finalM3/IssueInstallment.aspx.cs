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
    public partial class IssueInstallment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Home(object sender, EventArgs e)
        {
            Response.Redirect("Admin.aspx");
        }

        protected void Issue(object sender, EventArgs e)
        {
            dateWrning.Text = "";
            paymentWarning.Text = "";
            suc.Text = "";
            string connStr = WebConfigurationManager.ConnectionStrings["postGrad"].ToString();

            SqlConnection conn = new SqlConnection(connStr);
            Boolean f = true;
            if (datepicker.SelectedDate.ToLongDateString() == "Monday, January 1, 0001")
            {
                dateWrning.Text = "Please pick a date";
                f = false;
            }
            int PID = 0;
            try
            {
                PID = Int32.Parse(paymentID.Text);
            }
            catch
            {
                if(paymentID.Text.Length == 0)
                {
                    paymentWarning.Text = "This field is missing";
                }
                else
                {
                    paymentWarning.Text = "Payment ID must consist of digits";
                }
                f = false;
            }
            if (!f) return;

            string start = datepicker.SelectedDate.ToLongDateString();

            SqlCommand IssueInstall = new SqlCommand("AdminIssueInstallPayment", conn);
            IssueInstall.CommandType = System.Data.CommandType.StoredProcedure;

            IssueInstall.Parameters.Add(new SqlParameter("@paymentID", PID));
            IssueInstall.Parameters.Add(new SqlParameter("@InstallStartDate", SqlDbType.Date)).Value = start;

            conn.Open();
            try
            {
                IssueInstall.ExecuteNonQuery();
                suc.Text = "Installments issued successfully";
                suc.ForeColor = System.Drawing.Color.Green;
            }
            catch
            {
                suc.Text = "There is no Payment with ID " +PID;
                suc.ForeColor = System.Drawing.Color.Red;
            }
            conn.Close();
        }

    }
}