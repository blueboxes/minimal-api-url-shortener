# Minimal Api and Azure App Config Url Shortener 

This is a C# project to show creating a URL shortener using Minimal Apis  and Azure App config.

For more details on how it is was build and how to set the project up see https://goto42.dev/about

## Running the Project
This project is written in dotnet 6 and can be run locally after initial setup.

1. Create an Azure Storage Account
1. Rename `appsettings.devexample.json` to `appsettings.Development` and set the `StorageUri` value.
1. Create a table named `UrlLookUp`
1. Assign your azure account `Storage Table Data Reader` permission on the storage account
1. Login to Azure Cli
1. From the src folder run `dotnet run`

## Adding Links
This project does not yet have a UI to add new links. To add any new links add a new entry to the storage table. As we do this we need to set the partition key to `url`, the row key as the short Url vault and a new property of `TargetUrl` with the value of the destination Url.

## Todo:
- complete readme with full setup instructions
- API filters to allow other pages
- Unit and integration tests
- Support for adding Urls
- Build script
- Bicep templates for deployment

# Release Notes
0.1.0 - Initial Commit