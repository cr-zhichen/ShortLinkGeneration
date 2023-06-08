using ShortLinkGeneration.Entity;
using ShortLinkGeneration.Entity.Request;
using ShortLinkGeneration.Entity.Response;

namespace ShortLinkGeneration.Service.Service;

public interface IManagerLinksServer
{
    public Task<IRe<ManagerLinksResponse.GetAllLinkResponse>> GetAll(ManagerLinksRequest.GetAllLinkRequest data);
    public Task<IRe<ManagerLinksResponse.GetLinkResponse>> Get(ManagerLinksRequest.GetLinkRequest data);
    public Task<IRe<ManagerLinksResponse.UpdateLinkResponse>> Update(ManagerLinksRequest.UpdateLinkRequest data);
    public Task<IRe<ManagerLinksResponse.DeleteLinkResponse>> Delete(ManagerLinksRequest.DeleteLinkRequest data);
    public Task<IRe<ManagerLinksResponse.DisabledLinkResponse>> Disabled(ManagerLinksRequest.DisabledLinkRequest data);
    public Task<IRe<ManagerLinksResponse.SearchLinkResponse>> Search(ManagerLinksRequest.SearchLinkRequest data);
    public Task<IRe<ManagerLinksResponse.GetLinkByUserResponse>> GetByUser(ManagerLinksRequest.GetLinkByUserRequest data);
    public Task<IRe<ManagerLinksResponse.GetClicksResponse>> GetClicks(ManagerLinksRequest.GetClicksRequest data);
}