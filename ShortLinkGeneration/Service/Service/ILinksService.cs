using ShortLinkGeneration.Entity;
using ShortLinkGeneration.Entity.Request;
using ShortLinkGeneration.Entity.Response;

namespace ShortLinkGeneration.Service.Service;

public interface ILinksService
{
    public Task<IRe<LinksResponse.CreateResponse>> Create(LinksRequest.CreateRequest data);
    public Task<IRe<LinksResponse.DetectionResponse>> Detection(LinksRequest.DetectionRequest data);
}