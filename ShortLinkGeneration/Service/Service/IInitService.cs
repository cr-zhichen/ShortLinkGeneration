using ShortLinkGeneration.Entity;
using ShortLinkGeneration.Entity.Request;
using ShortLinkGeneration.Entity.Response;

namespace ShortLinkGeneration.Service.Service;

public interface IInitService
{
    public IRe<InitResponse.InitDbResponse> InitDb();
    public IRe<InitResponse.InitAdminResponse> InitAdmin(InitRequest.InitAdminRequest data);
}