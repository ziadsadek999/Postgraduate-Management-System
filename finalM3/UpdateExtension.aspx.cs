using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace finalM3
{
    public partial class UpdateExtension : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Home(object sender, EventArgs e)
        {
            Response.Redirect("Admin.aspx");
        }

        protected void Update(object sender, EventArgs e)
        {
            serialWarning.Text = "";
            suc.Text = ""; 
            string connStr = WebConfigurationManager.ConnectionStrings["postGrad"].ToString();

            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand update = new SqlCommand("AdminUpdateExtension", conn);
            update.CommandType = System.Data.CommandType.StoredProcedure;
            int Tserial = 0;
            try {
                Tserial = Int32.Parse(Serial.Text);
            }
            catch
            {
                if (Serial.Text.Length != 0)
                {
                    serialWarning.Text = "Serial Number must consist of digits only";
                }
                else
                {
                    serialWarning.Text = "This field is missing";
                }
                return;
            }
                

            update.Parameters.Add(new SqlParameter("@ThesisSerialNo", Tserial));

            conn.Open();

            try
            {
                update.ExecuteNonQuery();
                suc.Text = "The number of extensions is updated successfully";
                suc.ForeColor = System.Drawing.Color.Green;
            }
            catch
            {
                suc.Text = "There is no thesis with this serial number";
                suc.ForeColor = System.Drawing.Color.Red;
            }
            conn.Close();
        }
    }
}