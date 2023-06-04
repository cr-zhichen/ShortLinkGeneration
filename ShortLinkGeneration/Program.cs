using Microsoft.EntityFrameworkCore;
using ShortLinkGeneration.DB;
using ShortLinkGeneration.Service.Impl;
using ShortLinkGeneration.Service.Service;
using System.IO;
using System.Reflection;

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//强制使用HTTPS
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();