using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    /// <summary>
    /// 添加模型类
    /// </summary>
    public class TodoItem
    {
        public long Id { get; set; }//Id 属性用作关系数据库中的唯一键。
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
