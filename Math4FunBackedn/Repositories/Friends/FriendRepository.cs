using Math4FunBackedn.DBContext;
using Math4FunBackedn.DTO;
using Math4FunBackedn.Entities;
using Microsoft.EntityFrameworkCore;

namespace Math4FunBackedn.Repositories.Friend
{
    public class FriendRepository : IFriendRepository
    {  
        private readonly MyDbContext _context;
        public FriendRepository(MyDbContext context) {
            _context = context;
        }
        public async Task<FriendResponseDTO> AddFriend(Guid friendId, Guid userId)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == userId);
            var friendUser = await _context.User.FirstOrDefaultAsync(u => u.Id == friendId);
            if (user == null)
            {
                throw new Exception("Không tìm thấy người dùng này");
            }
            if(friendUser == null)
            {
                throw new Exception("Không tìm thấy người dùng này để theo dõi");
            }
            var friend = await _context.Friend.FirstOrDefaultAsync(f => f.FriendId == friendId && f.UserId == userId);
            if(friend != null)
            {
                throw new Exception("Bạn đã kết bạn với người dùng này rồi");
            }
            else
            {
                var newFriend = new Friends()
                {
                    CreateAt = DateTime.UtcNow,
                    Friend = friendUser,
                    User = user,
                    Id = Guid.NewGuid(),
                    FriendId = friendId,
                    UserId = userId,
                };
                await _context.Friend.AddAsync(newFriend);
                await _context.SaveChangesAsync();
                return new FriendResponseDTO()
                {
                    IsYourFriend = true,
                    User = friendUser
                };
            }
            
        }

        public Task<FriendResponseDTO> DeleteFriend(Guid friendId)
        {
            throw new NotImplementedException();
        }

        public async Task<PageResult<Friends>> GetAllFriendFollowing(Guid UserId)
        {
            var listFriends = await _context.Friend.Where(f => f.UserId == UserId).Include(f => f.Friend).ToListAsync();
            return new PageResult<Friends>()
            {
                Data = listFriends,
                Total = listFriends.Count,
            };
        }

        public async Task<FriendResponseDTO[]> GetAllYourFriends()
        {
            throw new NotImplementedException();
        }

        public async  Task<PageResult<Friends>> GetFollowers(Guid UserId)
        {
            var listFollowers = await _context.Friend.Where(f => f.FriendId == UserId).Include(f => f.User).ToListAsync();
            return new PageResult<Friends>()
            {
                Data = listFollowers,
                Total = listFollowers.Count,
            };
        }

        public async Task<UserFriendDTO> GetUserDetail(Guid friendId, Guid userId)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == friendId);
            if(user == null)
            {
                throw new Exception("Không tìm thấy người dùng này");
            }
            var following = await _context.Friend.FirstOrDefaultAsync(f => f.UserId == userId && f.FriendId == friendId);
            var follower = await _context.Friend.FirstOrDefaultAsync(f => f.UserId == friendId && f.FriendId == userId);
            return new UserFriendDTO()
            {
                User = user,
                IsFollower = follower != null,
                IsFollowing = following != null
            };
        }

        public async Task<PageResult<FriendResponseDTO>> SearchFriend(string keyword, Guid UserId)
        {
            if(string.IsNullOrEmpty(keyword))
            {
                return new PageResult<FriendResponseDTO>
                {
                    Data = new List<FriendResponseDTO>(),
                    Total = 0
                };
            }
            keyword = keyword.ToLower();
            var listUsers = await _context.User.Where(u => u.Username.ToLower().Contains(keyword) || u.Email.ToLower().Contains(keyword) || u.Fullname.ToLower().Contains(keyword)).ToListAsync();
            var listFriend = await _context.Friend.Where(f => f.UserId == UserId).Include(u => u.Friend).Where(f => f.Friend.Username.ToLower().Contains(keyword) || f.Friend.Email.ToLower().Contains(keyword) || f.Friend.Fullname.ToLower().Contains(keyword)).ToListAsync();
            var listResult = new List<FriendResponseDTO>();
            listUsers.ForEach(user =>
            {
                var isFriend = false;
                listFriend.ForEach(friend =>
                {
                    if (friend.FriendId == user.Id)
                    {
                        isFriend = true;
                    }
                });
                if(user.Id != UserId)
                {
                    listResult.Add(new FriendResponseDTO
                    {
                        User = user,
                        IsYourFriend = isFriend
                    });
                }
               
            });
            return new PageResult<FriendResponseDTO>
            {
                Total = listResult.Count,
                Data = listResult
            };
        }

        public async  Task<int> UnfollowerUser(Guid friendId, Guid userId)
        {
            var friend = await _context.User.FirstOrDefaultAsync(u => u.Id == friendId);
            if (friend == null)
            {
                throw new Exception("Không tìm thấy người dùng này");
            }
            var follower = await _context.Friend.FirstOrDefaultAsync(f => f.UserId == friendId && f.FriendId == userId);
            if (follower == null)
            {
                throw new Exception("Người dùng này chưa theo dõi bạn");
            }
            _context.Friend.Remove(follower);
            await _context.SaveChangesAsync();
            return 1;
        }

        public async Task<int> UnFollowFromBoth(Guid friendId, Guid userId)
        {
            var friend = await _context.User.FirstOrDefaultAsync(u => u.Id == friendId);
            if (friend == null)
            {
                throw new Exception("Không tìm thấy người dùng này");
            }
            var following = await _context.Friend.FirstOrDefaultAsync(f => f.UserId == userId && f.FriendId == friendId);
            var follower = await _context.Friend.FirstOrDefaultAsync(f => f.UserId == friendId && f.FriendId == userId);
            if (following == null)
            {
                throw new Exception("Bạn đang chưa theo dõi người dùng này");
            }
            if (follower == null)
            {
                throw new Exception("Người dùng này chưa theo dõi bạn");
            }
            _context.Friend.Remove(following);
            _context.Friend.Remove(follower);
            await _context.SaveChangesAsync();
            return 1;
        }

        public async Task<int> UnfollowingUser(Guid friendId, Guid userId)
        {
            var friend = await _context.User.FirstOrDefaultAsync(u => u.Id == friendId);
            if(friend == null)
            {
                throw new Exception("Không tìm thấy người dùng này");
            }
            var following = await _context.Friend.FirstOrDefaultAsync(f => f.UserId == userId && f.FriendId == friendId);
            if(following == null)
            {
                throw new Exception("Bạn đang chưa theo dõi người dùng này");
            }
            _context.Friend.Remove(following);
            await _context.SaveChangesAsync();
            return 1;
        } 

    }
}
