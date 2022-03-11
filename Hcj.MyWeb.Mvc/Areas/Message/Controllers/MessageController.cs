using Hcj.MyWeb.Dal.Models.BO;
using Hcj.MyWeb.Dal.Models.PO;
using Hcj.MyWeb.Dal.Services;
using Hcj.MyWeb.Mvc.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hcj.MyWeb.Mvc.Areas.Message.Controllers
{
    /// <summary>
    /// 留言板功能
    /// </summary>
    [Area("Message")]
    public class MessageController : BasePublicController
    {
        /// <summary>
        /// 主页面
        /// </summary>
        /// <returns></returns>
        public IActionResult MessageIndex()
        {
            return View();
        }

        readonly MessageServices service = new MessageServices();

        /// <summary>
        /// 查询留言
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult QueryMessage(MessageQueryRequest model) => Json(service.QueryMessage(model, UserInfo));

        /// <summary>
        /// 新增留言
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult AddMessage(TT_Hcj_Message_PO model) => Json(service.AddMessage(model, UserInfo));

    }
}
