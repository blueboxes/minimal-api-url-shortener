var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.UseDefaultFiles(new DefaultFilesOptions { DefaultFileNames = new  List<string> { "index.html" }}); 
app.UseStaticFiles();

app.MapGet("/{id}", async (string id, HttpContext context, IWebHostEnvironment env) => {

    var tableClient = new TableClient(new Uri(builder.Configuration["StorageUri"]), "UrlLookup", new Azure.Identity.DefaultAzureCredential());
    var url = tableClient.Query<TableRow>(ent => ent.PartitionKey.Equals("url") && ent.RowKey.Equals(id)).SingleOrDefault();
        
    if(url is null){ 
        context.Response.StatusCode = 404;
        return Results.Bytes(await File.ReadAllBytesAsync(System.IO.Path.Combine(env.WebRootPath,"404.html")),"text/html");
    }else{
        return Results.Redirect(url.TargetUrl, true, false);
    }
}); 

app.Run();
