using ShortLinkGeneration.Entity;
using ShortLinkGeneration.Entity.Request;
using ShortLinkGeneration.Entity.Response;

namespace ShortLinkGeneration.Service.Service;

public interface IUsersService
{
    public IRe<UsersResponse.RegisterResponse> Register(UsersRequest.RegisterRequest data);
    public IRe<UsersResponse.LoginResponse> Login(UsersRequest.LoginRequest data);
    public IRe<UsersResponse.SendCodeResponse> SendCode(UsersRequest.SendCodeRequest data);
    public IRe<UsersResponse.InfoResponse> Info(UsersRequest.InfoRequest data);
    public IRe<UsersResponse.UpdatePasswordResponse> UpdatePassword(UsersRequest.UpdatePasswordRequest data);
    public IRe<UsersResponse.ResetPasswordResponse> ResetPassword(UsersRequest.ResetPasswordRequest data);
}