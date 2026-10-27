using HM_Instagram.BLL.DTOs.Account;

namespace HM_Instagram.BLL.Interfaces;

public interface IAccountService
{
    Task<AccountResultDto> RegisterAsync(RegisterDto registration);
    Task<AccountResultDto> LoginAsync(LoginDto login);
    Task LogOffAsync();
}
