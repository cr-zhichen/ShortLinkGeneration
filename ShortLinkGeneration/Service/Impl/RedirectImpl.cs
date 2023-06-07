using ShortLinkGeneration.DB;
using ShortLinkGeneration.Service.Service;

namespace ShortLinkGeneration.Service.Impl;

public class RedirectImpl : IRedirectService
{
    private readonly ILogger<RedirectImpl> _logger;
    private readonly ShortLinkContext _db;

    public RedirectImpl(ILogger<RedirectImpl> logger, ShortLinkContext db)
    {
        _logger = logger;
        _db = db;
    }
}