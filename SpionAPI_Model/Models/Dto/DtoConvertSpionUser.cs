using SpionAPI_Model.Models;

namespace SpionAPI.Models.Dto
{
    public static class DtoConvertSpionUser
    {
        public static SpionUserDto? ToSpionUserDto(SpionUser? spionUser)
        {
            if (spionUser == null) return null;

            return new SpionUserDto()
            {
                UserName = spionUser.UserName,
                GamesPlayed = spionUser.GamesPlayed
            };

        }
        
        public static SpionUser? ToSpionUser(SpionUserDto? spionUserDto)
        {
            if (spionUserDto == null) return null;

            return new SpionUser()
            {
                UserName = spionUserDto.UserName,
                GamesPlayed = spionUserDto.GamesPlayed                
            };

        }



    }
}