using Microsoft.AspNetCore.Mvc;
using ShortLinkGeneration.Repositories.ShortLinkGeneration;

namespace ShortLinkGeneration.Controllers;

[ApiController]
[Route("")]
public class BaseController(IShortLinkGenerationRepository ShortLinkGenerationRepository) : ControllerBase
{
    [HttpGet("{shortUrl}")]
    public async Task<IActionResult> Get(string shortUrl)
    {
        try
        {
            // 尝试从数据库检索长链接
            var shortLinkGeneration = await ShortLinkGenerationRepository.GetShortLinkGenerationAsync(shortUrl);

            // 点击次数 +1
            await ShortLinkGenerationRepository.AddClickCountAsync(shortLinkGeneration.ShortId);

            // 重定向到长链接
            return Redirect(shortLinkGeneration.LongUrl);
        }
        catch (Exception)
        {
            // 如果找不到短链接，则跳转到/
            return Redirect("/");
        }
    }
}
