using Microsoft.EntityFrameworkCore;
using ShortLinkGeneration.DB;
using ShortLinkGeneration.Service.Impl;
using ShortLinkGeneration.Service.Service;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ShortLinkGeneration.Service;
using ShortLinkGeneration.Tool;
using ShortLinkGeneration.Tool.JWT;

var baseDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = baseDirectory,
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//连接mysql
builder.Services.AddDbContext<ShortLinkContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("ShortLinkContext"),
        new MySqlServerVersion(new Version())));

//注入服务
builder.Services.AddScoped<IInitService, InitImpl>();
builder.Services.AddScoped<IUsersService, UsersImpl>();

builder.Services.Configure<ConfigOptions>(builder.Configuration.GetSection("SMTP"));

#region 配置JWT

var section = builder.Configuration.GetSection("TokenOptions"); // 获取TokenOptions配置
var tokenOptions = section.Get<TokenOptions>();

builder.Services.AddTransient<IJwtService, JwtService>(); // 注册Jwt服务到容器
builder.Services.Configure<TokenOptions>(section); // 注入IOptions需要这个
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true, //是否在令牌期间验证签发者
            ValidateAudience = true, //是否验证接收者
            ValidateLifetime = true, //是否验证失效时间
            ValidateIssuerSigningKey = true, //是否验证签名
            ValidAudience = tokenOptions.Audience, //接收者
            ValidIssuer = tokenOptions.Issuer, //签发者，签发的Token的人
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecretKey))
        };
    });

#endregion

#region 配置swagger

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new() { Title = "短链接生成器", Version = "v1", Description = "" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "使用 Bearer 方案的 JWT 授权标头。",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "bearer",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT"
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new List<string>()
        }
    });
});

#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//强制使用HTTPS
// app.UseHttpsRedirection();

// 注意顺序，不然 401
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();