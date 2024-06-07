using Math4FunBackedn.DTO;
using Math4FunBackedn.Entities;

namespace Math4FunBackedn.Repositories.Friend
{
    public interface IFriendRepository
    {
        Task<FriendResponseDTO[]> GetAllYourFriends();
        Task<FriendResponseDTO> AddFriend(Guid friendId, Guid userId);
        Task<FriendResponseDTO> DeleteFriend(Guid friendId);
        Task<PageResult<FriendResponseDTO>> SearchFriend(string keyword, Guid UserId);
        Task<PageResult<Friends>> GetAllFriendFollowing(Guid UserId);
        Task<PageResult<Friends>> GetFollowers(Guid UserId);
        Task<UserFriendDTO> GetUserDetail(Guid friendId, Guid userId);
        Task<int> UnfollowingUser(Guid friendId, Guid userId);
        Task<int> UnfollowerUser(Guid friendId, Guid userId);
        Task<int> UnFollowFromBoth(Guid friendId, Guid userId);
    }
}
