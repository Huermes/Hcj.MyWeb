using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Hcj.MyWeb.Dal.Models.PO
{
    /// <summary>
    /// 回复表
    /// </summary>
    public class TT_Hcj_MessageReply_PO
    {
        /// <summary>
        /// ID
        /// </summary>
        public long ReplyID { get; set; }

        /// <summary>
        /// 回复的留言ID
        /// </summary>
        public long MessageID { get; set; }

        /// <summary>
        /// 回复人ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 回复内容
        /// </summary>
        public string Message { get; set; }

    }
}
