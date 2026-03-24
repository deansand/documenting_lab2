using RealEstate.BLL.Contracts;
using RealEstate.BLL.Interfaces;
using RealEstate.Domain.Entities;

namespace RealEstate.BLL.Services;

public sealed class DataImportService : IDataImportService
{
    private readonly ICsvRecordReader _csvRecordReader;
    private readonly IOwnerRepository _ownerRepository;
    private readonly IApartmentRepository _apartmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DataImportService(
        ICsvRecordReader csvRecordReader,
        IOwnerRepository ownerRepository,
        IApartmentRepository apartmentRepository,
        IUnitOfWork unitOfWork)
    {
        _csvRecordReader = csvRecordReader;
        _ownerRepository = ownerRepository;
        _apartmentRepository = apartmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ImportResult> ImportFromCsvAsync(string csvPath, CancellationToken cancellationToken = default)
    {
        var result = new ImportResult();

        await foreach (var row in _csvRecordReader.ReadAsync(csvPath, cancellationToken))
        {
            result.RowsRead++;

            var owner = await _ownerRepository.FindByNameAndContactAsync(
                row.OwnerName,
                row.OwnerContactInfo,
                cancellationToken);

            if (owner is null)
            {
                owner = new Owner
                {
                    Name = row.OwnerName,
                    ContactInfo = row.OwnerContactInfo
                };

                await _ownerRepository.AddAsync(owner, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                result.OwnersCreated++;
            }

            var apartment = await _apartmentRepository.FindByAddressAndOwnerAsync(
                row.ApartmentAddress,
                owner.Id,
                cancellationToken);

            if (apartment is null)
            {
                apartment = new Apartment
                {
                    Address = row.ApartmentAddress,
                    Price = row.ApartmentPrice,
                    RoomCount = row.ApartmentRoomCount,
                    OwnerId = owner.Id
                };

                await _apartmentRepository.AddAsync(apartment, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                result.ApartmentsCreated++;
            }

            if (!string.IsNullOrWhiteSpace(row.PhotoUrl))
            {
                var photoExists = await _apartmentRepository.PhotoExistsAsync(
                    apartment.Id,
                    row.PhotoUrl,
                    row.PhotoDescription,
                    cancellationToken);

                if (!photoExists)
                {
                    await _apartmentRepository.AddPhotoAsync(new Photo
                    {
                        ApartmentId = apartment.Id,
                        Url = row.PhotoUrl,
                        Description = row.PhotoDescription
                    }, cancellationToken);

                    result.PhotosCreated++;
                }
            }

            if (!string.IsNullOrWhiteSpace(row.ReviewText))
            {
                var reviewExists = await _apartmentRepository.ReviewExistsAsync(
                    apartment.Id,
                    row.ReviewName,
                    row.ReviewText,
                    cancellationToken);

                if (!reviewExists)
                {
                    await _apartmentRepository.AddReviewAsync(new Review
                    {
                        ApartmentId = apartment.Id,
                        Name = row.ReviewName,
                        Text = row.ReviewText
                    }, cancellationToken);

                    result.ReviewsCreated++;
                }
            }

            if (row.RatingScore is >= 1 and <= 5)
            {
                var ratingExists = await _apartmentRepository.RatingExistsAsync(
                    apartment.Id,
                    row.RatingScore.Value,
                    cancellationToken);

                if (!ratingExists)
                {
                    await _apartmentRepository.AddRatingAsync(new Rating
                    {
                        ApartmentId = apartment.Id,
                        Score = row.RatingScore.Value
                    }, cancellationToken);

                    result.RatingsCreated++;
                }
            }

            if (row.ReservationStartDate.HasValue && row.ReservationEndDate.HasValue && !string.IsNullOrWhiteSpace(row.ReservationStatus))
            {
                var reservationExists = await _apartmentRepository.ReservationExistsAsync(
                    apartment.Id,
                    row.ReservationStartDate.Value,
                    row.ReservationEndDate.Value,
                    row.ReservationStatus,
                    cancellationToken);

                if (!reservationExists)
                {
                    await _apartmentRepository.AddReservationAsync(new Reservation
                    {
                        ApartmentId = apartment.Id,
                        StartDate = row.ReservationStartDate.Value,
                        EndDate = row.ReservationEndDate.Value,
                        Status = row.ReservationStatus
                    }, cancellationToken);

                    result.ReservationsCreated++;
                }
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return result;
    }
}
