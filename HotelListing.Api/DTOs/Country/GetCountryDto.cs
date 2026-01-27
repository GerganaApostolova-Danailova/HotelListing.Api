namespace HotelListing.Api.DTOs.Country
{
    public record GetCountryDto(
        int Id,
        string Name,
        string ShortName
    );

    public record GetCountriesDto(
        int Id,
        string Name,
        string ShortName
    );
}
