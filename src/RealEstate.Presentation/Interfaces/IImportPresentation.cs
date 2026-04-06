using RealEstate.BLL.Contracts;

namespace RealEstate.Presentation.Interfaces;

public interface IImportPresentation
{
    Task ShowStartedAsync(string filePath, CancellationToken cancellationToken = default);
    Task ShowCompletedAsync(ImportResult result, CancellationToken cancellationToken = default);
    Task ShowErrorAsync(string message, CancellationToken cancellationToken = default);
}
