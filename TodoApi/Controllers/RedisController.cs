using Microsoft.AspNetCore.Mvc;
using TodoApi.Models.ApiResult;
using TodoApi.Redis;

namespace TodoApi.Controllers
{
    /// <summary>
    /// Redis缓存
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RedisController : ControllerBase
    {
        private readonly IRedisHelper _redisHelper;

        public RedisController(IRedisHelper redisHelper)
        {
            _redisHelper = redisHelper;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<ApiResult> Create([FromQuery] string key, string value)
        {
            bool setValue = _redisHelper.SetValue(key, value);

            if (setValue)
            {
                return ApiResultHelper.Success(setValue);
            }
            else
            {
                return ApiResultHelper.Error("新增失败");
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ApiResult> GetAll([FromQuery] string key)
        {
            string saveValue = _redisHelper.GetValue(key);
            return ApiResultHelper.Success(saveValue);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult<ApiResult> Update([FromQuery] string key, string value)
        {
            bool newValue = _redisHelper.SetValue(key, value);
            if (newValue)
            {
                string getValue = _redisHelper.GetValue(key);
                return ApiResultHelper.Success(getValue);
            }
            else
            {
                return ApiResultHelper.Error("修改失败");
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult<ApiResult> Delete([FromQuery] string key)
        {
            bool saveValue = _redisHelper.DeleteKey(key);
            if (saveValue)
            {
                return ApiResultHelper.Success(saveValue);
            }
            else
            {
                return ApiResultHelper.Error("删除失败");
            }
        }
    }
}
