using BLL.POCOs;
using BLL.Services.Implementation;
using BLL.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("https://localhost:7140", "https://localhost:7141", "https://localhost:7142", "https://localhost:7143", "https://localhost:7144");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IFileService, FileService>();

builder.Services.Configure<NodeConfig>(builder.Configuration.GetSection("NodeConfig"));

builder.Services.AddControllers();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();


app.Run();

