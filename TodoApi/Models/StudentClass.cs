using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    /// <summary>
    /// 班级类
    /// </summary>
    public class StudentClass
    {
        public int Id { get; set; }

        [Required]//不能为空（数据验证）
        [MaxLength(40)]//字符串最大长度
        public string Name { get; set; }
    }
}
