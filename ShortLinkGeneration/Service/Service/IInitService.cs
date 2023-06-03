using ShortLinkGeneration.Entity;
using ShortLinkGeneration.Entity.Response;

namespace ShortLinkGeneration.Service.Service;

public interface IInitService
{
    public IRe<InitResponse.InitDb> InitDb();
}