using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Configuration;     
    

namespace D_HansSs_Villa
{
    public partial class Transaction : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
        VillaAccountManager accntMgr = new VillaAccountManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            string NoOfMembers;
            if (!Page.IsPostBack)
                NoOfMembers = accntMgr.Populate_Available_User_List(DropDownList1, User.Identity.Name.ToString());
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //Validations
            bool IsValid = true;
            if ((TextBox2.Text.Trim().Equals("")) ||(!IsValidCurrency(TextBox2.Text)))
            {
                IsValid = false;
                Label5.Visible = true;
                Label5.ForeColor = Color.Red;
                Label5.Text = "Please Enter Valid Amount.";
            }
            if (TextBox3.Text.Trim().Equals(""))
            {
                IsValid = false;
                Label5.Visible = true;
                Label5.ForeColor = Color.Red;
                Label5.Text = "Please Enter Proper Description.";
            }
            if(IsValid)
            {
                SqlConnection con = new SqlConnection(strcon);

                con.Open();
                int TransID = Random_Number();
                DateTime date = Calendar1.SelectedDate;
                if (Calendar1.SelectedDate.ToString().Equals("1/1/0001 12:00:00 AM"))
                {
                    date = DateTime.Now;
                }

                String str = "INSERT INTO aspnet_Transactions (TransactionID,Name,Amount,Description,Date) VALUES ('" + TransID.ToString() + "','" + DropDownList1.SelectedItem.Text.ToString().Trim() + "','" + TextBox2.Text.Trim() + "','" + TextBox3.Text.Trim() + "','" + date + "')";
                SqlCommand cmd = new SqlCommand(str, con);
                int OBJ = Convert.ToInt32(cmd.ExecuteNonQuery());
                Label5.Visible = true;
                string Details = "<br/>Your transaction ID is :" + TransID.ToString() + "<br/>Name : " + DropDownList1.SelectedItem.Text.ToString() + "<br/> Amount : " + TextBox2.Text.ToString() + "<br/> Details : " + TextBox3.Text.ToString() + "<br/> Date : " + date.ToString("D");
                if (OBJ > 0)
                {
                    Label5.ForeColor = Color.Blue;
                    Label5.Text = "Data is successfully inserted in database!!!" + Details;
                    Reset_Fields();
                }
                else
                {
                    Label5.ForeColor = Color.Red;
                    Label5.Text = "Data is not inserted in database!!!";
                }
                con.Close();
            }
        }
        
        protected bool IsValidCurrency(string strCurrencyInput)
        {
            string val = strCurrencyInput;
            string pattern = @"^\d+(\.\d\d)?$";
            Match match = Regex.Match(val.Trim(), pattern, RegexOptions.IgnoreCase);

            if (match.Success)
                return true;
            else
                return false;
        }
        protected int Random_Number()
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            int RandomNumber;
            RandomNumber = rand.Next(1000000, 9999999);
            return RandomNumber;
        }
        protected void Reset_Fields()
        {
            TextBox2.Text = "";
            TextBox3.Text = "";
        }
    }
}