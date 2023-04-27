using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace TodoApi.Redis
{
    public class RedisHelper : IRedisHelper
    {
        private readonly RedisServer _tokenManagement;

        private ConnectionMultiplexer Redis { get; set; }

        private IDatabase DB { get; set; }

        public RedisHelper(IOptions<RedisServer> tokenManagement)
        {
            _tokenManagement = tokenManagement.Value;
            Redis = ConnectionMultiplexer.Connect(_tokenManagement.Server);
            DB = Redis.GetDatabase();
        }

        /// <summary>
        /// 增加/修改
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetValue(string key, string value)
        {
            return DB.StringSet(key, value);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetValue(string key)
        {
            return DB.StringGet(key);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool DeleteKey(string key)
        {
            return DB.KeyDelete(key);
        }
    }
}
