using Dapper;
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
    public class LoginServices
    {
        public string Connection = AppConfigurtaion.Configuration["appSettings:Connection"];

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseResponse RegionUser(TM_Hcj_User_PO model)
        {
            var result = new BaseResponse();
            try
            {
                // 必填项验证
                if (string.IsNullOrEmpty(model.UserNO))
                {
                    result.Message = "用户账号必填";
                    return result;
                }
                if (string.IsNullOrEmpty(model.Password))
                {
                    result.Message = "用户密码必填";
                    return result;
                }
                if (string.IsNullOrEmpty(model.UserName))
                {
                    result.Message = "用户名必填";
                    return result;
                }
                if (string.IsNullOrEmpty(model.IpAddress))
                {
                    result.Message = "IP未获取到";
                    return result;
                }
                if (model.UserNO.Length > 20)
                {
                    result.Message = "用户登录号不能超过20个字符";
                    return result;
                }
                if (model.UserName.Length > 20)
                {
                    result.Message = "用户名字不能超过20个字符";
                    return result;
                }
                if (model.Password.Length < 3 || model.Password.Length > 12)
                {
                    result.Message = "用户密码请在3-12位之间";
                    return result;
                }
                if (model.IpAddress.Length > 50)
                {
                    result.Message = "用户IP不能超过50个字符";
                    return result;
                }
                // 业务验证
                // 名字不能重复，用户号不能重复，IP不能重复
                using (var masterConn = new MySqlConnection(Connection))
                {
                    var oldObj = masterConn.QueryFirstOrDefault<TM_Hcj_User_PO>("select UserNO,IpAddress,UserName from tm_hcj_user where UserNO= @UserNO or IpAddress=@IpAddress or UserName=@UserName", model, commandTimeout: 300);
                    if (oldObj != null)
                    {
                        if (oldObj.UserNO == model.UserNO)
                        {
                            result.Message = $"用户号{model.UserNO}重复";
                            return result;
                        }
                        if (oldObj.UserName == model.UserName)
                        {
                            result.Message = $"用户名{model.UserName}重复";
                            return result;
                        }
                        if (oldObj.IpAddress == model.IpAddress)
                        {
                            result.Message = "该IP地址已经注册过账号了";
                            return result;
                        }
                    }
                }
                int a = new Random().Next(1, 5);
                // 组装
                var executUserModel = new TM_Hcj_User_PO
                {
                    CreateDate = DateTime.Now,
                    UserNO = model.UserNO,
                    IpAddress = model.IpAddress,
                    IsAdmin = 0,
                    Locked = 0,
                    Password = model.Password,
                    UserName = model.UserName,
                    UserPhoto = $"{a}.jpg",
                };
                var executUserLogModel = new TL_Hcj_UserLog_PO
                {
                    ipAddress = model.IpAddress,
                    LogType = 1,
                    Password = model.Password,
                    UserName = model.UserName,
                };

                // 执行
                using (var masterConn = new MySqlConnection(Connection))
                {
                    masterConn.Open();
                    using (var tran = masterConn.BeginTransaction())
                    {
                        try
                        {
                            // 插入用户表
                            executUserLogModel.UserID = masterConn.QueryFirst<int>(@"INSERT into tm_hcj_user(UserNO,UserName,Password,Locked,IsAdmin,CreateDate,ipAddress,UserPhoto)
VALUES(@UserNO, @UserName, @Password, @Locked, @IsAdmin, @CreateDate, @ipAddress,@UserPhoto); Select @@IDENTITY;", executUserModel);
                            // 插入用户日志表
                            masterConn.Execute(@"INSERT into TL_Hcj_UserLog(UserID,LogType,UserName,Password,MobilePhone,UserPhoto,ipAddress)
VALUES(@UserID,@LogType,@UserName,@Password,@MobilePhone,@UserPhoto,@ipAddress);", executUserLogModel);
                            tran.Commit();
                        }
                        catch (Exception)
                        {
                            tran.Rollback();
                            throw;
                        }
                    }
                }
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
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseResponse UpdateUser(TM_Hcj_User_PO model)
        {
            var result = new BaseResponse();
            try
            {
                // 必填项验证
                if (string.IsNullOrEmpty(model.UserNO))
                {
                    result.Message = "用户账号必填";
                    return result;
                }
                if (string.IsNullOrEmpty(model.MobilePhone))
                {
                    result.Message = "手机号必填";
                    return result;
                }
                if (string.IsNullOrEmpty(model.UserName))
                {
                    result.Message = "用户名必填";
                    return result;
                }
                if (string.IsNullOrEmpty(model.IpAddress))
                {
                    result.Message = "IP未获取到";
                    return result;
                }
                if (model.IpAddress.Length > 50)
                {
                    result.Message = "用户IP不能超过50个字符";
                    return result;
                }
                // 业务验证
                // 名字不能重复，用户号不能重复，IP不能重复
                int oldUserID = 0;
                using (var masterConn = new MySqlConnection(Connection))
                {
                    oldUserID = masterConn.QueryFirstOrDefault<int>("select UserID from tm_hcj_user where UserNO= @UserNO AND UserName=@UserName", model, commandTimeout: 300);
                    if (oldUserID <= 0)
                    {
                        result.Message = "账号和名字没有已注册的用户";
                        return result;
                    }
                }

                // 组装
                var executUserModel = new TM_Hcj_User_PO
                {
                    UserID = oldUserID,
                    MobilePhone = model.MobilePhone,
                };
                var executUserLogModel = new TL_Hcj_UserLog_PO
                {
                    ipAddress = model.IpAddress,
                    LogType = 4,
                    MobilePhone = model.MobilePhone,
                    UserID = oldUserID,
                };

                // 执行
                using (var masterConn = new MySqlConnection(Connection))
                {
                    masterConn.Open();
                    using (var tran = masterConn.BeginTransaction())
                    {
                        try
                        {
                            // 插入用户表
                            executUserLogModel.UserID = masterConn.Execute(@"update tm_hcj_user set  MobilePhone=@MobilePhone,UpdateDate=now() where UserID=@UserID;", executUserModel);
                            // 插入用户日志表
                            masterConn.Execute(@"INSERT into TL_Hcj_UserLog(UserID,LogType,UserName,Password,MobilePhone,UserPhoto,ipAddress)
VALUES(@UserID,@LogType,@UserName,@Password,@MobilePhone,@UserPhoto,@ipAddress);", executUserLogModel);
                            tran.Commit();
                        }
                        catch (Exception)
                        {
                            tran.Rollback();
                            throw;
                        }
                    }
                }
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
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseResponse LoginUser(TM_Hcj_User_PO model)
        {
            var result = new BaseResponse();
            try
            {
                if (string.IsNullOrEmpty(model.UserNO) || string.IsNullOrEmpty(model.Password))
                {
                    result.Message = "请输入账号和密码";
                    return result;
                }
                var userObj = new TM_Hcj_User_PO();
                using (var masterConn = new MySqlConnection(Connection))
                {
                    userObj = masterConn.QueryFirstOrDefault<TM_Hcj_User_PO>("select * from tm_hcj_user where UserNo= @UserNO AND Password=@Password", model, commandTimeout: 300);
                }
                if (userObj == null)
                {
                    result.Message = "用户不存在，或密码错误";
                    return result;
                }

                var executUserLogModel = new TL_Hcj_UserLog_PO
                {
                    ipAddress = model.IpAddress,
                    LogType = 3,
                    UserID = userObj.UserID
                };

                // 执行
                using (var masterConn = new MySqlConnection(Connection))
                {
                    masterConn.Open();
                    using (var tran = masterConn.BeginTransaction())
                    {
                        try
                        {
                            // 插入用户表
                            executUserLogModel.UserID = masterConn.Execute($@"update tm_hcj_user set  LastLoginDate=now() where UserID={userObj.UserID};", null);
                            // 插入用户日志表
                            masterConn.Execute(@"INSERT into TL_Hcj_UserLog(UserID,LogType,UserName,Password,MobilePhone,UserPhoto,ipAddress)
VALUES(@UserID,@LogType,@UserName,@Password,@MobilePhone,@UserPhoto,@ipAddress);", executUserLogModel);
                            tran.Commit();
                        }
                        catch (Exception)
                        {
                            tran.Rollback();
                            throw;
                        }
                    }
                }
                result.Data = userObj;
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
