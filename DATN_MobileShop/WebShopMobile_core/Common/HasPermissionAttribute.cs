using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebShopMobile_core.Common
{
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public string RoleId { get; set; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var session = HttpContext.Current.Session[CommonAdmin.ADMIN_SESSION] as WebShopMobile_core.Common.AdminLogin;
            if (session == null)
            {
                return false;
            }
            List<string> privilegeLevels = this.GetCredentialByLoggedInUser(session.AdminName);
            if (privilegeLevels.Contains(this.RoleId) || session.GroupAdminId == CommonGroup.ADMIN_GROUP)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        private List<string> GetCredentialByLoggedInUser(string adminName)
        {
            var permissions = (List<string>)HttpContext.Current.Session[CommonAdmin.SESSION_PERMISSION];
            return permissions;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext authorization)
        {
            authorization.Result = new ViewResult()
            {
                ViewName = "~/Areas/Admin/Views/HomeAd/_401.cshtml"
            };
        }
    }
}
