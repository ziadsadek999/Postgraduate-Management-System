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
    public partial class AddExaminer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            nationalWarning.Text = "";
            fieldWarning.Text = "";
            nameWarning.Text = "";
            serialWarning.Text = "";
            suc.Text = "";
            String serialNo = TextBox1.Text;
            String date = datepicker.SelectedDate.ToLongDateString();
            String name = TextBox3.Text;
            String field = TextBox4.Text;
            Boolean f = true;
            try
            {
                Int32.Parse(serialNo);
            }
            catch
            {
                if (serialNo.Length == 0)
                {
                    serialWarning.Text = "This field is missing";
                }
                else
                {
                    serialWarning.Text = "Serial Number must consist of digits only";
                }
                f = false;
            }

            if (datepicker.SelectedDate.ToLongDateString() == "Monday, January 1, 0001")
            {
                dateWrning.Text = "Please pick a date";
                f = false;
            }

            if (name.Length == 0)
            {
                nameWarning.Text = "This field is missing";
                f = false;
            }

            if (field.Length == 0)
            {
                fieldWarning.Text = "This field is missing";
                f = false;
            }

          


            String connStr = WebConfigurationManager.ConnectionStrings["postGrad"].ToString();

            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand addProc = new SqlCommand("AddExaminer", conn);
            addProc.CommandType = CommandType.StoredProcedure;

            addProc.Parameters.Add(new SqlParameter("@ThesisSerialNo", SqlDbType.Int)).Value = serialNo;
            addProc.Parameters.Add(new SqlParameter("@DefenseDate", SqlDbType.Date)).Value = date;
            addProc.Parameters.Add(new SqlParameter("@ExaminerName", SqlDbType.VarChar)).Value = name;
            addProc.Parameters.Add(new SqlParameter("@fieldOfWork", SqlDbType.VarChar)).Value = field;
            if (RadioButton1.Checked)
            {
                addProc.Parameters.Add(new SqlParameter("@National", SqlDbType.Bit)).Value = "true";
            }
            else
            {
                if (RadioButton2.Checked)
                {
                    addProc.Parameters.Add(new SqlParameter("@National", SqlDbType.Bit)).Value = "false";
                }
                else
                {
                    nationalWarning.Text = "choose Nationality status";
                    f = false;
                }
            }
            if (!f) return;

            SqlCommand check = new SqlCommand("checkThesisSup", conn);
            check.CommandType = CommandType.StoredProcedure;
            check.Parameters.Add(new SqlParameter("@ThesisSerialNo", Int32.Parse(serialNo)));
            check.Parameters.Add(new SqlParameter("@SupID", Int32.Parse(WelcomePage.Identity+"")));

            SqlParameter o = check.Parameters.Add("@out", SqlDbType.Bit);
            o.Direction = System.Data.ParameterDirection.Output;

            conn.Open();
            check.ExecuteNonQuery();
            conn.Close();

            if (o.Value.ToString() != "True")
            {
                suc.Text = "serial Number is not for any of your Thesis";
                suc.ForeColor = System.Drawing.Color.Red;
                return;
            }

            conn.Open();

            try
            {
                addProc.ExecuteNonQuery();
                suc.Text = "Examiner added Succesfully";
                suc.ForeColor = System.Drawing.Color.Green;
            }
            catch
            {
                suc.Text = "There is no defense with this date and thesis serial number";
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