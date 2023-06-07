using ShortLinkGeneration.Entity;
using ShortLinkGeneration.Entity.Request;
using ShortLinkGeneration.Entity.Response;

namespace ShortLinkGeneration.Service.Service;

public interface IManageUsersService
{
    public Task<IRe<ManageUsersResponse.GetAllUserResponse>> GetAll(ManageUsersRequest.GetAllUserRequest data);
    public Task<IRe<ManageUsersResponse.CreateUserResponse>> Create(ManageUsersRequest.CreateUserRequest data);
    public Task<IRe<ManageUsersResponse.GetUserResponse>> Get(ManageUsersRequest.GetUserRequest data);
    public Task<IRe<ManageUsersResponse.DeleteUserResponse>> Delete(ManageUsersRequest.DeleteUserRequest data);
    public Task<IRe<ManageUsersResponse.RestUserPasswordResponse>> RestPassword(ManageUsersRequest.RestUserPasswordRequest data);
    public Task<IRe<ManageUsersResponse.SearchUserResponse>> Search(ManageUsersRequest.SearchUserRequest data);
}