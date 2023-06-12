using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Math4FunBackedn.Entities
{
    public class Course
    {
        [Key]
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public double? TotalMember { get; set; }
        public ICollection<Chapter>? ChapterList { get; set; }
        public ICollection<Users_Courses>? Users_Courses { get; set; }
    }
}
