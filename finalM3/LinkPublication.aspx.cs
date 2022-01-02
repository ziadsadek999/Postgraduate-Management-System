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
    public partial class LinkPublication : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Label1.Text = "";
            Label2.Text = "";
            Label3.Text = "";

            Boolean f = true;
            String connStr = WebConfigurationManager.ConnectionStrings["postGrad"].ToString();

            SqlConnection conn = new SqlConnection(connStr);

            String pubId = TextBox1.Text;
            String thNo = TextBox2.Text;
            if (pubId.Length == 0)
            {
                Label2.Text = "This field is required";
                f = false;
            }
            if (thNo.Length == 0)
            {
                Label3.Text = "This field is required";
                f = false;
            }
            for (int i = 0; i < TextBox1.Text.Length; i++)
            {
                if (TextBox1.Text[i] > '9' || TextBox1.Text[i] < '0')
                {
                    f = false;
                    Label2.Text = "Publication ID must consist of digits only";
                    break;
                }
            }
            for (int i = 0; i < TextBox2.Text.Length; i++)
            {
                if (TextBox2.Text[i] > '9' || TextBox2.Text[i] < '0')
                {
                    f = false;
                    Label3.Text = "Thesis serial number must consist of digits only";
                    break;
                }
            }
            if (!f)
                return;
            SqlCommand test = new SqlCommand("checkProgressReportIsMine", conn);
            test.CommandType = CommandType.StoredProcedure;
            test.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = WelcomePage.Identity;
            test.Parameters.Add(new SqlParameter("@serialNo", SqlDbType.Int)).Value = thNo;
            SqlParameter res = test.Parameters.Add("@res", SqlDbType.Bit);
            res.Direction = System.Data.ParameterDirection.Output;

            conn.Open();
            test.ExecuteNonQuery();
            conn.Close();

            if (res.Value.ToString() == "True")
            {

                SqlCommand testOngoing = new SqlCommand("checkOnGoing", conn);
                testOngoing.CommandType = CommandType.StoredProcedure;
                testOngoing.Parameters.Add(new SqlParameter("@serial", SqlDbType.Int)).Value = TextBox2.Text;
                try
                {
                    conn.Open();
                    testOngoing.ExecuteNonQuery();
                    conn.Close();
                }
                catch (SqlException ex)
                {
                    Label3.Text = ex.Message;
                    return;
                }


                SqlCommand test2 = new SqlCommand("alreadyLinked", conn);
                test2.CommandType = CommandType.StoredProcedure;
                test2.Parameters.Add(new SqlParameter("@thNo", SqlDbType.Int)).Value = thNo;
                test2.Parameters.Add(new SqlParameter("@pubId", SqlDbType.Int)).Value = pubId;
                SqlParameter res2 = test2.Parameters.Add("@res", SqlDbType.Bit);
                res2.Direction = System.Data.ParameterDirection.Output;

                conn.Open();
                test2.ExecuteNonQuery();
                conn.Close();
                if (res2.Value.ToString() == "True")
                {
                    Label1.Text = "This publication is already linked to this thesis";
                    return;
                }
                

                    SqlCommand linkProc = new SqlCommand("linkPubThesis", conn);
                linkProc.CommandType = CommandType.StoredProcedure;
                try
                {
                    linkProc.Parameters.Add(new SqlParameter("@PubID", SqlDbType.Int)).Value = pubId;
                    linkProc.Parameters.Add(new SqlParameter("@thesisSerialNo", SqlDbType.Int)).Value = thNo;
                    conn.Open();
                    linkProc.ExecuteNonQuery();
                    conn.Close();
                    Label1.Text = "Publication linked successfully!";
                }
                catch (Exception ex)
                {
                    Label2.Text = "There is not any publication with this ID";
                }
            }
            else
            {
                Label3.Text = "You are not registered to any thesis with this serial number";
            } 

        }

        protected void Home(object sender, EventArgs e)
        {
            Response.Redirect("StudentHome.aspx");
        }
    }
}