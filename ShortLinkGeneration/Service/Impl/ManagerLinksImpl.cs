using ShortLinkGeneration.DB;
using ShortLinkGeneration.Entity;
using ShortLinkGeneration.Entity.Request;
using ShortLinkGeneration.Entity.Response;
using ShortLinkGeneration.Service.Service;

namespace ShortLinkGeneration.Service.Impl;

public class ManagerLinksImpl : IManagerLinksServer
{
    private readonly ILogger<ManagerLinksImpl> _logger;
    private readonly ShortLinkContext _db;
    private readonly IJwtService _jwtService;

    public ManagerLinksImpl(ILogger<ManagerLinksImpl> logger, ShortLinkContext db, IJwtService jwtService)
    {
        _logger = logger;
        _db = db;
        _jwtService = jwtService;
    }

    public Task<IRe<ManagerLinksResponse.GetAllLinkResponse>> GetAll(ManagerLinksRequest.GetAllLinkRequest data)
    {
        throw new NotImplementedException();
    }

    public Task<IRe<ManagerLinksResponse.GetLinkResponse>> Get(ManagerLinksRequest.GetLinkRequest data)
    {
        throw new NotImplementedException();
    }

    public Task<IRe<ManagerLinksResponse.UpdateLinkResponse>> Update(ManagerLinksRequest.UpdateLinkRequest data)
    {
        throw new NotImplementedException();
    }

    public Task<IRe<ManagerLinksResponse.DeleteLinkResponse>> Delete(ManagerLinksRequest.DeleteLinkRequest data)
    {
        throw new NotImplementedException();
    }

    public Task<IRe<ManagerLinksResponse.DisabledLinkResponse>> Disabled(ManagerLinksRequest.DisabledLinkRequest data)
    {
        throw new NotImplementedException();
    }

    public Task<IRe<ManagerLinksResponse.SearchLinkResponse>> Search(ManagerLinksRequest.SearchLinkRequest data)
    {
        throw new NotImplementedException();
    }

    public Task<IRe<ManagerLinksResponse.GetLinkByUserResponse>> GetByUser(ManagerLinksRequest.GetLinkByUserRequest data)
    {
        throw new NotImplementedException();
    }

    public Task<IRe<ManagerLinksResponse.GetClicksResponse>> GetClicks(ManagerLinksRequest.GetClicksRequest data)
    {
        throw new NotImplementedException();
    }
}