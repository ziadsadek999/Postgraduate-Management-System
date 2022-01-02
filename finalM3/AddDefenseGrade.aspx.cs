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
    public partial class AddDefenseGrade : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Home(object sender, EventArgs e)
        {
            Response.Redirect("Examiner.aspx");
        }

        protected void Add_Grade(object sender, EventArgs e)
        {
            serialWarning.Text = "";
            GradeWarning.Text = "";
            dateWrning.Text = "";
            suc.Text = "";
            string connStr = WebConfigurationManager.ConnectionStrings["postGrad"].ToString();

            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand AddGrade = new SqlCommand("AddDefenseGrade", conn);
            AddGrade.CommandType = System.Data.CommandType.StoredProcedure;


            Boolean f = true;
            if (datepicker.SelectedDate.ToLongDateString() == "Monday, January 1, 0001")
            {
                dateWrning.Text = "Please pick a date";
                f = false;
            }

            int Tserial = 0;
            try
            {
                Tserial = Int32.Parse(Serial.Text);
            }
            catch
            {
                if (Serial.Text.Length == 0)
                {
                    serialWarning.Text = "This Field is missing";
                }
                else
                {
                    serialWarning.Text = "Serial Number must consist of digits";
                }
                f = false;
            }
            string date = datepicker.SelectedDate.ToLongDateString();
            Decimal grade = 0;
            try
            {
                grade = decimal.Parse(Grade.Text);
            }
            catch
            {
                if(Grade.Text.Length == 0)
                {
                    GradeWarning.Text = "This field is missing";
                }
                else
                {
                    GradeWarning.Text = "The grade must be decimal";
                }
                f = false;
            }

            if (grade > 100 || grade < 0)
            {
                f = false;
                GradeWarning.Text = "Grade must be between 0 and 100";
            }

            int examid = (int)WelcomePage.Identity;

            SqlCommand check = new SqlCommand("checkThesisExam", conn);
            check.CommandType = System.Data.CommandType.StoredProcedure;

            check.Parameters.Add(new SqlParameter("@ThesisSerialNo", Tserial));
            check.Parameters.Add(new SqlParameter("@ExamID", examid));

            SqlParameter o = check.Parameters.Add("@out", SqlDbType.Bit);
            o.Direction = ParameterDirection.Output;
            conn.Open();

            check.ExecuteNonQuery();

            conn.Close();

            if (o.Value.ToString() == "False")
            {
                if (Serial.Text.Length != 0)
                {
                    suc.Text = "Serial number is not for one of your defense thesis";
                }
                f = false;
                suc.ForeColor = System.Drawing.Color.Red;
            }

            if (!f) return;

            AddGrade.Parameters.Add(new SqlParameter("@ThesisSerialNo", Tserial));
            AddGrade.Parameters.Add(new SqlParameter("@DefenseDate", SqlDbType.Date)).Value = date;
            AddGrade.Parameters.Add(new SqlParameter("@grade", grade));
            conn.Open();
            
            try
            {
                AddGrade.ExecuteNonQuery();
                suc.Text = "Grade has been added Successfully";
                suc.ForeColor = System.Drawing.Color.Green;
            }
            catch
            {
                suc.Text = "There is no defense with this date for this thesis";
                suc.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}