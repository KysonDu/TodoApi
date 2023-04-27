using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using IDatabase = StackExchange.Redis.IDatabase;

namespace TodoApi.Redis
{
    public interface IRedisHelper
    {
        /// <summary>
        /// 增加/修改
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool SetValue(string key, string value);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetValue(string key);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool DeleteKey(string key);
    }
}
