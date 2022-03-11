using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Hcj.MyWeb.Dal.Models.PO
{
    /// <summary>
    /// 用户日志表
    /// </summary>
    public class TL_Hcj_UserLog_PO
    {
        /// <summary>
        /// 日志ID
        /// </summary>
        public int LogID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 日志类型 1 新增 2 修改 3 登录
        /// </summary>
        public int LogType { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string MobilePhone { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string UserPhoto { get; set; }

        /// <summary>
        /// 注册时的ip地址
        /// </summary>
        public string ipAddress { get; set; }

        /// <summary>
        /// 注册时的mac地址
        /// </summary>
        public string macAddress { get; set; }

    }
}
