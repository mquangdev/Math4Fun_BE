using System.ComponentModel.DataAnnotations;

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
    }
}
