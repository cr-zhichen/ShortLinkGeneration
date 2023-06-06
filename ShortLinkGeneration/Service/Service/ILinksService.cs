using ShortLinkGeneration.Entity;
using ShortLinkGeneration.Entity.Request;
using ShortLinkGeneration.Entity.Response;

namespace ShortLinkGeneration.Service.Service;

public interface ILinksService
{
    public IRe<LinksResponse.CreateResponse> Create(LinksRequest.CreateRequest data);
}