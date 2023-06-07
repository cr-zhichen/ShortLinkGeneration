using Microsoft.AspNetCore.Mvc;
using ShortLinkGeneration.Entity;
using ShortLinkGeneration.Entity.Request;
using ShortLinkGeneration.Entity.Response;

namespace ShortLinkGeneration.Service.Service;

public interface IRedirectService
{
    public Task<IActionResult> Redirect(string shortLink);

    public Task<IRe<RedirectResponse.RedirectPostResponse>> RedirectPost(
        RedirectRequest.RedirectPostRequest data);
}