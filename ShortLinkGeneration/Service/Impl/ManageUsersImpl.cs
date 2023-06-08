using ShortLinkGeneration.DB;
using ShortLinkGeneration.Entity;
using ShortLinkGeneration.Entity.Enum;
using ShortLinkGeneration.Entity.Request;
using ShortLinkGeneration.Entity.Response;
using ShortLinkGeneration.Service.Service;
using ShortLinkGeneration.Static;
using ShortLinkGeneration.Tool;

namespace ShortLinkGeneration.Service.Impl;

public class ManageUsersImpl : IManageUsersService
{
    private readonly ILogger<ManageUsersImpl> _logger;
    private readonly ShortLinkContext _db;
    private readonly IJwtService _jwtService;

    public ManageUsersImpl(ILogger<ManageUsersImpl> logger, ShortLinkContext db, IJwtService jwtService)
    {
        _logger = logger;
        _db = db;
        _jwtService = jwtService;
    }


    public async Task<IRe<ManageUsersResponse.GetAllUserResponse>> GetAll(ManageUsersRequest.GetAllUserRequest data)
    {
        //分页查找全部用户
        var users = _db.Users
            .Skip((data.Page - 1) * data.PageSize)
            .Take(data.PageSize)
            .ToList();

        int pageCount = (int)Math.Ceiling((double)_db.Users.Count() / data.PageSize);

        //返回结果
        return new Ok<ManageUsersResponse.GetAllUserResponse>
        {
            Code = Code.Success,
            Message = "获取成功",
            Data = new ManageUsersResponse.GetAllUserResponse
            {
                UsersList = users.Select(user => new ManageUsersResponse.UserItemResponse()
                {
                    UserID = user.UserID,
                    Username = user.Username,
                    Role = user.Role,
                    CreationTime = user.CreationTime,
                    LinkCount = _db.Links.Count(link => link.UserID == user.UserID)
                }).ToList(),
                PageCount = pageCount
            }
        };
    }

    public async Task<IRe<ManageUsersResponse.CreateUserResponse>> Create(ManageUsersRequest.CreateUserRequest data)
    {
        //创建用户
        var user = new User
        {
            Username = data.Username,
            PasswordHash = data.Password.HashPassword(data.Username),
            Role = data.Role,
            CreationTime = DateTime.Now
        };
        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        //返回结果
        return new Ok<ManageUsersResponse.CreateUserResponse>
        {
            Code = Code.Success,
            Message = "创建成功"
        };
    }


    public async Task<IRe<ManageUsersResponse.GetUserResponse>> Get(ManageUsersRequest.GetUserRequest data)
    {
        //根据用户ID查询用户
        var user = _db.Users.FirstOrDefault(user => user.UserID == data.UserID);

        //判断用户是否存在
        if (user == null)
        {
            return await Task.FromResult<IRe<ManageUsersResponse.GetUserResponse>>(
                new Error<ManageUsersResponse.GetUserResponse>
                {
                    Code = Code.UserNotExist,
                    Message = "用户不存在"
                });
        }


        //返回结果
        return await Task.FromResult<IRe<ManageUsersResponse.GetUserResponse>>(
            new Ok<ManageUsersResponse.GetUserResponse>
            {
                Code = Code.Success,
                Message = "获取成功",
                Data = new ManageUsersResponse.GetUserResponse
                {
                    User = new ManageUsersResponse.UserItemResponse
                    {
                        UserID = user.UserID,
                        Username = user.Username,
                        Role = user.Role,
                        CreationTime = user.CreationTime,
                        LinkCount = _db.Links.Count(link => link.UserID == user.UserID)
                    }
                }
            });
    }

