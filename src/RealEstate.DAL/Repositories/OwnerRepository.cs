using Microsoft.EntityFrameworkCore;
using RealEstate.BLL.Interfaces;
using RealEstate.DAL.Persistence;
using RealEstate.Domain.Entities;

namespace RealEstate.DAL.Repositories;

public sealed class OwnerRepository : IOwnerRepository
{
    private readonly AppDbContext _dbContext;

    public OwnerRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Owner?> FindByNameAndContactAsync(string name, string contactInfo, CancellationToken cancellationToken = default)
    {
        return _dbContext.Owners.FirstOrDefaultAsync(
            x => x.Name == name && x.ContactInfo == contactInfo,
            cancellationToken);
    }

    public Task AddAsync(Owner owner, CancellationToken cancellationToken = default)
    {
        return _dbContext.Owners.AddAsync(owner, cancellationToken).AsTask();
    }
}
