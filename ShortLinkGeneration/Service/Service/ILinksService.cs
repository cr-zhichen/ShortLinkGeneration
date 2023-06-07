using ShortLinkGeneration.Entity;
using ShortLinkGeneration.Entity.Request;
using ShortLinkGeneration.Entity.Response;

namespace ShortLinkGeneration.Service.Service;

public interface ILinksService
{
    public Task<IRe<LinksResponse.CreateLinkResponse>> Create(LinksRequest.CreateLinkRequest data);
    public Task<IRe<LinksResponse.DetectionResponse>> Detection(LinksRequest.DetectionRequest data);
    public Task<IRe<LinksResponse.GetAllLinkResponse>> GetAll(LinksRequest.GetAllLinkRequest data);
    public Task<IRe<LinksResponse.GetLinkResponse>> Get(LinksRequest.GetLinkRequest data);
    public Task<IRe<LinksResponse.SearchLinkResponse>> Search(LinksRequest.SearchLinkRequest data);
    public Task<IRe<LinksResponse.UpdateLinkResponse>> Update(LinksRequest.UpdateLinkRequest data);
    public Task<IRe<LinksResponse.DeleteLinkResponse>> Delete(LinksRequest.DeleteLinkRequest data);
}