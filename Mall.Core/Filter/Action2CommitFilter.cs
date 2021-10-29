using Mall.Common.Extend;
using Mall.Common.Models;
using Mall.Common.Utils;
using Mall.Core.Redis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;

namespace Mall.Core.Filter
{
    /// <summary>
    /// 防止重复提交过滤
    /// </summary>
    public class Action2CommitFilter: ActionFilterAttribute
    {
        private readonly ILogger<Action2CommitFilter> logger;
        private readonly RedisProvider redis;
        private static readonly string KEY_PREFIX = "mall.2CommitFilter";
        /// <summary>
        /// 防重复提交周期  单位秒
        /// </summary>
        private int TimeOut = 10;
        public Action2CommitFilter(ILogger<Action2CommitFilter> logger, RedisProvider redis)
        {
            this.logger = logger;
            this.redis = redis;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string url = context.HttpContext.Request.Path.Value;
            string argument = context.ActionArguments.ToJson();
            string ip = context.HttpContext.Connection.RemoteIpAddress.ToString();
            string agent = context.HttpContext.Request.Headers["User-Agent"];
            string sInfo = $"{url}-{argument}-{ip}-{agent}";
            string summary = MD5Helper.MD5EncodingOnly(sInfo);

            string totalKey = $"{KEY_PREFIX}-{summary}";

            string result = this.redis.Get<string>(totalKey);
            if (string.IsNullOrEmpty(result))
            {
                this.redis.Add(totalKey, "1", TimeSpan.FromSeconds(TimeOut));
                this.logger.LogInformation($"CustomAction2CommitFilterAttribute:{sInfo}");
            }
            else
            {
                //已存在
                this.logger.LogWarning($"Action2CommitFilter重复请求:{sInfo}");
                context.Result = new JsonResult(RespResult.Fail($"请勿重复提交，{this.TimeOut}s之后重试"));
            }
        }
    }
}
