using Hcj.MyWeb.Dal.Models.Common;
using Hcj.MyWeb.Dal.Models.PO;
using Hcj.MyWeb.Dal.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hcj.MyWeb.Mvc.Areas.Login.Controllers
{
    [Area("Login")]
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        LoginServices service = new LoginServices();

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult RegionUser(TM_Hcj_User_PO model) => Json(service.RegionUser(model));

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult LoginUser(TM_Hcj_User_PO model)
        {
            var result = service.LoginUser(model);
            if (!result.Flag)
            {
                return Json(result);
            }
            //登录认证，存入Cookie
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,result.Data.UserID),
                new Claim("UserNo",result.Data.UserNo),
                new Claim("UserName",result.Data.UserName),
                new Claim("Password",result.Data.Password),
                new Claim("IsAdmin",result.Data.IsAdmin)
            };
            //init the identity instances 
            var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Customer"));
            //signin 
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal, new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                IsPersistent = false,
                AllowRefresh = false
            });
            return Json(result);
        }

    }
}
