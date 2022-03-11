using Dapper;
using Hcj.MyWeb.Dal.Models.BO;
using Hcj.MyWeb.Dal.Models.Common;
using Hcj.MyWeb.Dal.Models.PO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Hcj.MyWeb.Dal.Services
{

    /// <summary>
    /// 登录注册业务层
    /// </summary>
    public class MessageServices
    {
        public string Connection = AppConfigurtaion.Configuration["appSettings:Connection"];

        /// <summary>
        /// 查询留言板
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public BaseResponse QueryMessage(MessageQueryRequest model, TM_Hcj_User_PO userModel)
        {
            var result = new BaseResponse
            {
                Data = new MessageQueryResponse()
            };
            try
            {
                // 必填项验证
                if (userModel == null)
                {
                    result.Message = "登录过期，请重新登录";
                    return result;
                }
                if (model.Type != 1 && model.Type != 2)
                {
                    result.Message = "查询类型有问题";
                    return result;
                }
                if (model.StartPage < 0)
                {
                    model.StartPage = 0;
                }
                if (model.PageSize > 100)
                {
                    model.PageSize = 100;
                }

                int total = 0;
                using (var masterConn = new MySqlConnection(Connection))
                {
                    string sqlCount = model.Type switch
                    {
                        1 => $"select count(1) from tt_hcj_message where UserID={userModel.UserID}",
                        2 => "select count(1) from tt_hcj_message where MessageType=1",
                        _ => ""
                    };
                    // 查询所有人公开的留言 2
                    // 查询我的留言 1
                    string sql = model.Type switch
                    {
                        1 => $"select * from tt_hcj_message where UserID={userModel.UserID} limit @StartPage,@PageSize",
                        2 => "select * from tt_hcj_message where MessageType=1 limit @StartPage,@PageSize",
                        _ => ""
                    };
                    total = masterConn.QueryFirst<int>(sqlCount, null, commandTimeout: 300);
                    if (total <= 0)
                    {
                        result.Message = "未找到数据";
                        return result;
                    }
                    result.Data = masterConn.QueryFirstOrDefault<MessageQueryResponse>(sql, model, commandTimeout: 300);
                }
                result.Data.Total = total;
                // 返回
                result.Flag = true;
            }
            catch (Exception ex)
            {
                result.Flag = false;
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 留言
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public BaseResponse AddMessage(TT_Hcj_Message_PO model, TM_Hcj_User_PO userModel)
        {
            var result = new BaseResponse();
            try
            {
                // 必填项验证
                if (userModel == null)
                {
                    result.Message = "登录过期，请重新登录";
                    return result;
                }
                if (string.IsNullOrEmpty(model.Message?.Trim()))
                {
                    result.Message = "留言内容不能为空";
                    return result;
                }
                if (model.MessageType != 0 && model.MessageType != 1)
                {
                    result.Message = "请选择留言类型";
                    return result;
                }
                if (model.Message.Length > 1999)
                {
                    result.Message = "留言内容不能超过1999个字符";
                    return result;
                }
                model.UserID = userModel.UserID;
                // 插入留言表
                using (var masterConn = new MySqlConnection(Connection))
                {
                    masterConn.Execute("INSERT INTO tt_hcj_message(UserID,MessageType,Message)VALUES(@UserID,@MessageType,@Message);", model, commandTimeout: 300);
                }
                result.Flag = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
