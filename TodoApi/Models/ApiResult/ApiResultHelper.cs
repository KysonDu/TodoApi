namespace TodoApi.Models.ApiResult
{
    public static class ApiResultHelper
    {
        /// <summary>
        /// 成功后返回的数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static ApiResult Success(dynamic data)
        {
            return new ApiResult
            {
                Code = 200,
                Data = data,
                Msg = "操作成功",
                Total = 0
            };
        }

        /// <summary>
        /// 成功后返回的数据（分页）
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static ApiResult Success(dynamic data, int total)
        {
            return new ApiResult
            {
                Code = 200,
                Data = data,
                Msg = "操作成功",
                Total = total
            };
        }

        /// <summary>
        /// 失败后返回的数据
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static ApiResult Error(string msg)
        {
            return new ApiResult
            {
                Code = 500,
                Data = null,
                Msg = msg,
                Total = 0
            };
        }
    }
}
