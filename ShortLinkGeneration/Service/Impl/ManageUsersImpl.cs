using ShortLinkGeneration.DB;
using ShortLinkGeneration.Entity;
using ShortLinkGeneration.Entity.Request;
using ShortLinkGeneration.Entity.Response;
using ShortLinkGeneration.Service.Service;

namespace ShortLinkGeneration.Service.Impl;

public class ManageUsersImpl : IManageUsersService
{
    private readonly ILogger<ManageUsersImpl> _logger;
    private readonly ShortLinkContext _db;
    private readonly IJwtService _jwtService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ManageUsersImpl(ILogger<ManageUsersImpl> logger, ShortLinkContext db, IJwtService jwtService,
        IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _db = db;
        _jwtService = jwtService;
        _httpContextAccessor = httpContextAccessor;
    }


    public Task<IRe<ManageUsersResponse.GetAllUserResponse>> GetAll(ManageUsersRequest.GetAllUserRequest data)
    {
        throw new NotImplementedException();
    }

    public Task<IRe<ManageUsersResponse.CreateUserResponse>> Create(ManageUsersRequest.CreateUserRequest data)
    {
        throw new NotImplementedException();
    }

    public Task<IRe<ManageUsersResponse.GetUserResponse>> Get(ManageUsersRequest.GetUserRequest data)
    {
        throw new NotImplementedException();
    }

    public Task<IRe<ManageUsersResponse.DeleteUserResponse>> Delete(ManageUsersRequest.DeleteUserRequest data)
    {
        throw new NotImplementedException();
    }

    public Task<IRe<ManageUsersResponse.RestUserPasswordResponse>> RestPassword(
        ManageUsersRequest.RestUserPasswordRequest data)
    {
        throw new NotImplementedException();
    }

    public Task<IRe<ManageUsersResponse.SearchUserResponse>> Search(ManageUsersRequest.SearchUserRequest data)
    {
        throw new NotImplementedException();
    }
}