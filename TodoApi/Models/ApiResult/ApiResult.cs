namespace TodoApi.Models.ApiResult
{
    public class ApiResult
    {
        //状态字码
        public int Code { get; set; }

        //数据
        public dynamic Data { get; set; }

        //返回成功/错误信息
        public string Msg { get; set; }

        //数据总条数
        public int Total { get; set; }
    }
}
