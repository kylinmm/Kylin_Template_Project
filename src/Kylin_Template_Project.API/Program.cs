#if (EnableNlog)
using NLog;
using NLog.Web;
#endif

#if (EnableNlog)
var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");
try
{
#endif

var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

#if (EnableNlog)
    // NLog：注册Nlog组件
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
#endif


builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

#if (EnableNlog)
}
catch (Exception exception)
{
    // NLog: 抓取全局错误
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // 确保在跑出异常停止线程 (避免在linux中报错)
    NLog.LogManager.Shutdown();
}
#endif