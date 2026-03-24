using Microsoft.Extensions.DependencyInjection;
using RealEstate.BLL.Interfaces;
using RealEstate.BLL.Services;
using RealEstate.DAL.DependencyInjection;
using RealEstate.DAL.Persistence;

var argsMap = ParseArgs(args);
var csvPath = argsMap.TryGetValue("--csv", out var csv) ? csv : "data/import.csv";
var connectionString = argsMap.TryGetValue("--db", out var db)
    ? db
    : "Data Source=realestate.db";

var services = new ServiceCollection();
services.AddRealEstateDal(connectionString);
services.AddScoped<IDataImportService, DataImportService>();

var provider = services.BuildServiceProvider();

using var scope = provider.CreateScope();
var scopedProvider = scope.ServiceProvider;
var dbContext = scopedProvider.GetRequiredService<AppDbContext>();
await dbContext.Database.EnsureCreatedAsync();

var importer = scopedProvider.GetRequiredService<IDataImportService>();

Console.WriteLine($"Starting import from: {csvPath}");
var result = await importer.ImportFromCsvAsync(csvPath);

Console.WriteLine("Import completed:");
Console.WriteLine($"RowsRead: {result.RowsRead}");
Console.WriteLine($"OwnersCreated: {result.OwnersCreated}");
Console.WriteLine($"ApartmentsCreated: {result.ApartmentsCreated}");
Console.WriteLine($"PhotosCreated: {result.PhotosCreated}");
Console.WriteLine($"ReviewsCreated: {result.ReviewsCreated}");
Console.WriteLine($"RatingsCreated: {result.RatingsCreated}");
Console.WriteLine($"ReservationsCreated: {result.ReservationsCreated}");

static Dictionary<string, string> ParseArgs(string[] args)
{
    var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

    for (var i = 0; i < args.Length; i++)
    {
        var key = args[i];
        if (!key.StartsWith("--", StringComparison.Ordinal))
        {
            continue;
        }

        if (i + 1 >= args.Length)
        {
            result[key] = string.Empty;
            continue;
        }

        var value = args[i + 1];
        if (value.StartsWith("--", StringComparison.Ordinal))
        {
            result[key] = string.Empty;
            continue;
        }

        result[key] = value;
        i++;
    }

    return result;
}
