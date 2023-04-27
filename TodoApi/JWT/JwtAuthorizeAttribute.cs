using Microsoft.AspNetCore.Authorization;

namespace TodoApi.JWT
{
    /// <summary>
    /// 后端权限认证
    /// </summary>
    public class BackendJwtAuthorizeAttribute : AuthorizeAttribute
    {
        public const string AuthenticationScheme = "BearerBackend";

        public BackendJwtAuthorizeAttribute()
        {
            AuthenticationSchemes = AuthenticationScheme;
        }
    }

    /// <summary>
    /// 移动端权限认证
    /// </summary>
    public class APPJwtAuthorizeAttribute : AuthorizeAttribute
    {
        public const string AuthenticationScheme = "BearerApiAttribute";

        public APPJwtAuthorizeAttribute()
        {
            AuthenticationSchemes = AuthenticationScheme;
        }
    }
}
