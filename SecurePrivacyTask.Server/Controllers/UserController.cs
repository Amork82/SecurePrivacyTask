using Microsoft.AspNetCore.Mvc;
using SecurePrivacyTask.Server.Dto.Input;
using SecurePrivacyTask.Server.Dto.Output;
using SecurePrivacyTask.Server.Services;

namespace SecurePrivacyTask.Server.Controllers
{
    public class UserController : BaseController
    {
        private readonly UserService _userService;

        public UserController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _userService = serviceProvider.GetRequiredService<UserService>();   
        }

        [HttpPost]
        public async Task<ActionResult<List<UserDtoOutput>>> GetUserList(UserFilterDto filter)
        {
            return Ok(await _userService.GetUserList(filter));
        }


        [HttpGet]
        public async Task<ActionResult<UserDtoOutput>> GetUserById(string id)
        {
            return Ok(await _userService.GetUserById(id));
        }

        [HttpPost]
        public async Task<ActionResult<UserDtoOutput>> CreateUser(UserDto newUser)
        {
            return Ok(await _userService.CreateUser(newUser));
        }

        [HttpPut]
        public async Task<ActionResult<UserDtoOutput>> UpdateUser(UserDto updateUser, string Id)
        {
            return Ok(await _userService.UpdateUser(updateUser, Id));
        }


        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteUser(string Id)
        {
            return Ok(await _userService.DeleteUser(Id));
        }
    }
}
