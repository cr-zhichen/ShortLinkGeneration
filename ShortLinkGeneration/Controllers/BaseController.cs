using Microsoft.AspNetCore.Mvc;
using ShortLinkGeneration.Repositories.ShortLinkGeneration;

namespace ShortLinkGeneration.Controllers;

[ApiController]
[Route("")]
public class BaseController(IShortLinkGenerationRepository ShortLinkGenerationRepository) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        try
        {
            // 尝试从数据库检索长链接
            var ShortLinkGeneration = await ShortLinkGenerationRepository.GetShortLinkGenerationAsync(id);

            // 重定向到长链接
            return Redirect(ShortLinkGeneration.LongUrl);
        }
        catch (Exception)
        {
            // 如果找不到短链接，则跳转到/
            return Redirect("/");
        }
    }
}
