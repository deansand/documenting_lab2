using RealEstate.Domain.Entities;

namespace RealEstate.BLL.Interfaces;

public interface IOwnerRepository
{
    Task<Owner?> FindByNameAndContactAsync(string name, string contactInfo, CancellationToken cancellationToken = default);
    Task AddAsync(Owner owner, CancellationToken cancellationToken = default);
}
