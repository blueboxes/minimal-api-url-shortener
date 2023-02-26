var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.UseDefaultFiles(new DefaultFilesOptions { DefaultFileNames = new  List<string> { "index.html" }}); 
app.UseStaticFiles();

app.MapGet("/{id}", async (string id, HttpContext context, IWebHostEnvironment env) => {

    string? targetUrl = null;

    if(id.Length >= 2){
        var tableClient = new TableClient(new Uri(builder.Configuration["StorageUri"] ?? throw new NullReferenceException("StorageUri Missing")), "UrlLookup", new Azure.Identity.DefaultAzureCredential());
        targetUrl = tableClient.Query<TableRow>(ent => ent.PartitionKey.Equals(id.Substring(0,2)) && ent.RowKey.Equals(id)).SingleOrDefault()?.TargetUrl;
    }
        
    if(targetUrl is null){ 
        context.Response.StatusCode = 404;
        return Results.Bytes(await File.ReadAllBytesAsync(System.IO.Path.Combine(env.WebRootPath,"404.html")),"text/html");
    }else{
        return Results.Redirect(targetUrl, true, false);
    }
}); 

app.Run();
