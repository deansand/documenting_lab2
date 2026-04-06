using CsvHelper;
using System.Globalization;

var argsMap = ParseArgs(args);
var outputPath = argsMap.TryGetValue("--output", out var output) ? output : "data/import.csv";
var rowCount = argsMap.TryGetValue("--rows", out var rowsRaw) && int.TryParse(rowsRaw, out var rows)
    ? Math.Max(rows, 1000)
    : 1200;

var directory = Path.GetDirectoryName(outputPath);
if (!string.IsNullOrWhiteSpace(directory))
{
    Directory.CreateDirectory(directory);
}

var statuses = new[] { "Pending", "Confirmed", "Cancelled", "Completed" };
var random = new Random(2026);

using var writer = new StreamWriter(outputPath);
using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

csv.WriteField("OwnerName");
csv.WriteField("OwnerContactInfo");
csv.WriteField("ApartmentAddress");
csv.WriteField("ApartmentPrice");
csv.WriteField("ApartmentRoomCount");
csv.WriteField("PhotoUrl");
csv.WriteField("PhotoDescription");
csv.WriteField("ReviewName");
csv.WriteField("ReviewText");
csv.WriteField("RatingScore");
csv.WriteField("ReservationStartDate");
csv.WriteField("ReservationEndDate");
csv.WriteField("ReservationStatus");
csv.NextRecord();

for (var i = 1; i <= rowCount; i++)
{
    var ownerId = random.Next(1, 250);
    var apartmentId = random.Next(1, 450);
    var reviewId = random.Next(1, 2000);

    var startDate = DateTime.UtcNow.Date.AddDays(random.Next(-120, 120));
    var endDate = startDate.AddDays(random.Next(1, 14));

    csv.WriteField($"Owner {ownerId}");
    csv.WriteField($"owner{ownerId}@mail.test");
    csv.WriteField($"{random.Next(1, 300)} Main Street, Apt {apartmentId}");
    csv.WriteField(random.Next(30000, 450000));
    csv.WriteField(random.Next(1, 6));
    csv.WriteField($"https://img.example.com/apartments/{apartmentId}/photo-{random.Next(1, 6)}.jpg");
    csv.WriteField($"Living room view #{random.Next(1, 20)}");
    csv.WriteField($"Reviewer {reviewId}");
    csv.WriteField($"Review text sample #{reviewId}");
    csv.WriteField(random.Next(1, 6));
    csv.WriteField(startDate.ToString("yyyy-MM-dd"));
    csv.WriteField(endDate.ToString("yyyy-MM-dd"));
    csv.WriteField(statuses[random.Next(0, statuses.Length)]);
    csv.NextRecord();
}

Console.WriteLine($"CSV generated at '{outputPath}' with {rowCount} rows.");

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
