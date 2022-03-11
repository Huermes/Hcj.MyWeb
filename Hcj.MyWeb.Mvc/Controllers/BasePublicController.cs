using Hcj.MyWeb.Dal.Models.PO;
using Hcj.MyWeb.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hcj.MyWeb.Mvc.Controllers
{

    [Authorize]
    public class BasePublicController : Controller
    {
        /// <summary>
        /// 获取用户对象
        /// </summary>
        public virtual TM_Hcj_User_PO UserInfo
        {
            get
            {
                return new TM_Hcj_User_PO
                {
                    UserID = int.Parse(User.FindFirstValue(ClaimTypes.Sid)),
                    UserNO = User.FindFirstValue("UserNo"),
                    UserName = User.FindFirstValue("UserName"),
                    Password = User.FindFirstValue("Password"),
                    IsAdmin = int.Parse(User.FindFirstValue("IsAdmin")),
                };
            }
        }
    }
}
