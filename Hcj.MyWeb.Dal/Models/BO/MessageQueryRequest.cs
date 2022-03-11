using Hcj.MyWeb.Dal.Models.PO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hcj.MyWeb.Dal.Models.BO
{
    /// <summary>
    /// 查询BO
    /// </summary>
    public class MessageQueryRequest : TT_Hcj_Message_PO
    {
        /// <summary>
        /// 开始页码
        /// </summary>
        public int StartPage { get; set; }

        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 查询类型 1 我的留言 2 公共留言
        /// </summary>
        public int Type { get; set; }
    }
}
