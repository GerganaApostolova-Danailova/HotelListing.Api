using HotelListing.Api.DTOs.Hotel;

namespace HotelListing.Api.Contacts;

public interface IHotelsService
{
    Task<bool> HotelExistsAsync(int id);
    Task<bool> HotelExistsAsync(string name);
    Task<GetHotelDto> CreateHotelAsync(CreateHotelDto createDto);
    Task DeleteHotelAsync(int id);
    Task<IEnumerable<GetHotelDto>> GetHotelsAsync();
    Task<GetHotelDto?> GetHotelAsync(int id);
    Task UpdateHotelAsync(int id, UpdateHotelDto updateDto);
}