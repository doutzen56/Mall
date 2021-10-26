using Mall.Common.Extend;
using Mall.Common.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;

namespace Mall.Core.Filter
{
    /// <summary>
    /// 系统级别日志拦截器，用于记录Request请求相关
    /// </summary>
    public class RequestLogFilter : ActionFilterAttribute
    {
        private ILogger<RequestLogFilter> logger = null;
        public RequestLogFilter(ILogger<RequestLogFilter> logger)
        {
            this.logger = logger;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var url = context.HttpContext.Request.Path.Value;
            var argument = context.ActionArguments.ToJson();

            var controllerName = context.Controller.GetType().FullName;
            var actionName = context.ActionDescriptor.DisplayName;
            var log = new LogModel
            {
                LogType = Common.Enum.LogType.System,
                LogLevel = Common.Enum.LogLevel.Trace,
                CreateTime = DateTime.UtcNow,
                Message = $"URL:{url},参数:{argument},控制器:{controllerName},action:{actionName}"
            };
            //记录请求信息，方便排查问题
            logger.LogTrace(log.ToJson());
        }
    }
}
