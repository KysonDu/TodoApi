using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApi.JWT;
using TodoApi.Models.ApiResult;

namespace TodoApi.Controllers
{
    /// <summary>
    /// Log4日志
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class Log4Controller : ControllerBase
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Log4Controller));

        [BackendJwtAuthorize]
        /// <summary>
        /// 新增日志
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("BackendCreate")]
        public ActionResult<ApiResult> BackendCreate()
        {
            log.Debug("Debug message");
            log.Info("Informational message");
            log.Warn("Warning message");
            log.Error("Error message");
            log.Fatal("Fatal error message");

            return ApiResultHelper.Success("Ok");
        }

        [APPJwtAuthorize]
        /// <summary>
        /// 新增日志
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("APPCreate")]
        public ActionResult<ApiResult> APPCreate()
        {
            log.Debug("Debug message");
            log.Info("Informational message");
            log.Warn("Warning message");
            log.Error("Error message");
            log.Fatal("Fatal error message");

            return ApiResultHelper.Success("Ok");
        }
    }
}
