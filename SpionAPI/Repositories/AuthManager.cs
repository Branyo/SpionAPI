using Microsoft.AspNetCore.Identity;
using SpionAPI.Interfaces;
using SpionAPI.Models;
using SpionAPI.Models.Dto;
using SpionAPI_Model.Models;

namespace SpionAPI.Repositories
{
    public class AuthManager : IAuthManager
    {
        private readonly UserManager<SpionUser> _userManager;

        public AuthManager(UserManager<SpionUser> userManager)
        {
            this._userManager = userManager;
        }

        public async Task<IEnumerable<IdentityError>> Register(SpionUserDto spionUserDto)
        {
            var user = DtoConvertSpionUser.ToSpionUser(spionUserDto);

            var result = await _userManager.CreateAsync(user, spionUserDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }

            return result.Errors;
            
        }
    }
}
