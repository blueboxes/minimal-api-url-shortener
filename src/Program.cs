using Azure.Identity;
var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration(cfgBuilder =>cfgBuilder.AddAzureAppConfiguration(options =>
{
    options
    .Connect(new Uri(builder.Configuration["AppConfig:Endpoint"]), new DefaultAzureCredential())
    .ConfigureRefresh((refreshOptions) =>
    {
        refreshOptions.Register(key: "Settings:Sentinel", refreshAll: true);
        refreshOptions.SetCacheExpiration(TimeSpan.FromSeconds(5));
    });
}));

builder.Services.AddAzureAppConfiguration();
var app = builder.Build();

app.UseDefaultFiles(new DefaultFilesOptions { DefaultFileNames = new  List<string> { "index.html" }}); 
app.UseStaticFiles();

app.MapGet("/{id}", async (string id, HttpContext context, IWebHostEnvironment env) => {
    var url = app.Configuration[id];
    if(url is null){ 
        context.Response.StatusCode = 404;
        return Results.Bytes(await File.ReadAllBytesAsync(System.IO.Path.Combine(env.WebRootPath,"404.html")),"text/html");
    }else{
        return Results.Redirect(url, true, false);
    }
});

app.UseAzureAppConfiguration();//for refresh
app.Run();