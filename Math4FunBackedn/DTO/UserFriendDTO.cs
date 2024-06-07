using Math4FunBackedn.Entities;

namespace Math4FunBackedn.DTO
{
    public class UserFriendDTO
    {
        public User User { get; set; }
        public bool IsFollowing { get; set; }
        public bool IsFollower { get; set; }
    }
}
