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
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace D_HansSs_Villa
{
    public class VillaAccountManager
    {
        string strcon = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
        public string CreateQueryResult(string Query, string filePath, bool fullyQualifiedPath)
        {
            string fileExcel, fileName, strLine, sql;
            FileStream objFileStream;
            StreamWriter objStreamWriter;
            Random nRandom = new Random(DateTime.Now.Millisecond);
            SqlConnection cnn = new SqlConnection(strcon);

            fileExcel = "t" + nRandom.Next().ToString() + ".xls";
            fileName = filePath + "\\Temp\\" + fileExcel;

            objFileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
            objStreamWriter = new StreamWriter(objFileStream);

            cnn.Open();
            sql = Query;
            SqlCommand cmd = new SqlCommand(sql, cnn);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            strLine = "";

            for (int i = 0; i <= dr.FieldCount - 1; i++)
            {
                strLine = strLine + dr.GetName(i).ToString() + Convert.ToChar(9);
            }

            objStreamWriter.WriteLine(strLine);

            strLine = "";

            while (dr.Read())
            {
                for (int i = 0; i <= dr.FieldCount - 1; i++)
                {
                    strLine = strLine + dr.GetValue(i).ToString() + Convert.ToChar(9);
                }
                objStreamWriter.WriteLine(strLine);
                strLine = "";
            }

            dr.Close();
            cnn.Close();
            objStreamWriter.Close();
            objFileStream.Close();

            if (fullyQualifiedPath)
                return fileName;
            else
                return fileExcel;
        }

        public string getEmailID(string name)
        {
            SqlConnection cnn = new SqlConnection(strcon);

            cnn.Open();
            string query = "select Email from aspnet_Membership where UserId = (select UserId from aspnet_Users where UserName = '" + name + "')";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataReader dr;
            string mailId = "";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                for (int i = 0; i <= dr.FieldCount - 1; i++)
                {
                    mailId = dr.GetValue(i).ToString();
                }
            }
            dr.Close();
            cnn.Close();
            return mailId;
        }

        public string getMobileNo(string name)
        {
            AccountProfile ap = new AccountProfile();
            AccountProfile prof = ap.GetProfile(name);
            return(prof.MobileNumber.ToString());
        }

        public string Send_Mail(string to, string subject, string body, string attachment)
        {
            try
            {
                string from = "villaaccntmanager@gmail.com";
                MailMessage MyMailMessage = new MailMessage(from, to, subject, body);
                

                Attachment data = new Attachment(attachment, MediaTypeNames.Application.Octet);
                // Add time stamp information for the file.
                ContentDisposition disposition = data.ContentDisposition;
                disposition.CreationDate = System.IO.File.GetCreationTime(attachment);
                disposition.ModificationDate = System.IO.File.GetLastWriteTime(attachment);
                disposition.ReadDate = System.IO.File.GetLastAccessTime(attachment);
                // Add the file attachment to this e-mail message.
                
                if (!attachment.Equals(""))
                    MyMailMessage.Attachments.Add(data);

                MyMailMessage.IsBodyHtml = true;

                //Proper Authentication Details need to be passed when sending email from gmail
                System.Net.NetworkCredential mailAuthentication = new
                System.Net.NetworkCredential("villaaccntmanager@gmail.com", "Villa@123");

                //Smtp Mail server of Gmail is "smpt.gmail.com" and it uses port no. 587
                System.Net.Mail.SmtpClient mailClient = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);

                //Enable SSL
                mailClient.EnableSsl = true;
                mailClient.UseDefaultCredentials = false;
                mailClient.Credentials = mailAuthentication;
                mailClient.Send(MyMailMessage);
                return "success";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Send Email Failed: " + ex.Message);
                return ex.Message;
            }
        }
        public string Send_Message(string uid, string password, string message, string no)
        {
            try
            {
                string reqString = "http://ubaid.tk/sms/sms.aspx?uid=" + uid + "&pwd=" + password + "&msg=" + message + " &phone=" + no + "&provider=site2sms";
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(reqString);
                HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                string responseString = respStreamReader.ReadToEnd();
                respStreamReader.Close();
                myResp.Close();
                return "success";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Send Message Failed: " + ex.Message);
                return ex.Message;
            }
        }
        public string Populate_Available_User_List(DropDownList DropDownList1, string loggedInUser)
        {

            DataTable payees = new DataTable();
            using (SqlConnection con = new SqlConnection(strcon))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT DISTINCT UserName FROM aspnet_users", con);
                    adapter.Fill(payees);

                    DropDownList1.DataSource = payees;
                    DropDownList1.DataTextField = "UserName";
                    DropDownList1.DataValueField = "UserName";
                    DropDownList1.DataBind();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occured: " + ex.Message);
                }
            }

            if (!loggedInUser.Equals("Admin"))
                DropDownList1.Items.Remove("Admin");
            DropDownList1.Items.FindByText(loggedInUser).Selected = true;

            return DropDownList1.Items.Count.ToString();
        }

        public string CalculateBill( DateTime SelectedDate, double HouseRent, double noOfMembers, string Name, bool IsHtmlText)
        {
            double total_expenditure = 0;
            double my_expenditure = 0;
            double total_expenditure_per_head = 0;
            double my_share = 0;
            string query1, query2, newline;
            DateTime firstDayOfMonth, lastDayOfMonth;
            SqlConnection cnn = new SqlConnection(strcon);

            cnn.Open();
            DateTime date;
            if (SelectedDate.ToString().Equals("1/1/0001 12:00:00 AM"))
                date = DateTime.Now;
            else
                date = SelectedDate;
            firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            string frstDay = firstDayOfMonth.ToString("MM/dd/yyyy");
            string lstDay = lastDayOfMonth.ToString("MM/dd/yyyy");
            query1 = "SELECT Amount FROM aspnet_Transactions WHERE Date BETWEEN '" + frstDay + "' AND '" + lstDay + "'";
            query2 = "SELECT Amount FROM aspnet_Transactions WHERE (Date BETWEEN '" + frstDay + "' AND '" + lstDay + "') AND Name = '" + Name + "'";

            SqlCommand cmd = new SqlCommand(query1, cnn);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                for (int i = 0; i <= dr.FieldCount - 1; i++)
                {
                    total_expenditure += Convert.ToDouble(dr["Amount"]);
                }
            }
            dr.Close();

            SqlCommand cmd1 = new SqlCommand(query2, cnn);
            SqlDataReader dr1 = cmd1.ExecuteReader();

             while (dr1.Read())
            {
                for (int i = 0; i <= dr1.FieldCount - 1; i++)
                {
                    my_expenditure += Convert.ToDouble(dr1["Amount"]);
                }
            }
            dr1.Close();
            total_expenditure = total_expenditure + HouseRent;
            if (noOfMembers > 2)
            {
                total_expenditure_per_head = total_expenditure / noOfMembers;
            }
            else
            {
                return  "Atleast 2 members are required for Billing";
            }
            my_share = total_expenditure_per_head - my_expenditure;
            if (IsHtmlText)
                newline = "<br/>";
            else
                newline = " ";

            string share = "";
            string Details = "Total Expenditure of the month: " + total_expenditure.ToString() + newline + "Expenditure Per Head: " + total_expenditure_per_head.ToString() + newline + "Your Expenditure: " + my_expenditure.ToString();
            if (my_share > 0)
            {
                share = newline + "You will Pay Rs. " + my_share.ToString();
            }
            else if (my_share < 0)
            {
                my_share = my_share * (-1);
                share = newline + "You will Get Rs. " + my_share.ToString();
            }
            else
            {
                share = newline + "Your bill is settled !!!";
            }
            cnn.Close();
            return (Details + share);
        }
        public void write_log(string data, string fileName)
        {
            string strLine;
            FileStream objFileStream;
            StreamWriter objStreamWriter;

            objFileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
            objStreamWriter = new StreamWriter(objFileStream);
            strLine = DateTime.Now.ToString() + " : " + data;

            objStreamWriter.WriteLine(strLine);
            objStreamWriter.Close();
            objFileStream.Close();
        }
        public int Set_House_Rent(string rent)
        {
            SqlConnection cnn = new SqlConnection(strcon);
            cnn.Open();
            string query = "UPDATE aspnet_Extras SET Houserent = " + rent;
            SqlCommand cmd = new SqlCommand(query, cnn);
            int OBJ = Convert.ToInt32(cmd.ExecuteNonQuery());           
            cnn.Close();
            if (OBJ < 0)
                return -1;
            else
                return 1;
        }
        
        public double Get_House_Rent() 
        {
            SqlConnection cnn = new SqlConnection(strcon);
            double HouseRent = 0;
            cnn.Open();
            string query = "select HouseRent from aspnet_Extras";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                for (int i = 0; i <= dr.FieldCount - 1; i++)
                {
                    HouseRent = Convert.ToDouble(dr["HouseRent"]);
                }
            }
            dr.Close();
            cnn.Close();
            return HouseRent;
        }
        public string InformAll(char option, string tempPath)
        {
            // option : 'm' = Inform By mail, 't' = Inform By Text, any other charecter like 'a' = both mail and text

            int NoOfMembers = 0;
            string name, to, subject, body, smsTo, smsBody, attachment;
            DataTable payees = new DataTable();

            using (SqlConnection con = new SqlConnection(strcon))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT DISTINCT UserName FROM aspnet_users", con);
                    adapter.Fill(payees);
                    List<DataRow> list = new List<DataRow>();
                    foreach (DataRow dr in payees.Rows)
                    {
                        list.Add(dr);
                        NoOfMembers ++;
                    }
                    NoOfMembers--; // Admin is not a member
                    attachment = this.CreateQueryResult("SELECT * FROM aspnet_Transactions ORDER BY Date", tempPath, true);
                    foreach (DataRow username in list)
                    {
                        name = username["UserName"].ToString();
                        if (!name.Equals("Admin"))
                        {
                            to = getEmailID(name);
                            smsTo = getMobileNo(name);
                            subject = "Monthly bill for : " + name;
                            body = "Hi " + name + "!!! Your montly bill is as follows: <br>" + this.CalculateBill(DateTime.Now, Get_House_Rent(), NoOfMembers, name, true);
                            smsBody = "Hi " + name + "!!! Your montly bill Details: " + this.CalculateBill(DateTime.Now, Get_House_Rent(), NoOfMembers, name, false);
                            if (!option.Equals('t'))
                                Send_Mail(to, subject, body, attachment);
                            if (!option.Equals('m'))
                                Send_Message("9590942794", "Villa123", smsBody, smsTo);
                        }
                    }
                    return "success";
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occured: " + ex.Message);
                    return ex.Message;
                }
            }
        }
    }
}