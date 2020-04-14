using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using IdentityServer.Roles.Dto;
using IdentityServer.Users.Dto;

namespace IdentityServer.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);
    }
}
