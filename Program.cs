using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Prometheus;
using NLog;
using NLog.Web;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Info("MY API!");

try
{


    var builder = WebApplication.CreateBuilder(args);
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();

    // Add services to the container.

    builder.Services.AddControllers();

    builder.Services.AddDbContext<OfferAndFindAPI.Models.Offer_And_FindContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));

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

    app.UseHttpsRedirection();

    app.UseAuthorization();
    app.UseRouting();
    app.UseHttpMetrics();
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapMetrics();
    });
    app.MapControllers();

    app.Run();

}
catch (Exception ex){
    logger.Error(ex, "API STOP");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}
