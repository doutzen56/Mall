using Mall.Common.Extend;
using Mall.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;

namespace Mall.Core.Filter
{
    /// <summary>
    /// 全局异常拦截器，用于捕获系统未处理异常
    /// </summary>
    public class ExceptionFilter: IExceptionFilter
    {
        private ILogger<ExceptionFilter> logger = null;
        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            this.logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            //如果异常未被处理
            if (!context.ExceptionHandled)
            {
                context.Result = new JsonResult(new RespResult
                {
                    Msg="操作失败！",
                    Status=500,
                    Data=context.Exception.Message
                });
                var url = context.HttpContext.Request.Path.Value;
                var actionName = context.ActionDescriptor.DisplayName;
                var log = new LogModel
                {
                    LogType = Common.Enum.LogType.Exception,
                    LogLevel = Common.Enum.LogLevel.Error,
                    CreateTime = DateTime.UtcNow,
                    Message = $"URL:{url},action:{actionName},异常信息:{context.Exception.ToJson()}"
                };
                //记录请求信息，方便排查问题
                logger.LogError(log.ToJson());
            }
            //将异常处理状态设置为已处理
            context.ExceptionHandled = true;
        }
    }
}
