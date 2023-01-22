//this code adds X records to the table
//for use with load testing
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder().AddJsonFile($"appsettings.json", false, false);
var config = builder.Build();

Console.WriteLine($"Uri {config["StorageUri"]}");

var tableClient = new TableClient(new Uri(config["StorageUri"]), "UrlLookup", new Azure.Identity.DefaultAzureCredential());

var item = new TableRow();
item.TargetUrl = "about:blank";
item.PartitionKey = "url";
item.RowKey = "001-test";
tableClient.AddEntity(item);

Console.WriteLine("Item Added");