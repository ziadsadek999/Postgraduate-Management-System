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
    public partial class Comment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Home(object sender, EventArgs e)
        {
            Response.Redirect("Examiner.aspx");
        }

        protected void Add_Comment(object sender, EventArgs e)
        {
            dateWrning.Text = "";
            serialWarning.Text = "";
            commentWarning.Text = "";
            suc.Text = "";
            string connStr = WebConfigurationManager.ConnectionStrings["postGrad"].ToString();

            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand Addcomment = new SqlCommand("AddCommentsGrade", conn);
            Addcomment.CommandType = System.Data.CommandType.StoredProcedure;

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

            int examid = (int)WelcomePage.Identity;

            SqlCommand check = new SqlCommand("checkThesisExam",conn);
            check.CommandType = System.Data. CommandType.StoredProcedure;

            check.Parameters.Add(new SqlParameter("@ThesisSerialNo", Tserial));
            check.Parameters.Add(new SqlParameter("@ExamID", examid));

            SqlParameter o = check.Parameters.Add("@out", SqlDbType.Bit);
            o.Direction = ParameterDirection.Output;
            conn.Open();

            check.ExecuteNonQuery();

            conn.Close();

            if(o.Value.ToString() == "False")
            {
                if (Serial.Text.Length != 0)
                {
                    suc.Text = "Serial number is not one of your defense thesis";
                }
                f = false;
                suc.ForeColor = System.Drawing.Color.Red;
            }

            string date = datepicker.SelectedDate.ToLongDateString();
            string comm = Comments.Text;

            if (comm.Length == 0)
            {
                commentWarning.Text = "This field is missing";
                f = false;
            }

            if (!f)
            {
                return;
            }

            
            Addcomment.Parameters.Add(new SqlParameter("@ThesisSerialNo", Tserial));
            Addcomment.Parameters.Add(new SqlParameter("@DefenseDate", SqlDbType.Date)).Value = date;
            Addcomment.Parameters.Add(new SqlParameter("@comments", comm));

            conn.Open();
            try
            {
                Addcomment.ExecuteNonQuery();
                suc.Text = "Comments added successfully";
                suc.ForeColor = System.Drawing.Color.Green;
            }
            catch
            {
                suc.Text = "There is no defense with this date";
                suc.ForeColor = System.Drawing.Color.Red;
            }
            conn.Close();
        }
    }
}