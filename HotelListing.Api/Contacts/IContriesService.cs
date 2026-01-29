using HotelListing.Api.DTOs.Country;

namespace HotelListing.Api.Contacts;

public interface IContriesService
{
    Task<bool> CountryExistsAsync(int id);
    Task<bool> CountryExistsAsync(string name);
    Task<GetCountryDto> CreateCountryAsync(CreateCountryDto createDto);
    Task DeleteCountryAsync(int id);
    Task<IEnumerable<GetCountriesDto>> GetCountriesAsync();
    Task<GetCountryDto?> GetCountryAsync(int id);
    Task UpdateCountryAsync(int id, UpdateCountryDto updateDto);
}
