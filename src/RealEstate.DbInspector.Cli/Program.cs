using Microsoft.EntityFrameworkCore;
using RealEstate.DAL.Persistence;

var argsMap = ParseArgs(args);
var connectionString = argsMap.TryGetValue("--db", out var db)
    ? db
    : "Data Source=realestate.db";

var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
optionsBuilder.UseSqlite(connectionString);

using var dbContext = new AppDbContext(optionsBuilder.Options);

Console.WriteLine($"\n Database Inspector: {connectionString}\n");
Console.WriteLine("─────────────────────────────────────────────────────────\n");

var ownerCount = await dbContext.Owners.CountAsync();
var apartmentCount = await dbContext.Apartments.CountAsync();
var photoCount = await dbContext.Photos.CountAsync();
var reviewCount = await dbContext.Reviews.CountAsync();
var ratingCount = await dbContext.Ratings.CountAsync();
var reservationCount = await dbContext.Reservations.CountAsync();

Console.WriteLine("Table Row Counts:");
Console.WriteLine($"  Owners:       {ownerCount,6}");
Console.WriteLine($"  Apartments:   {apartmentCount,6}");
Console.WriteLine($"  Photos:       {photoCount,6}");
Console.WriteLine($"  Reviews:      {reviewCount,6}");
Console.WriteLine($"  Ratings:      {ratingCount,6}");
Console.WriteLine($"  Reservations: {reservationCount,6}");

Console.WriteLine("\n─────────────────────────────────────────────────────────\n");
Console.WriteLine("Sample Data:\n");

if (ownerCount > 0)
{
    var allOwners = await dbContext.Owners.ToListAsync();
    Console.WriteLine($" All Owners ({allOwners.Count}):");
    foreach (var owner in allOwners)
    {
        Console.WriteLine($"   [{owner.Id}] {owner.Name} | {owner.ContactInfo}");
    }
}

if (apartmentCount > 0)
{
    var allApartments = await dbContext.Apartments
        .Include(x => x.Owner)
        .ToListAsync();
    Console.WriteLine($"\n All Apartments ({allApartments.Count}):");
    foreach (var apt in allApartments)
    {
        Console.WriteLine($"   [{apt.Id}] {apt.Address} | ${apt.Price:N0} | {apt.RoomCount} rooms | Owner: {apt.Owner?.Name}");
    }
}

if (photoCount > 0)
{
    var allPhotos = await dbContext.Photos.ToListAsync();
    Console.WriteLine($"\n All Photos ({allPhotos.Count}):");
    foreach (var photo in allPhotos)
    {
        Console.WriteLine($"   [{photo.Id}] Apt {photo.ApartmentId}: {photo.Description}");
        Console.WriteLine($"        URL: {photo.Url}");
    }
}

if (reviewCount > 0)
{
    var allReviews = await dbContext.Reviews.ToListAsync();
    Console.WriteLine($"\n All Reviews ({allReviews.Count}):");
    foreach (var review in allReviews)
    {
        Console.WriteLine($"   [{review.Id}] Apt {review.ApartmentId} - {review.Name}");
        Console.WriteLine($"        \"{review.Text}\"");
    }
}

if (ratingCount > 0)
{
    var allRatings = await dbContext.Ratings.ToListAsync();
    Console.WriteLine($"\n All Ratings ({allRatings.Count}):");
    foreach (var rating in allRatings)
    {
        Console.WriteLine($"   [{rating.Id}] Apt {rating.ApartmentId}: {rating.Score}/5");
    }
}

if (reservationCount > 0)
{
    var allReservations = await dbContext.Reservations.ToListAsync();
    Console.WriteLine($"\n All Reservations ({allReservations.Count}):");
    foreach (var res in allReservations)
    {
        Console.WriteLine($"   [{res.Id}] Apt {res.ApartmentId}: {res.StartDate:yyyy-MM-dd} → {res.EndDate:yyyy-MM-dd} | {res.Status}");
    }
}

Console.WriteLine("\n─────────────────────────────────────────────────────────");
Console.WriteLine(" Database inspection complete.\n");

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
