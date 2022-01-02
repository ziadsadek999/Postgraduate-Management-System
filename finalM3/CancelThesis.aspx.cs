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
    public partial class CancelThesis : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            serialWarning.Text = "";
            suc.Text = "";
            String s = TextBox1.Text;
            if (s.Length == 0)
            {
                serialWarning.Text = "This feild is required";
                return;
            }
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] < '0' || s[i] > '9')
                {
                    serialWarning.Text = "Thesis serial number must consist of digits only";
                    return;
                }
            }
            String connStr = WebConfigurationManager.ConnectionStrings["postGrad"].ToString();

            SqlConnection conn = new SqlConnection(connStr);

            String serialNo = TextBox1.Text;

            SqlCommand check = new SqlCommand("checkThesisSup", conn);
            check.CommandType = CommandType.StoredProcedure;
            check.Parameters.Add(new SqlParameter("@ThesisSerialNo", Int32.Parse(serialNo)));
            check.Parameters.Add(new SqlParameter("@SupID", Int32.Parse(WelcomePage.Identity + "")));

            SqlParameter o = check.Parameters.Add("@out", SqlDbType.Bit);
            o.Direction = System.Data.ParameterDirection.Output;

            conn.Open();
            check.ExecuteNonQuery();
            conn.Close();

            if (o.Value.ToString() == "True")
            {

                SqlCommand cancelProc = new SqlCommand("CancelThesis", conn);
                cancelProc.CommandType = CommandType.StoredProcedure;

                cancelProc.Parameters.Add(new SqlParameter("@ThesisSerialNo", SqlDbType.Int)).Value = TextBox1.Text;
                SqlParameter sucess = cancelProc.Parameters.Add("@suc", SqlDbType.Bit);
                sucess.Direction = System.Data.ParameterDirection.Output;
                try
                {
                    conn.Open();
                    cancelProc.ExecuteNonQuery();
                    conn.Close();
                    if (sucess.Value.ToString() == "True")
                    {
                        suc.Text = "Thesis is cancelled successfully!";
                        suc.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        suc.Text = "Thesis cannot be cancelled as the evaluation of its last progress report is not zero";
                        suc.ForeColor = System.Drawing.Color.Red;
                    }
                }
                catch
                {
                    serialWarning.Text = "There is not any thesis with this serial number";
                }
            }
            else
            {
                serialWarning.Text = "You do not supervise any thesis with serial number";
            }
        }

        protected void Home(object sender, EventArgs e)
        {
            Response.Redirect("SupervisorHome.aspx");
        }
    }
}