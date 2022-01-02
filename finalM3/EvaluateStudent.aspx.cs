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
    public partial class EvaluateStudent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            evalWarning.Text = "";
            reportWarning.Text = "";
            serailWarning.Text = "";
            suc.Text = "";

            String serialNo = TextBox1.Text;
            String pNo = TextBox2.Text;
            String eval = TextBox3.Text;

            Boolean f = true;
            try
            {
                Int32.Parse(serialNo);
            }
            catch
            {
                if (serialNo.Length == 0)
                {
                    serailWarning.Text = "This field is missing";
                }
                else
                {
                    serailWarning.Text = "Serial number must consist of digits only";
                }
                f = false;
            }

            try
            {
                Int32.Parse(pNo);
            }
            catch
            {
                if(pNo.Length == 0)
                {
                    reportWarning.Text = "This field is missing";
                }
                else
                {
                    reportWarning.Text = "Progress report number must consist of digits only";
                }
                f = false;
            }

            try
            {
                int x = Int32.Parse(eval);
                if(x<0 || x > 3)
                {
                    evalWarning.Text = "Evaluation must be beteween 0 and 3";
                    f = false;
                }

            }
            catch
            {
                if (eval.Length == 0)
                {
                    evalWarning.Text = "This field is missing";
                }
                else
                {
                    evalWarning.Text = "Evaluation must be integer";
                }
                f = false;
            }

            if (!f) return;

            String connStr = WebConfigurationManager.ConnectionStrings["postGrad"].ToString();

            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand evalProc = new SqlCommand("EvaluateProgressReport", conn);
            evalProc.CommandType = CommandType.StoredProcedure;

            evalProc.Parameters.Add(new SqlParameter("@ThesisSerialNo", SqlDbType.Int)).Value = serialNo;
            evalProc.Parameters.Add(new SqlParameter("@progressReportNo", SqlDbType.Int)).Value = pNo;
            evalProc.Parameters.Add(new SqlParameter("@evaluation", SqlDbType.Int)).Value = eval;

            evalProc.Parameters.Add(new SqlParameter("@supervisorID ", SqlDbType.Int)).Value = WelcomePage.Identity;

            SqlCommand check = new SqlCommand("checkThesisSup", conn);
            check.CommandType = CommandType.StoredProcedure;
            check.Parameters.Add(new SqlParameter("@ThesisSerialNo", Int32.Parse(serialNo)));
            check.Parameters.Add(new SqlParameter("@SupID", Int32.Parse(WelcomePage.Identity + "")));

            SqlParameter o = check.Parameters.Add("@out", SqlDbType.Bit);
            o.Direction = System.Data.ParameterDirection.Output;

            conn.Open();
            check.ExecuteNonQuery();
            conn.Close();

            if (o.Value.ToString() != "True")
            {
                suc.Text = "Serial Number is not for any of your Thesis";
                suc.ForeColor = System.Drawing.Color.Red;
                return;
            }
            
            
            conn.Open();
            try
            {
                
                evalProc.ExecuteNonQuery();

                suc.Text = "Evaluation added successfully";
                suc.ForeColor = System.Drawing.Color.Green;
            }
            catch
            {

                suc.Text = "wrong progress report number";
                suc.ForeColor = System.Drawing.Color.Red;
            }
            conn.Close();
        }

        protected void Home(object sender, EventArgs e)
        {
            Response.Redirect("SupervisorHome.aspx");
        }
    }
}