using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Math4FunBackedn.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string? Fullname { get; set; }
        public string? Gender { get; set; }
        public DateTime? Dob { get; set; }
        public string? Avatar { get; set; }
        public string? Email { get; set; }
        public double? TotalExp { get; set; }
        public double? TotalGem { get; set; }
        public int? Role { get; set; }
        public ICollection<Users_Courses>? Users_Courses { get; set; }
    }
}
