using HotelListing.Api.DTOs.Auth;

namespace HotelListing.Api.Contracts
{
    public interface IUsersService
    {
        string UserId { get; }
        Task<Results.Results.Result<string>> LoginAsync(LoginUserDto dto);
        Task<Results.Results.Result<RegisteredUserDto>> RegisterAsync(RegisterUserDto registerUserDto);
    }
}