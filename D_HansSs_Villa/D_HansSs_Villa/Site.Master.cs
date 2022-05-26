using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace D_HansSs_Villa
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
