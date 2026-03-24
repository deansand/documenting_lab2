using RealEstate.BLL.Contracts;

namespace RealEstate.BLL.Interfaces;

public interface IDataImportService
{
    Task<ImportResult> ImportFromCsvAsync(string csvPath, CancellationToken cancellationToken = default);
}
