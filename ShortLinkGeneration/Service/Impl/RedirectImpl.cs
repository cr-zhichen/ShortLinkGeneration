using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShortLinkGeneration.DB;
using ShortLinkGeneration.Entity;
using ShortLinkGeneration.Entity.Enum;
using ShortLinkGeneration.Entity.Request;
using ShortLinkGeneration.Entity.Response;
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

    public async Task<IActionResult> Redirect(string shortLink)
    {
        //判断短链接是否存在
        var link = await _db.Links.FirstOrDefaultAsync(x => x.ShortLink == shortLink);

        //判断短链接是否不存在，或者已经被禁用，或者已经过期
        if (link == null || link.IsDisabled || link.ExpiryDate < DateTime.Now)
        {
            var errorObject = new Error<object>
            {
                Code = Code.ShortLinkNotExist,
                Message = "短连接不存在"
            };
            return new JsonResult(errorObject) { StatusCode = 404 };
        }

        //重定向到外部链接
        return new RedirectResult(link.OriginalLink);
    }

    public async Task<IRe<RedirectResponse.RedirectPostResponse>> RedirectPost(RedirectRequest.RedirectPostRequest data)
    {
        //判断短链接是否存在
        var link = _db.Links.FirstOrDefault(x => x.ShortLink == data.ShortLink);

        //判断短链接是否不存在，或者已经被禁用，或者已经过期
        if (link == null || link.IsDisabled || link.ExpiryDate < DateTime.Now)
        {
            return new Error<RedirectResponse.RedirectPostResponse>
            {
                Code = Code.ShortLinkNotExist,
                Message = "短连接不存在"
            };
        }

        //返回长链接
        return new Ok<RedirectResponse.RedirectPostResponse>
        {
            Code = Code.Success,
            Message = "成功",
            Data = new RedirectResponse.RedirectPostResponse
            {
                LongLink = link.OriginalLink
            }
        };
    }
}