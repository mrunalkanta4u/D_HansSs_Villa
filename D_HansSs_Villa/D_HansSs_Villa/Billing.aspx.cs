using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;
using System.Configuration;


namespace D_HansSs_Villa
{
    public partial class Billing : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
        VillaAccountManager accntMgr = new VillaAccountManager();
        // Event Handlers
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) 
                Label8.Text = accntMgr.Populate_Available_User_List(DropDownList1, User.Identity.Name.ToString());
            Label4.Text = accntMgr.Get_House_Rent().ToString();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            bool IsValid = true;

            if (Calendar1.SelectedDate.Month > DateTime.Now.Month)
            {
                IsValid = false;
                Label5.Visible = true;
                Label5.ForeColor = Color.Red;
                Label5.Text = "Can't generate Bill for Future Month.";
            }
            if (IsValid)
            {
                string billdetails = accntMgr.CalculateBill(Calendar1.SelectedDate, Convert.ToDouble(Label4.Text.ToString().Trim()), Convert.ToDouble(Label8.Text.ToString().Trim()), DropDownList1.SelectedValue.ToString(),true);
                if (billdetails.Equals("Minimum 2 members are required for calculation"))
                Label5.ForeColor = Color.Red;
                else
                Label5.ForeColor = Color.Blue;
                Label5.Text = billdetails;
                Label5.Visible = true;
            }
        }

        //Validators
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
    }   
}
