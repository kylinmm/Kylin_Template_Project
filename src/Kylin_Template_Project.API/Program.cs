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
    // NLog��ע��Nlog���
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
    // NLog: ץȡȫ�ִ���
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // ȷ�����ܳ��쳣ֹͣ�߳� (������linux�б���)
    NLog.LogManager.Shutdown();
}
#endif