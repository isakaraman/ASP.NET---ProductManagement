using API.Extensions;
using LoggerService;
using NLog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
LogManager.Setup(options =>
		options.LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/NLog.config")));

builder.Services.ConfigureCors();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServices();
builder.Services.ConfigureDbContext(builder.Configuration);

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
