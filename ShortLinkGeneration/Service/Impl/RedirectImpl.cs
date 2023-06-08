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
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RedirectImpl(ILogger<RedirectImpl> logger, ShortLinkContext db, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _db = db;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IActionResult> Redirect(string shortLink)
    {
        //判断短链接是否存在
        var link = await _db.Links.FirstOrDefaultAsync(x => x.ShortLink == shortLink);

        //判断短链接是否不存在，或者已经被禁用，或者已经过期
        if (link == null || link.IsDisabled || link.ExpiryDate < DateTime.Now ||
            (link.MaxClicks != null && link.ClickCount >= link.MaxClicks))
        {
            var errorObject = new Error<object>
            {
                Code = Code.ShortLinkNotExist,
                Message = "短连接不存在"
            };
            return new JsonResult(errorObject) { StatusCode = 404 };
        }

        //保存访问记录
        await _db.Clicks.AddAsync(new Click
        {
            LinkID = link.LinkID,
            ClickTime = DateTime.Now,
            SourceIP = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString()
        });

        //连接访问次数+1
        link.ClickCount++;

        await _db.SaveChangesAsync();

        //重定向到外部链接
        return new RedirectResult(link.OriginalLink);
    }

    public async Task<IRe<RedirectResponse.RedirectPostResponse>> RedirectPost(RedirectRequest.RedirectPostRequest data)
    {
        //判断短链接是否存在
        var link = _db.Links.FirstOrDefault(x => x.ShortLink == data.ShortLink);

        //判断短链接是否不存在，或者已经被禁用，或者已经过期
        if (link == null || link.IsDisabled || link.ExpiryDate < DateTime.Now ||
            (link.MaxClicks != null && link.ClickCount >= link.MaxClicks))
        {
            return new Error<RedirectResponse.RedirectPostResponse>
            {
                Code = Code.ShortLinkNotExist,
                Message = "短连接不存在"
            };
        }

        //保存访问记录
        await _db.Clicks.AddAsync(new Click
        {
            LinkID = link.LinkID,
            ClickTime = DateTime.Now,
            SourceIP = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString()
        });

        //连接访问次数+1
        link.ClickCount++;

        await _db.SaveChangesAsync();

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