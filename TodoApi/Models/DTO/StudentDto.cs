namespace TodoApi.Models.DTO
{
    public class StudentDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string Address { get; set; }

        public Gender Gender { get; set; }

        public string PhotoPath { get; set; }

        public int StudentClassId { get; set; }

        public string StudentClassName { get; set; }
    }
}
