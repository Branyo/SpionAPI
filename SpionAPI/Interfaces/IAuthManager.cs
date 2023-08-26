using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SpionAPI.Models;

namespace SpionAPI.Interfaces
{
    public interface IAuthManager
    {
        public Task<IEnumerable<IdentityError>> Register(SpionUserDto spionUserDto);


    }
}
