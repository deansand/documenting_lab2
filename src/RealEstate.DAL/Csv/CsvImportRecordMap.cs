using CsvHelper.Configuration;
using RealEstate.BLL.Contracts;

namespace RealEstate.DAL.Csv;

public sealed class CsvImportRecordMap : ClassMap<ImportCsvRecord>
{
    public CsvImportRecordMap()
    {
        Map(x => x.OwnerName).Name("OwnerName");
        Map(x => x.OwnerContactInfo).Name("OwnerContactInfo");
        Map(x => x.ApartmentAddress).Name("ApartmentAddress");
        Map(x => x.ApartmentPrice).Name("ApartmentPrice");
        Map(x => x.ApartmentRoomCount).Name("ApartmentRoomCount");
        Map(x => x.PhotoUrl).Name("PhotoUrl");
        Map(x => x.PhotoDescription).Name("PhotoDescription");
        Map(x => x.ReviewName).Name("ReviewName");
        Map(x => x.ReviewText).Name("ReviewText");
        Map(x => x.RatingScore).Name("RatingScore");
        Map(x => x.ReservationStartDate).Name("ReservationStartDate");
        Map(x => x.ReservationEndDate).Name("ReservationEndDate");
        Map(x => x.ReservationStatus).Name("ReservationStatus");
    }
}
