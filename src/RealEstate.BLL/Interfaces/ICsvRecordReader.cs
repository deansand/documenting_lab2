using RealEstate.BLL.Contracts;

namespace RealEstate.BLL.Interfaces;

public interface ICsvRecordReader
{
    IAsyncEnumerable<ImportCsvRecord> ReadAsync(string csvPath, CancellationToken cancellationToken = default);
}
