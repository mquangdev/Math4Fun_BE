using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Math4FunBackedn.Entities
{
    public class Streak
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public int CurrentStreakCount { get; set; }
        public DateTime LastLessonDate { get; set; }
        public DateTime StartLessonDate { get; set; }
        public User User { get; set; }
    }
}
