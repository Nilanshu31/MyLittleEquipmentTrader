using Microsoft.AspNetCore.Mvc;
//using MyLittleEquipmentTrader.Application.Interfaces;
using MyLittleEquipmentTrader.Application.Models;
using MyLittleEquipmentTrader.Application.Services;
using System.Threading.Tasks;

namespace MyLittleEquipmentTrader.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private readonly IUserInfoService _UserInfoService;

        public UserInfoController(IUserInfoService UserInfoService)
        {
            _UserInfoService = UserInfoService;
        }

        // GET: /api/UserInfo
        [HttpGet]
        public async Task<IActionResult> GetUserInfos()
        {
            var UserInfos = await _UserInfoService.GetAllUserInfosAsync();
            return Ok(UserInfos);
        }

        // POST: /api/UserInfo/filter
        [HttpPost("filter")]
        public async Task<IActionResult> FilterUserInfos([FromBody] ProductFilterRequest request)
        {
            if (request == null)
            {
                return BadRequest("Filter request cannot be null.");
            }

            var pagedUserInfos = await _UserInfoService.GetFilteredUserInfosAsync(request);
            return Ok(pagedUserInfos);
        }
    }
}
