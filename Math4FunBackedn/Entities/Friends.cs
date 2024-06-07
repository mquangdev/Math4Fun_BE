using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Math4FunBackedn.Entities
{
    public class Friends
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        [ForeignKey("FriendId")]
        public Guid FriendId { get; set; }
        public User Friend { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
