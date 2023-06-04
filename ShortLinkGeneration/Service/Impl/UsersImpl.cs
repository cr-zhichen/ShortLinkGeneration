using ShortLinkGeneration.DB;
using ShortLinkGeneration.Entity;
using ShortLinkGeneration.Entity.Request;
using ShortLinkGeneration.Entity.Response;
using ShortLinkGeneration.Service.Service;

namespace ShortLinkGeneration.Service.Impl;

public class UsersImpl : IUsersService
{
    private readonly ILogger<UsersImpl> _logger;
    private readonly ShortLinkContext _db;

    public UsersImpl(ILogger<UsersImpl> logger, ShortLinkContext db)
    {
        _logger = logger;
        _db = db;
    }

    public IRe<UsersResponse.RegisterResponse> Register(UsersRequest.RegisterRequest data)
    {
        throw new NotImplementedException();
    }
}