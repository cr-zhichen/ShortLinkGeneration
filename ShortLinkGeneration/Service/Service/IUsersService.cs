using ShortLinkGeneration.Entity;
using ShortLinkGeneration.Entity.Request;
using ShortLinkGeneration.Entity.Response;

namespace ShortLinkGeneration.Service.Service;

public interface IUsersService
{
    public IRe<UsersResponse.RegisterResponse> Register(UsersRequest.RegisterRequest data);

    public IRe<UsersResponse.LoginResponse> Login(UsersRequest.LoginRequest data);
    public IRe<UsersResponse.SendCodeResponse> SendCode(UsersRequest.SendCodeRequest data);
}