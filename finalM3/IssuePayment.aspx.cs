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
    public partial class IssuePayment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Issue(object sender, EventArgs e)
        {
            serialWarning.Text = "";
            AmountWarning.Text = "";
            noInstallmentWarning.Text = "";
            percentageWarning.Text = "";
            suc.Text = "";

            int Tserial =0;
            decimal amnt =0;
            int noins =0;
            decimal per =0;
            Boolean f = true; 
            try
            {
                Tserial = Int32.Parse(serial.Text);
            }
            catch
            {
                if (serial.Text.Length != 0)
                {
                    serialWarning.Text = "Serail Number must consist of digits only";
                }
                else
                {
                    serialWarning.Text = "This field is missing";
                }
                f = false;
            }
            try
            {
                noins = Int32.Parse(noInstallment.Text);
            }
            catch
            {
                if (noInstallment.Text.Length!=0) 
                { 
                    noInstallmentWarning.Text = "Number of Installments must be integer";
                }
                else
                {
                    noInstallmentWarning.Text = "This field is missing";
                }
                f = false;
            }
            try
            {
                amnt = decimal.Parse(amount.Text);
            }
            catch
            {
                if (amount.Text.Length != 0)
                {
                    AmountWarning.Text = "Amount must be decimal";
                }
                else {
                    AmountWarning.Text = "This field is missing";
                }
                
                f = false;
            }
            try
            {
                per = decimal.Parse(percentage.Text);
            }
            catch
            {
                if (percentage.Text.Length != 0)
                {
                    percentageWarning.Text = "percentage must be decimal";
                }
                else
                {
                    percentageWarning.Text = "This field is missing";
                }
                f = false;
            }

            if (!f) return;
            


            string connStr = WebConfigurationManager.ConnectionStrings["postGrad"].ToString();

            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand Issue = new SqlCommand("AdminIssueThesisPayment", conn);
            Issue.CommandType = System.Data.CommandType.StoredProcedure;


            Issue.Parameters.Add(new SqlParameter("@ThesisSerialNo", Tserial));
            Issue.Parameters.Add(new SqlParameter("@amount", amnt));
            Issue.Parameters.Add(new SqlParameter("@noOfInstallments", noins));
            Issue.Parameters.Add(new SqlParameter("@fundPercentage", per));

            SqlParameter success = Issue.Parameters.Add("@Success", SqlDbType.Bit);

            success.Direction = ParameterDirection.Output;

            conn.Open();
            Issue.ExecuteNonQuery();
            if (success.Value.ToString() == "True")
            {
                suc.Text = "Payment Issued Succesfully";
                suc.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                suc.Text = "There is no thesis with serial number "+ Tserial;
                suc.ForeColor = System.Drawing.Color.Red;
            }
            conn.Close();
        }

        protected void Home(object sender, EventArgs e)
        {
            Response.Redirect("Admin.aspx");
        }
    }
}