    public async Task<IRe<ManageUsersResponse.DeleteUserResponse>> Delete(ManageUsersRequest.DeleteUserRequest data)
    {
        //判断用户是否为管理员
        if (data.UserID == 1)
        {
            return await Task.FromResult<IRe<ManageUsersResponse.DeleteUserResponse>>(
                new Error<ManageUsersResponse.DeleteUserResponse>
                {
                    Code = Code.UserNotExist,
                    Message = "不能删除管理员"
                });
        }

        //根据用户ID查询用户
        var user = _db.Users.FirstOrDefault(user => user.UserID == data.UserID);

        //判断用户是否存在
        if (user == null)
        {
            return await Task.FromResult<IRe<ManageUsersResponse.DeleteUserResponse>>(
                new Error<ManageUsersResponse.DeleteUserResponse>
                {
                    Code = Code.UserNotExist,
                    Message = "用户不存在"
                });
        }

        //删除用户所有短连接的点击记录
        _db.Clicks.RemoveRange(_db.Clicks.Where(click => click.Link.UserID == data.UserID));

        //删除用户所有的短连接
        _db.Links.RemoveRange(_db.Links.Where(link => link.UserID == data.UserID));

        //删除用户
        _db.Users.Remove(user);

        //注销令牌
        await _jwtService.LogoutByUsernameAsync(user.Username);

        await _db.SaveChangesAsync();

        //返回结果
        return await Task.FromResult<IRe<ManageUsersResponse.DeleteUserResponse>>(
            new Ok<ManageUsersResponse.DeleteUserResponse>
            {
                Code = Code.Success,
                Message = "删除成功"
            });
    }

    public async Task<IRe<ManageUsersResponse.RestUserPasswordResponse>> RestPassword(
        ManageUsersRequest.RestUserPasswordRequest data)
    {
        //根据用户ID查询用户
        var user = _db.Users.FirstOrDefault(user => user.UserID == data.UserID);

        //判断用户是否存在
        if (user == null)
        {
            return await Task.FromResult<IRe<ManageUsersResponse.RestUserPasswordResponse>>(
                new Error<ManageUsersResponse.RestUserPasswordResponse>
                {
                    Code = Code.UserNotExist,
                    Message = "用户不存在"
                });
        }

        //判断新密码格式
        if (!data.NewPassword.IsPassword())
        {
            return await Task.FromResult<IRe<ManageUsersResponse.RestUserPasswordResponse>>(
                new Error<ManageUsersResponse.RestUserPasswordResponse>
                {
                    Code = Code.PasswordFormatError,
                    Message = "密码格式错误"
                });
        }

        //重置密码
        user.PasswordHash = data.NewPassword.HashPassword(user.Username);

        //注销令牌
        await _jwtService.LogoutByUsernameAsync(user.Username);

        //保存更改
        await _db.SaveChangesAsync();

        //返回结果
        return await Task.FromResult<IRe<ManageUsersResponse.RestUserPasswordResponse>>(
            new Ok<ManageUsersResponse.RestUserPasswordResponse>
            {
                Code = Code.Success,
                Message = "重置成功"
            });
    }

    public async Task<IRe<ManageUsersResponse.SearchUserResponse>> Search(ManageUsersRequest.SearchUserRequest data)
    {
        //分页模糊查询用户
        var users = _db.Users
            .Where(user => user.Username.Contains(data.Keyword) || user.UserID.ToString().Contains(data.Keyword))
            .Skip((data.Page - 1) * data.PageSize)
            .Take(data.PageSize)
            .ToList();

        int pageCount = (int)Math.Ceiling((double)_db.Users.Count(user =>
            user.Username.Contains(data.Keyword) || user.UserID.ToString().Contains(data.Keyword)) / data.PageSize);

        //返回结果
        return new Ok<ManageUsersResponse.SearchUserResponse>
        {
            Code = Code.Success,
            Message = "获取成功",
            Data = new ManageUsersResponse.SearchUserResponse
            {
                UsersList = users.Select(user => new ManageUsersResponse.UserItemResponse()
                {
                    UserID = user.UserID,
                    Username = user.Username,
                    Role = user.Role,
                    CreationTime = user.CreationTime,
                    LinkCount = _db.Links.Count(link => link.UserID == user.UserID)
                }).ToList(),
                PageCount = pageCount
            }
        };
    }
}