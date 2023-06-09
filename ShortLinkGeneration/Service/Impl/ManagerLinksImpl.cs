using ShortLinkGeneration.DB;
using ShortLinkGeneration.Entity;
using ShortLinkGeneration.Entity.Enum;
using ShortLinkGeneration.Entity.Request;
using ShortLinkGeneration.Entity.Response;
using ShortLinkGeneration.Service.Service;

namespace ShortLinkGeneration.Service.Impl;

public class ManagerLinksImpl : IManagerLinksServer
{
    private readonly ILogger<ManagerLinksImpl> _logger;
    private readonly ShortLinkContext _db;
    private readonly IJwtService _jwtService;

    public ManagerLinksImpl(ILogger<ManagerLinksImpl> logger, ShortLinkContext db, IJwtService jwtService)
    {
        _logger = logger;
        _db = db;
        _jwtService = jwtService;
    }

    public async Task<IRe<ManagerLinksResponse.GetAllLinkResponse>> GetAll(ManagerLinksRequest.GetAllLinkRequest data)
    {
        //分页查找全部链接
        var links = _db.Links
            .OrderByDescending(l => l.CreationDate)
            .Skip((data.Page - 1) * data.PageSize)
            .Take(data.PageSize)
            .ToList();

        int pageCount = (int)Math.Ceiling((double)_db.Links.Count() / data.PageSize);

        //返回结果
        return new Ok<ManagerLinksResponse.GetAllLinkResponse>
        {
            Code = Code.Success,
            Message = "获取成功",
            Data = new ManagerLinksResponse.GetAllLinkResponse
            {
                LinkList = links.Select(link => new ManagerLinksResponse.LinkItemResponse
                {
                    LinkID = link.LinkID,
                    ShortLink = link.ShortLink,
                    LongLink = link.OriginalLink,
                    CreationDate = link.CreationDate,
                    ClickCount = link.ClickCount,
                    ExpiryDate = link.ExpiryDate,
                    MaxClicks = link.MaxClicks,
                    IsDisabled = link.IsDisabled
                }).ToList(),
                PageCount = pageCount
            }
        };
    }

    public Task<IRe<ManagerLinksResponse.GetLinkResponse>> Get(ManagerLinksRequest.GetLinkRequest data)
    {
        //根据ID查找链接
        var link = _db.Links.FirstOrDefault(l => l.LinkID == data.LinkID);

        //判断链接是否存在
        if (link == null)
        {
            return Task.FromResult<IRe<ManagerLinksResponse.GetLinkResponse>>(
                new Error<ManagerLinksResponse.GetLinkResponse>
                {
                    Code = Code.ShortLinkNotExist,
                    Message = "链接不存在"
                });
        }

        //返回结果
        return Task.FromResult<IRe<ManagerLinksResponse.GetLinkResponse>>(
            new Ok<ManagerLinksResponse.GetLinkResponse>
            {
                Code = Code.Success,
                Message = "获取成功",
                Data = new ManagerLinksResponse.GetLinkResponse
                {
                    Link = new ManagerLinksResponse.LinkItemResponse
                    {
                        LinkID = link.LinkID,
                        ShortLink = link.ShortLink,
                        LongLink = link.OriginalLink,
                        CreationDate = link.CreationDate,
                        ClickCount = link.ClickCount,
                        ExpiryDate = link.ExpiryDate,
                        MaxClicks = link.MaxClicks,
                        IsDisabled = link.IsDisabled
                    }
                }
            });
    }

    public async Task<IRe<ManagerLinksResponse.UpdateLinkResponse>> Update(ManagerLinksRequest.UpdateLinkRequest data)
    {
        //根据ID查找链接
        var link = _db.Links.FirstOrDefault(l => l.LinkID == data.LinkID);

        //判断链接是否存在
        if (link == null)
        {
            return await Task.FromResult<IRe<ManagerLinksResponse.UpdateLinkResponse>>(
                new Error<ManagerLinksResponse.UpdateLinkResponse>
                {
                    Code = Code.ShortLinkNotExist,
                    Message = "链接不存在"
                });
        }

        //更新链接
        link.OriginalLink = data.Link.LongLink;
        link.ExpiryDate = data.Link.ExpiryDate;
        link.MaxClicks = data.Link.MaxClicks;

        //保存更改
        await _db.SaveChangesAsync();

        //返回结果
        return await Task.FromResult<IRe<ManagerLinksResponse.UpdateLinkResponse>>(
            new Ok<ManagerLinksResponse.UpdateLinkResponse>
            {
                Code = Code.Success,
                Message = "更新成功"
            });
    }

    public Task<IRe<ManagerLinksResponse.DeleteLinkResponse>> Delete(ManagerLinksRequest.DeleteLinkRequest data)
    {
        //根据ID查找链接
        var link = _db.Links.FirstOrDefault(l => l.LinkID == data.LinkID);

        //判断链接是否存在
        if (link == null)
        {
            return Task.FromResult<IRe<ManagerLinksResponse.DeleteLinkResponse>>(
                new Error<ManagerLinksResponse.DeleteLinkResponse>
                {
                    Code = Code.ShortLinkNotExist,
                    Message = "链接不存在"
                });
        }

        //删除链接
        _db.Links.Remove(link);

        //删除链接点击记录
        _db.Clicks.RemoveRange(_db.Clicks.Where(c => c.LinkID == data.LinkID));

        //保存更改
        _db.SaveChanges();

        //返回结果
        return Task.FromResult<IRe<ManagerLinksResponse.DeleteLinkResponse>>(
            new Ok<ManagerLinksResponse.DeleteLinkResponse>
            {
                Code = Code.Success,
                Message = "删除成功"
            });
    }

