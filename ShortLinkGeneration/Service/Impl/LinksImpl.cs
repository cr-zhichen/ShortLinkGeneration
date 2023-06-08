using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;
using ShortLinkGeneration.DB;
using ShortLinkGeneration.Entity;
using ShortLinkGeneration.Entity.Enum;
using ShortLinkGeneration.Entity.Request;
using ShortLinkGeneration.Entity.Response;
using ShortLinkGeneration.Service.Service;
using ShortLinkGeneration.Static;
using ShortLinkGeneration.Tool;

namespace ShortLinkGeneration.Service.Impl;

public class LinksImpl : ILinksService
{
    private readonly ILogger<LinksImpl> _logger;
    private readonly ShortLinkContext _db;
    private readonly IJwtService _jwtService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LinksImpl(ILogger<LinksImpl> logger, ShortLinkContext db, IJwtService jwtService,
        IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _db = db;
        _jwtService = jwtService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IRe<LinksResponse.CreateLinkResponse>> Create(LinksRequest.CreateLinkRequest data)
    {
        //从Headers中获取Token
        string token = _httpContextAccessor.HttpContext!.Request.Headers["Authorization"]!.ToString().Split(' ').Last();

        if (data.ShortLink is not null && data.ShortLink.Length != 0 && !JudgeShortConnection(data.ShortLink))
        {
            return new Ok<LinksResponse.CreateLinkResponse>
            {
                Code = Code.ShortLinkExists,
                Message = "短连接已存在"
            };
        }

        //判断长连接格式
        if (!data.LongLink.IsUrl())
        {
            return new Error<LinksResponse.CreateLinkResponse>
            {
                Code = Code.LongLinkFormatError,
                Message = "长连接格式错误"
            };
        }

        string shortLink;
        //Token不存在，直接生成短链接
        if (token == "")
        {
            try
            {
                shortLink = CreateShortLink(data.ShortLink, data.LongLink, data.ExpiryDate, data.MaxClicks);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "生成短链接失败");
                return new Error<LinksResponse.CreateLinkResponse>
                {
                    Code = Code.ShortLinkGenerationFailed,
                    Message = "短链接生成失败"
                };
            }

            //将短链接存入数据库
            _db.Links.Add(new Link()
            {
                ShortLink = shortLink,
                OriginalLink = data.LongLink,
                UserID = null,
                CreationDate = DateTime.Now,
                ClickCount = 0,
                ExpiryDate = data.ExpiryDate,
                MaxClicks = data.MaxClicks,
                IsDisabled = false
            });

            //保存数据库
            await _db.SaveChangesAsync();

            return new Ok<LinksResponse.CreateLinkResponse>
            {
                Code = Code.Success,
                Message = "短链接生成成功",
                Data = new LinksResponse.CreateLinkResponse
                {
                    ShortLink = shortLink
                }
            };
        }
        else
        {
            //Token存在，判断Token是否有效
            var isValid = _jwtService.ValidateTokenAsync(token, "Admin").Result;
            //判断令牌是否在缓存中
            isValid = isValid && TokenList.TokenLists.Any(x => x.Token == token);

            if (!isValid)
            {
                return new Error<LinksResponse.CreateLinkResponse>
                {
                    Code = Code.TokenError,
                    Message = "Token错误"
                };
            }

            //根据用户名查找用户id
            var userID = _db.Users
                .FirstOrDefault(x => x.Username == _jwtService.GetUsernameAsync(token).Result.ToString())?.UserID;

            if (userID is null)
            {
                return new Error<LinksResponse.CreateLinkResponse>
                {
                    Code = Code.UserNotExist,
                    Message = "用户不存在"
                };
            }

            try
            {
                shortLink = CreateShortLink(data.ShortLink, data.LongLink, data.ExpiryDate, data.MaxClicks);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "生成短链接失败");
                return new Error<LinksResponse.CreateLinkResponse>
                {
                    Code = Code.ShortLinkGenerationFailed,
                    Message = "短链接生成失败"
                };
            }

            //生成短链接
            //将短链接存入数据库
            _db.Links.Add(new Link()
            {
                ShortLink = shortLink,
                OriginalLink = data.LongLink,
                UserID = userID,
                CreationDate = DateTime.Now,
                ClickCount = 0,
                ExpiryDate = data.ExpiryDate,
                MaxClicks = data.MaxClicks,
                IsDisabled = false
            });

            //保存数据库
            await _db.SaveChangesAsync();

            return new Ok<LinksResponse.CreateLinkResponse>
            {
                Code = Code.Success,
                Message = "短链接生成成功",
                Data = new LinksResponse.CreateLinkResponse
                {
                    ShortLink = shortLink
                }
            };
        }
    }

    public async Task<IRe<LinksResponse.DetectionResponse>> Detection(LinksRequest.DetectionRequest data)
    {
        //是否满足条件
        var isAvailable = JudgeShortConnection(data.ShortLink);

        if (isAvailable)
        {
            return new Ok<LinksResponse.DetectionResponse>
            {
                Code = Code.Success,
                Message = "短链接可用"
            };
        }
        else
        {
            return new Error<LinksResponse.DetectionResponse>
            {
                Code = Code.ShortLinkExists,
                Message = "短链接不可用"
            };
        }
    }

    public async Task<IRe<LinksResponse.GetAllLinkResponse>> GetAll(LinksRequest.GetAllLinkRequest data)
    {
        //从Headers中获取Token
        string token = _httpContextAccessor.HttpContext!.Request.Headers["Authorization"]!.ToString().Split(' ').Last();

        //从Token中获取用户名
        var username = _jwtService.GetUsernameAsync(token).Result.ToString();

        //根据用户名查找用户id
        var userID = _db.Users.FirstOrDefault(x => x.Username == username)?.UserID;


        //根据用户id查找短链接
        var linkList = _db.Links
            .Where(x => x.UserID == userID)
            .Skip((data.Page - 1) * data.PageSize)
            .Take(data.PageSize)
            .ToList();

        var pageCount = (int)Math.Ceiling((double)_db.Links.Count(x => x.UserID == userID) / data.PageSize);

        //返回数据
        return new Ok<LinksResponse.GetAllLinkResponse>
        {
            Code = Code.Success,
            Message = "获取成功",
            Data = new LinksResponse.GetAllLinkResponse
            {
                LinkList = linkList.Select(x => new LinksResponse.LinkItemResponse
                {
                    LinkID = x.LinkID,
                    ShortLink = x.ShortLink,
                    LongLink = x.OriginalLink,
                    CreationDate = x.CreationDate,
                    ClickCount = x.ClickCount,
                    ExpiryDate = x.ExpiryDate,
                    MaxClicks = x.MaxClicks,
                    IsDisabled = x.IsDisabled
                }).ToList(),
                PageCount = pageCount
            }
        };
    }

    public async Task<IRe<LinksResponse.GetLinkResponse>> Get(LinksRequest.GetLinkRequest data)
    {
        //从Headers中获取Token
        string token = _httpContextAccessor.HttpContext!.Request.Headers["Authorization"]!.ToString().Split(' ').Last();

        //从Token中获取用户名
        var username = _jwtService.GetUsernameAsync(token).Result.ToString();

        //根据用户名查找用户id
        var userID = _db.Users.FirstOrDefault(x => x.Username == username)?.UserID;

        //根据短链接查找短链接信息
        var link = _db.Links.FirstOrDefault(x => x.LinkID == data.LinkID && x.UserID == userID);

        //判断短链接是否存在
        if (link is null)
        {
            return new Error<LinksResponse.GetLinkResponse>
            {
                Code = Code.ShortLinkNotExist,
                Message = "短链接不存在"
            };
        }

        //返回数据
        return new Ok<LinksResponse.GetLinkResponse>
        {
            Code = Code.Success,
            Message = "获取成功",
            Data = new LinksResponse.GetLinkResponse
            {
                Link = new LinksResponse.LinkItemResponse
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
        };
    }

    public async Task<IRe<LinksResponse.SearchLinkResponse>> Search(LinksRequest.SearchLinkRequest data)
    {
        //从Headers中获取Token
        string token = _httpContextAccessor.HttpContext!.Request.Headers["Authorization"]!.ToString().Split(' ').Last();

        //从Token中获取用户名
        var username = _jwtService.GetUsernameAsync(token).Result.ToString();

        //根据用户名查找用户id
        var userID = _db.Users.FirstOrDefault(x => x.Username == username)?.UserID;

        //模糊查找短链接
        var linkList = _db.Links
            .Where(x => x.UserID == userID &&
                        (x.ShortLink.Contains(data.keywords) ||
                         x.OriginalLink.Contains(data.keywords) ||
                         x.LinkID.ToString().Contains(data.keywords)))
            .Skip((data.Page - 1) * data.PageSize)
            .Take(data.PageSize)
            .ToList();

        var pageCount = (int)Math.Ceiling(
            (double)_db.Links.Count(x =>
                x.UserID == userID &&
                (x.ShortLink.Contains(data.keywords) ||
                 x.OriginalLink.Contains(data.keywords) ||
                 x.LinkID.ToString().Contains(data.keywords))) /
            data.PageSize);

        //返回数据
        return new Ok<LinksResponse.SearchLinkResponse>
        {
            Code = Code.Success,
            Message = "获取成功",
            Data = new LinksResponse.SearchLinkResponse
            {
                LinkList = linkList.Select(x => new LinksResponse.LinkItemResponse
                {
                    LinkID = x.LinkID,
                    ShortLink = x.ShortLink,
                    LongLink = x.OriginalLink,
                    CreationDate = x.CreationDate,
                    ClickCount = x.ClickCount,
                    ExpiryDate = x.ExpiryDate,
                    MaxClicks = x.MaxClicks,
                    IsDisabled = x.IsDisabled
                }).ToList(),
                PageCount = pageCount
            }
        };
    }

    public async Task<IRe<LinksResponse.UpdateLinkResponse>> Update(LinksRequest.UpdateLinkRequest data)
    {
        //从Headers中获取Token
        string token = _httpContextAccessor.HttpContext!.Request.Headers["Authorization"]!.ToString().Split(' ').Last();

        //从Token中获取用户名
        var username = _jwtService.GetUsernameAsync(token).Result.ToString();

        //根据用户名查找用户id
        var userID = _db.Users.FirstOrDefault(x => x.Username == username)?.UserID;

        //根据短链接查找短链接信息
        var link = _db.Links.FirstOrDefault(x => x.LinkID == data.LinkID && x.UserID == userID);

        //判断短链接是否存在
        if (link is null)
        {
            return new Error<LinksResponse.UpdateLinkResponse>
            {
                Code = Code.ShortLinkNotExist,
                Message = "短链接不存在"
            };
        }

        //更新短链接信息
        link.OriginalLink = data.Link.LongLink;
        link.ExpiryDate = data.Link.ExpiryDate;
        link.MaxClicks = data.Link.MaxClicks;

        //保存更改
        await _db.SaveChangesAsync();

        //返回数据
        return new Ok<LinksResponse.UpdateLinkResponse>
        {
            Code = Code.Success,
            Message = "更新成功",
            Data = new LinksResponse.UpdateLinkResponse
            {
                Link = new LinksResponse.LinkItemResponse
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
        };
    }

    public async Task<IRe<LinksResponse.DeleteLinkResponse>> Delete(LinksRequest.DeleteLinkRequest data)
    {
        //从Headers中获取Token
        string token = _httpContextAccessor.HttpContext!.Request.Headers["Authorization"]!.ToString().Split(' ').Last();

        //从Token中获取用户名
        var username = _jwtService.GetUsernameAsync(token).Result.ToString();

        //根据用户名查找用户id
        var userID = _db.Users.FirstOrDefault(x => x.Username == username)?.UserID;

        //根据短链接查找短链接信息
        var link = _db.Links.FirstOrDefault(x => x.LinkID == data.LinkID && x.UserID == userID);

        //判断短链接是否存在
        if (link is null)
        {
            return new Error<LinksResponse.DeleteLinkResponse>
            {
                Code = Code.ShortLinkNotExist,
                Message = "短链接不存在"
            };
        }

        //删除短连接点击记录
        _db.Clicks.RemoveRange(_db.Clicks.Where(x => x.LinkID == link.LinkID));

        //删除短链接
        _db.Links.Remove(link);

        //保存更改
        await _db.SaveChangesAsync();

        //返回数据
        return new Ok<LinksResponse.DeleteLinkResponse>
        {
            Code = Code.Success,
            Message = "删除成功"
        };
    }

    #region 工具方法

    /// <summary>
    /// 游客生成短连接
    /// </summary>
    /// <param name="shortLink"></param>
    /// <param name="longLink"></param>
    /// <param name="expiryDate"></param>
    /// <returns></returns>
    private string CreateShortLink(string? shortLink, string longLink, DateTime? expiryDate, int? maxClicks)
    {
        string tempShortLink = shortLink ?? "";
        int retryCount = 0;
        const int maxRetries = 1000; // 设置最大重试次数

        //生成短链接
        while (shortLink is null || shortLink.Length == 0 || !JudgeShortConnection(tempShortLink))
        {
            if (++retryCount > maxRetries)
            {
                throw new Exception("无法生成短链接");
            }

            tempShortLink = RandomlyGenerateShortLinks();
        }

        shortLink = tempShortLink;

        return shortLink;
    }

    /// <summary>
    /// 随机生成短连接
    /// </summary>
    /// <returns></returns>
    public string RandomlyGenerateShortLinks()
    {
        // 可以使用的字符
        char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

        // 创建一个随机对象
        Random random = new Random();

        // 初始化结果字符串
        string shortLink = string.Empty;

        // 循环5次，每次随机选择一个字符加入结果字符串
        for (int i = 0; i < 5; i++)
        {
            shortLink += chars[random.Next(chars.Length)];
        }

        return shortLink;
    }


    /// <summary>
    /// 判断短连接是否可用
    /// </summary>
    /// <param name="shortLink"></param>
    /// <returns></returns>
    private bool JudgeShortConnection(string shortLink)
    {
        return DetermineTheShortConnectionFormat(shortLink) && !_db.Links.Any(x => x.ShortLink == shortLink);
    }

    /// <summary>
    /// 判断短连接格式是否正确
    /// </summary>
    /// <param name="shortLink"></param>
    /// <returns></returns>
    private bool DetermineTheShortConnectionFormat(string shortLink)
    {
        if (string.IsNullOrEmpty(shortLink))
        {
            return false;
        }

        // 使用正则表达式判断格式是否正确
        string pattern = @"^[a-z0-9_-]+$";
        Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

        return regex.IsMatch(shortLink);
    }

    #endregion
}