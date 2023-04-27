using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Models
{
    /// <summary>
    /// 学生类
    /// </summary>
    public class Student
    {
        public int Id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Required]//不能为空（数据验证）
        [MaxLength(40)]//字符串最大长度
        public string Name { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string PhotoPath { get; set; }

        /// <summary>
        /// 外键（跟StudentClass关联）
        /// </summary>
        public int StudentClassId { get; set; }

        /// <summary>
        /// 班级
        /// </summary>
        [ForeignKey("StudentClassId")]
        public StudentClass StudentClass { get; set; }
    }
}
