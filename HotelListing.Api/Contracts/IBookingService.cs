using HotelListing.Api.DTOs.Booking;

namespace HotelListing.Api.Contracts
{
    public interface IBookingService
    {
        Task<Results.Results.Result> AdminConfirmBookingAsync(int hotelId, int bookingId);
        Task<Results.Results.Result> CancelBookingAsync(int hotelId, int bookingId);
        Task<Results.Results.Result<GetBookingDto>> CreateBookingAsync(CreateBookingDto dto);
        Task<Results.Results.Result<IEnumerable<GetBookingDto>>> GetBookingsForHotelAsync(int hotelId);
        Task<Results.Results.Result<GetBookingDto>> UpdateBookingAsync(int hotelId, int bookingId, UpdateBookingDto updateBookingDto);
        Task<Results.Results.Result> AdminCancelBookingAsync(int hotelId, int bookingId);
    }
}