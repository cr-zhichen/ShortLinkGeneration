using ShortLinkGeneration.Entity;
using ShortLinkGeneration.Entity.Request;
using ShortLinkGeneration.Entity.Response;

namespace ShortLinkGeneration.Service.Service;

public interface IUsersService
{
    public Task<IRe<UsersResponse.RegisterResponse>> Register(UsersRequest.RegisterRequest data);
    public Task<IRe<UsersResponse.LoginResponse>> Login(UsersRequest.LoginRequest data);
    public Task<IRe<UsersResponse.SendCodeResponse>> SendCode(UsersRequest.SendCodeRequest data);
    public Task<IRe<UsersResponse.InfoResponse>> Info(UsersRequest.InfoRequest data);
    public Task<IRe<UsersResponse.UpdatePasswordResponse>> UpdatePassword(UsersRequest.UpdatePasswordRequest data);
    public Task<IRe<UsersResponse.ResetPasswordResponse>> ResetPassword(UsersRequest.ResetPasswordRequest data);
}