using HotelListing.Api.DTOs.Auth;

namespace HotelListing.Api.Contracts
{
    public interface IUsersService
    {
        Task<Results.Results.Result<string>> LoginAsync(LoginUserDto dto);
        Task<Results.Results.Result<RegisteredUserDto>> RegisterAsync(RegisterUserDto registerUserDto);
    }
}