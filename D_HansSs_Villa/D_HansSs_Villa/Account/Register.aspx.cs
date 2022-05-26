using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace D_HansSs_Villa.Account
{
    public partial class Register : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterUser.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];
        }

        protected void RegisterUser_CreatedUser(object sender, EventArgs e)
        {
            FormsAuthentication.SetAuthCookie(RegisterUser.UserName, false /* createPersistentCookie */);

            // Get profile of registered user
            AccountProfile customuserProfile = (AccountProfile)AccountProfile.Create(RegisterUser.UserName, true);

            // update these custom fields to him/her
            customuserProfile.FullName = ((TextBox)RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("FullName")).Text;
            customuserProfile.MobileNumber = ((TextBox)RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("MobileNumber")).Text;

            customuserProfile.Save();
            string continueUrl = RegisterUser.ContinueDestinationPageUrl;
            if (String.IsNullOrEmpty(continueUrl))
            {
                continueUrl = "~/";
            }
            Response.Redirect(continueUrl);
        }
    }
}
