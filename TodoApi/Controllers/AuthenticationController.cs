using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApi.JWT;
using TodoApi.Models.ApiResult;

namespace TodoApi.Controllers
{
    /// <summary>
    /// 登录获取Jwt
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticateService _authService;
        public AuthenticationController(IAuthenticateService authService)
        {
            this._authService = authService;
        }

        [HttpPost, Route("BackendRequestToken")]
        public ActionResult<ApiResult> BackendRequestToken([FromBody] LoginRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Request");
            }

            string token;
            if (_authService.BackendIsAuthenticated(request, out token))
            {
                return ApiResultHelper.Success("Bearer" + token);
            }

            return BadRequest("Invalid Request");
        }

        [HttpPost, Route("APPRequestToken")]
        public ActionResult<ApiResult> APPRequestToken([FromBody] LoginRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Request");
            }

            string token;
            if (_authService.APPIsAuthenticated(request, out token))
            {
                return ApiResultHelper.Success("Bearer" + token);
            }

            return BadRequest("Invalid Request");
        }
    }
}
