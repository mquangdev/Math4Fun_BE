using Math4FunBackedn.Repositories.Friend;
using Math4FunBackedn.Repositories.TokenRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Math4FunBackedn.Controllers
{
    [Authorize]
    [Route("Friend")]
    public class FriendController : ControllerBase
    {
        private readonly IFriendRepository _friendRepo;
        private readonly ITokenRepository _tokenRepo;
        public FriendController(IFriendRepository friendRepo, ITokenRepository tokenRepo)
        {
            _friendRepo = friendRepo;
            _tokenRepo = tokenRepo;
        }
        [HttpGet("FindFriend")]
        public async Task<IActionResult> FindFriends(string keyword)
        {
            try
            {
                string authorizationHeader = Request.Headers["Authorization"];
                Guid userId = await _tokenRepo.DecodeToken(authorizationHeader);
                return Ok(await _friendRepo.SearchFriend(keyword, userId)); 
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost("AddFriend")]
        public async Task<IActionResult> AddFriend(Guid friendId)
        {
            try {
                string authorizationHeader = Request.Headers["Authorization"];
                Guid userId = await _tokenRepo.DecodeToken(authorizationHeader);
                return Ok(await _friendRepo.AddFriend(friendId, userId));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("GetFriendsFollowing")]
        public async Task<IActionResult> GetAllFriendsFollowing(Guid? anotherUserId)
        {
           try
            {
                string authorizationHeader = Request.Headers["Authorization"];
                Guid userId = await _tokenRepo.DecodeToken(authorizationHeader);
                if(anotherUserId != null)
                {
                    return Ok(await _friendRepo.GetAllFriendFollowing((Guid) anotherUserId));
                }
                else
                {
                    return Ok(await _friendRepo.GetAllFriendFollowing(userId));
                }
                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("GetFollowers")]
        public async Task<IActionResult> GetAllFollowers(Guid? anotherUserId)
        {
            try
            {
                string authorizationHeader = Request.Headers["Authorization"];
                Guid userId = await _tokenRepo.DecodeToken(authorizationHeader);
                if (anotherUserId != null)
                {
                    return Ok(await _friendRepo.GetFollowers((Guid) anotherUserId));
                }
                else
                {
                    return Ok(await _friendRepo.GetFollowers(userId));
                }
               
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("GetUserDetail")]
        public async Task<IActionResult> GetUserDetail(Guid friendId)
        {
            try
            {
                string authorizationHeader = Request.Headers["Authorization"];
                Guid userId = await _tokenRepo.DecodeToken(authorizationHeader);
                return Ok(await _friendRepo.GetUserDetail(friendId, userId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("UnFollowingUser")]
        public async Task<IActionResult> UnfollowingUser(Guid friendId)
        {
            try
            {
                string authorizationHeader = Request.Headers["Authorization"];
                Guid userId = await _tokenRepo.DecodeToken(authorizationHeader);
                return Ok(await _friendRepo.UnfollowingUser(friendId, userId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("RemoveFollower")]
        public async Task<IActionResult> RemoveFollower(Guid friendId)
        {
            try
            {
                string authorizationHeader = Request.Headers["Authorization"];
                Guid userId = await _tokenRepo.DecodeToken(authorizationHeader);
                return Ok(await _friendRepo.UnfollowerUser(friendId, userId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("UnfollowFromBoth")]
        public async Task<IActionResult> UnfollowFromBoth(Guid friendId)
        {
            try
            {
                string authorizationHeader = Request.Headers["Authorization"];
                Guid userId = await _tokenRepo.DecodeToken(authorizationHeader);
                return Ok(await _friendRepo.UnFollowFromBoth    (friendId, userId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
