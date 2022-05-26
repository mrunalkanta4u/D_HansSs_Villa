using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.Security;
namespace D_HansSs_Villa
{
    public class AccountProfile : ProfileBase
    {
        public string FullName
        {
            get { return ((string)(base["FullName"])); }
            set { base["FullName"] = value; Save(); }
        }

        public string MobileNumber
        {
            get { return ((string)(base["MobileNumber"])); }
            set { base["MobileNumber"] = value; Save(); }
        }
        public virtual AccountProfile GetProfile(string username)
        {
            return ((AccountProfile)(ProfileBase.Create(username)));
        }
    }
}