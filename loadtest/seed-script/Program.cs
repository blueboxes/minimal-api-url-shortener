//this code adds X records to the table
//for use with load testing
using Microsoft.Extensions.Configuration;

const int TestTotal = 1000;

var builder = new ConfigurationBuilder().AddJsonFile($"appsettings.json", false, false);
var config = builder.Build();
var tableClient = new TableClient(new Uri(config["StorageUri"]), "UrlLookup", new Azure.Identity.AzureCliCredential());

for (int i = 0; i < TestTotal; i++)
{
    var prefix = i.ToString().PadLeft(3,'0');
 
    var item = new TableRow();
    item.TargetUrl = "https://www.google.co.uk/";
    item.RowKey = $"{prefix}-test";
    item.PartitionKey = item.RowKey.Substring(0,2);
    tableClient.AddEntity(item);
    Console.WriteLine($"Item {i+1} Added");
}