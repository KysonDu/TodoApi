namespace TodoApi.Models.DTO
{
    public class TodoItemDto
    {
        public long Id { get; set; }//Id 属性用作关系数据库中的唯一键。
        public string Name { get; set; }
    }
}
