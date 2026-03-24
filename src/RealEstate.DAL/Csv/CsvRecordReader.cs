using CsvHelper;
using System.Globalization;
using RealEstate.BLL.Contracts;
using RealEstate.BLL.Interfaces;

namespace RealEstate.DAL.Csv;

public sealed class CsvRecordReader : ICsvRecordReader
{
    public async IAsyncEnumerable<ImportCsvRecord> ReadAsync(string csvPath, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        if (!File.Exists(csvPath))
        {
            throw new FileNotFoundException($"CSV file not found: {csvPath}", csvPath);
        }

        using var streamReader = new StreamReader(csvPath);
        using var csv = new CsvReader(streamReader, CultureInfo.InvariantCulture);
        csv.Context.RegisterClassMap<CsvImportRecordMap>();

        await foreach (var row in csv.GetRecordsAsync<ImportCsvRecord>(cancellationToken))
        {
            yield return row;
        }
    }
}
