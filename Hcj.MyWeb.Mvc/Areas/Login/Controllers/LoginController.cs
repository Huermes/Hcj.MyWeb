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

        readonly LoginServices service = new LoginServices();

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
            var data = result.Data as TM_Hcj_User_PO;
            //登录认证，存入Cookie
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Sid,data.UserID.ToString()),
                    new Claim("UserNo", data.UserNO),
                    new Claim("UserName", data.UserName),
                    new Claim("Password", data.Password),
                    new Claim("UserPhoto", data.UserPhoto),
                    new Claim("IsAdmin", data.IsAdmin.ToString())
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

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult UpdateUser(TM_Hcj_User_PO model) => Json(service.UpdateUser(model));

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult LoginOut()
        {
            HttpContext.SignOutAsync().Wait();
            return Json(new { });
        }

    }
}
