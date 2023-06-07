using ShortLinkGeneration.Entity;
using ShortLinkGeneration.Entity.Request;
using ShortLinkGeneration.Entity.Response;

namespace ShortLinkGeneration.Service.Service;

public interface ILinksService
{
    public Task<IRe<LinksResponse.CreateResponse>> Create(LinksRequest.CreateRequest data);
    public Task<IRe<LinksResponse.DetectionResponse>> Detection(LinksRequest.DetectionRequest data);
    public Task<IRe<LinksResponse.GetAllResponse>> GetAll(LinksRequest.GetAllRequest data);
    public Task<IRe<LinksResponse.GetResponse>> Get(LinksRequest.GetRequest data);
    public Task<IRe<LinksResponse.SearchResponse>> Search(LinksRequest.SearchRequest data);
    public Task<IRe<LinksResponse.UpdateResponse>> Update(LinksRequest.UpdateRequest data);
    public Task<IRe<LinksResponse.DeleteResponse>> Delete(LinksRequest.DeleteRequest data);
}