    public Task<IRe<ManagerLinksResponse.DisabledLinkResponse>> Disabled(ManagerLinksRequest.DisabledLinkRequest data)
    {
        //根据ID查找链接
        var link = _db.Links.FirstOrDefault(l => l.LinkID == data.LinkID);

        //判断链接是否存在
        if (link == null)
        {
            return Task.FromResult<IRe<ManagerLinksResponse.DisabledLinkResponse>>(
                new Error<ManagerLinksResponse.DisabledLinkResponse>
                {
                    Code = Code.ShortLinkNotExist,
                    Message = "链接不存在"
                });
        }

        //禁用链接
        link.IsDisabled = data.IsDisabled;

        //保存更改
        _db.SaveChanges();

        //返回结果
        return Task.FromResult<IRe<ManagerLinksResponse.DisabledLinkResponse>>(
            new Ok<ManagerLinksResponse.DisabledLinkResponse>
            {
                Code = Code.Success,
                Message = "禁用成功"
            });
    }

    public Task<IRe<ManagerLinksResponse.SearchLinkResponse>> Search(ManagerLinksRequest.SearchLinkRequest data)
    {
        //根据关键字查找链接
        var links = _db.Links
            .Where(l => l.ShortLink.Contains(data.keywords) || l.OriginalLink.Contains(data.keywords))
            .OrderByDescending(l => l.CreationDate)
            .Skip((data.Page - 1) * data.PageSize)
            .Take(data.PageSize)
            .ToList();

        int pageCount =
            (int)Math.Ceiling((double)_db.Links.Count(l =>
                l.ShortLink.Contains(data.keywords) || l.OriginalLink.Contains(data.keywords)) / data.PageSize);

        //返回结果
        return Task.FromResult<IRe<ManagerLinksResponse.SearchLinkResponse>>(
            new Ok<ManagerLinksResponse.SearchLinkResponse>
            {
                Code = Code.Success,
                Message = "获取成功",
                Data = new ManagerLinksResponse.SearchLinkResponse
                {
                    LinkList = links.Select(link => new ManagerLinksResponse.LinkItemResponse
                    {
                        LinkID = link.LinkID,
                        ShortLink = link.ShortLink,
                        LongLink = link.OriginalLink,
                        CreationDate = link.CreationDate,
                        ClickCount = link.ClickCount,
                        ExpiryDate = link.ExpiryDate,
                        MaxClicks = link.MaxClicks,
                        IsDisabled = link.IsDisabled
                    }).ToList(),
                    PageCount = pageCount
                }
            });
    }

    public Task<IRe<ManagerLinksResponse.GetLinkByUserResponse>> GetByUser(
        ManagerLinksRequest.GetLinkByUserRequest data)
    {
        //根据用户ID查找链接
        var links = _db.Links
            .Where(l => l.UserID == data.UserID)
            .OrderByDescending(l => l.CreationDate)
            .Skip((data.Page - 1) * data.PageSize)
            .Take(data.PageSize)
            .ToList();

        int pageCount = (int)Math.Ceiling((double)_db.Links.Count(l => l.UserID == data.UserID) / data.PageSize);

        //返回结果
        return Task.FromResult<IRe<ManagerLinksResponse.GetLinkByUserResponse>>(
            new Ok<ManagerLinksResponse.GetLinkByUserResponse>
            {
                Code = Code.Success,
                Message = "获取成功",
                Data = new ManagerLinksResponse.GetLinkByUserResponse
                {
                    LinkList = links.Select(link => new ManagerLinksResponse.LinkItemResponse
                    {
                        LinkID = link.LinkID,
                        ShortLink = link.ShortLink,
                        LongLink = link.OriginalLink,
                        CreationDate = link.CreationDate,
                        ClickCount = link.ClickCount,
                        ExpiryDate = link.ExpiryDate,
                        MaxClicks = link.MaxClicks,
                        IsDisabled = link.IsDisabled
                    }).ToList(),
                    PageCount = pageCount
                }
            });
    }

    public Task<IRe<ManagerLinksResponse.GetClicksResponse>> GetClicks(ManagerLinksRequest.GetClicksRequest data)
    {
        //根据链接ID查找点击记录
        var clicks = _db.Clicks
            .Where(c => c.LinkID == data.LinkID)
            .OrderByDescending(c => c.ClickTime)
            .Skip((data.Page - 1) * data.PageSize)
            .Take(data.PageSize)
            .ToList();

        int pageCount = (int)Math.Ceiling((double)_db.Clicks.Count(c => c.LinkID == data.LinkID) / data.PageSize);

        //返回结果
        return Task.FromResult<IRe<ManagerLinksResponse.GetClicksResponse>>(
            new Ok<ManagerLinksResponse.GetClicksResponse>
            {
                Code = Code.Success,
                Message = "获取成功",
                Data = new ManagerLinksResponse.GetClicksResponse
                {
                    ClicksList = clicks.Select(click => new ManagerLinksResponse.ClicksItemResponse
                    {
                        ClickID = click.ClickID,
                        LinkID = click.LinkID,
                        ClickTime = click.ClickTime,
                        SourceIP = click.SourceIP
                    }).ToList(),
                    PageCount = pageCount
                }
            });
    }
}