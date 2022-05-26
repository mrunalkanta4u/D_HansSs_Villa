using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
namespace D_HansSs_Villa.Admin
{
    public partial class AdminPage : System.Web.UI.Page
    {
        VillaAccountManager accntMgr = new VillaAccountManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            string NoOfMembers;
            if(!Page.IsPostBack)
                NoOfMembers = accntMgr.Populate_Available_User_List(DropDownList1, User.Identity.Name.ToString());
        }
        protected void Page_Unoad(object sender, EventArgs e)
        {
            
        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {     
            
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            string sFileName = accntMgr.CreateQueryResult(TextBox3.Text.ToString(), Server.MapPath(".."),false);

            System.IO.FileStream fs = null;
            fs = System.IO.File.Open(Server.MapPath("\\Temp\\" + sFileName), System.IO.FileMode.Open);
            byte[] btFile = new byte[fs.Length];
            fs.Read(btFile, 0, Convert.ToInt32(fs.Length));
            fs.Close();
            Response.AddHeader("Content-disposition", "attachment; filename=" + sFileName);
            Response.ContentType = "application/octet-stream";
            Response.BinaryWrite(btFile);
            Response.End();

            //HyperLink1.NavigateUrl = accntMgr.CreateQueryResult(TextBox3.Text.ToString(), Server.MapPath(".."));
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string name = DropDownList1.SelectedItem.Value.ToString();
            string result = "";
            if (name.Equals("Admin"))
            {
                result = accntMgr.InformAll('t', Server.MapPath(".."));
            }
            else
            {
                double NoOfMembers = Convert.ToDouble( DropDownList1.Items.Count - 1);
                string smsTo = accntMgr.getMobileNo(name);
                string smsBody = "Hi " + name + "!!! Your montly bill Details: " + accntMgr.CalculateBill(DateTime.Now, accntMgr.Get_House_Rent(), NoOfMembers, name, false);
                result = accntMgr.Send_Message("9590942794", "Villa123", smsBody, smsTo);
            }
            if (result.Equals("success"))
            {
                Label2.ForeColor = Color.Blue;
                Label2.Text = "Message sent successfully!!!";
            }
            else
            {
                Label2.ForeColor = Color.Red;
                Label2.Text = result;
            }
            Label2.Visible = true;
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            string name = DropDownList1.SelectedItem.Value.ToString();
            string result = "";
            if (name.Equals("Admin"))
            {
                result = accntMgr.InformAll('m', Server.MapPath("..").ToString());
            }
            else
            {
                double NoOfMembers = Convert.ToDouble( DropDownList1.Items.Count - 1);
                string to = accntMgr.getEmailID(name);
                string subject = "Monthly bill for : " + name;
                string attachment = accntMgr.CreateQueryResult("SELECT * FROM aspnet_Transactions ORDER BY Date", Server.MapPath(".."),true);
                string body = "Hi " + name + "!!! Your montly bill is as follows: <br>" + accntMgr.CalculateBill(DateTime.Now, accntMgr.Get_House_Rent(), NoOfMembers, name, true);
                result = accntMgr.Send_Mail(to, subject, body, attachment);
            }
            if (result.Equals("success"))
            {
                Label2.ForeColor = Color.Blue;
                Label2.Text = "Mail sent successfully!!!";
            }
            else
            {
                Label2.ForeColor = Color.Red;
                Label2.Text = result;
            }
            Label2.Visible = true;
        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            int result = accntMgr.Set_House_Rent(TextBox1.Text);
            if (result < 0)
            {
                Label2.ForeColor = Color.Red;
                Label2.Text = "House rent Could not be set. Please Re-enter :";
            }
            else
            {
                Label2.ForeColor = Color.Blue;
                Label2.Text = "House rent has been set.";
            }
            Label2.Visible = true;
        }
        protected void Page_UnLoad(object sender, EventArgs e)
        {
            try
            {
                System.IO.DirectoryInfo directory = new System.IO.DirectoryInfo(Server.MapPath(".") + "\\Temp");
                foreach (System.IO.FileInfo file in directory.GetFiles())
                    file.Delete();
            }

            catch (Exception ex)
            {
                Label2.ForeColor = Color.Red;
                Label2.Text = ex.Message;
            }
        }
    }
}