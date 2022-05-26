using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace D_HansSs_Villa
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.IsInRole("Administrator"))
            {
                Response.Redirect("~/Admin/AdminPage.aspx");
            }

            if (HttpContext.Current.User.Identity.IsAuthenticated.Equals(true))
            {
                Response.Redirect("~/Options.aspx");
            }
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

            }
        }
    }
}
