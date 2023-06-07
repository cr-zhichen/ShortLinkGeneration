using Microsoft.AspNetCore.Mvc;

namespace ShortLinkGeneration.Service.Service;

public interface IRedirectService
{
    public Task<IActionResult> Redirect(string shortLink);
}