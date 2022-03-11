using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Hcj.MyWeb.Dal.Models.PO
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class TM_Hcj_User_PO
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 登录号
        /// </summary>
        public string UserNO { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 1锁定
        /// </summary>
        public int Locked { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime LastLoginDate { get; set; }

        /// <summary>
        /// 是否管理员
        /// </summary>
        public int IsAdmin { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateDate { get; set; }

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
        public string IpAddress { get; set; }

        /// <summary>
        /// 注册时的mac地址
        /// </summary>
        public string MacAddress { get; set; }

    }
}
