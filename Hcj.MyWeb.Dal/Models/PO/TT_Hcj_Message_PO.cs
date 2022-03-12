using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Hcj.MyWeb.Dal.Models.PO
{
    /// <summary>
    /// 留言表
    /// </summary>
    public class TT_Hcj_Message_PO
    {
        /// <summary>
        /// ID
        /// </summary>
        public long MessageID { get; set; }

        /// <summary>
        /// 留言人ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 留言类型 0 私密 1 公开 
        /// </summary>
        public int MessageType { get; set; }

        /// <summary>
        /// 留言内容
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

    }
}
