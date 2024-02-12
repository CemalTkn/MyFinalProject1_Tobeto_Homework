using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.Autofac;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using Core.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Core.DependencyResolvers;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using static Serilog.Sinks.MSSqlServer.ColumnOptions;
using System.Data;
using System.Security.Claims;
using Entities.Concrete;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new AutofacBusinessModule());
});


// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddSingleton<IProductService, ProductManager>();
//builder.Services.AddSingleton<IProductDal, EfProductDal>();


Logger log = new LoggerConfiguration()
    .WriteTo.Console()//WriteTo ile loglama yapacaðýmýz kaynaðý belirtiyoruz yani console'a loglama yap.
    .WriteTo.File("logs/log.txt")
    .WriteTo.MSSqlServer(builder.Configuration.GetConnectionString("ETradeC"),
    sinkOptions: new MSSqlServerSinkOptions { TableName = "Log", AutoCreateSqlTable = true },
    columnOptions:new ColumnOptions()
    {
        Message = { ColumnName = "message" },
        MessageTemplate = { ColumnName = "message_template" },
        Level = { ColumnName = "level"},
        TimeStamp = { ColumnName = "time_stamp" },
        Exception= { ColumnName = "exception" },
        LogEvent = { ColumnName = "log_event"}
    })
    .CreateLogger();

builder.Host.UseSerilog(log);


var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey),
            NameClaimType = ClaimTypes.Name//Jwt üzerinde Name claime karþýlýk gelen deðeri User.ýdentity.name propertysinden elde edebiliriz.
            
        };
    });

// CoreModel gibi modülleri eklemek için, farklý moduleler eklenirse onlarýda 
builder.Services.AddDependencyResolvers(new ICoreModule[] { new CoreModule() }); //,new ÖrnekModule() gibi ekleyebiliriz.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureCustomExceptionMiddleware();

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
