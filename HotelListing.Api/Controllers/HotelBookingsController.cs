using HotelListing.Api.Contracts;
using HotelListing.Api.DTOs.Booking;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.Api.Controllers;

[Route("api/hotels/{hotelId:int}[controller]")]
[ApiController]
[Authorize]
public class HotelBookingsController(IBookingService bookingService) : BaseApiController
{
    [HttpGet("/admin")]
    [Authorize(Roles = "Hotel Admin, Administrator")]

    public async Task<ActionResult<IEnumerable<GetBookingDto>>> GetBookingsAdmin([FromRoute]int hotelId)
    {
        var result = await bookingService.GetBookingsForHotelAsync(hotelId);
        return ToActionResult(result);
    }

    [HttpGet]

    public async Task<ActionResult<IEnumerable<GetBookingDto>>> GetBookings([FromRoute] int hotelId)
    {
        var result = await bookingService.GetUserBookingsForHotelAsync(hotelId);
        return ToActionResult(result);
    }


    [HttpPost]
    public async Task<ActionResult<GetBookingDto>> CreateBooking(
    [FromRoute] int hotelId,
    [FromBody] CreateBookingDto createBookingDto)
    {
        var result = await bookingService.CreateBookingAsync(createBookingDto);
        return ToActionResult(result);
    }

    [HttpPut("{bookingId:int}")]
    public async Task<ActionResult<GetBookingDto>> UpdateBooking(
    [FromRoute] int hotelId,
    [FromRoute] int bookingId,
    [FromBody] UpdateBookingDto updateBookingDto)
    {
        var result = await bookingService.UpdateBookingAsync(hotelId, bookingId, updateBookingDto);
        return ToActionResult(result);
    }

    [HttpPut("{bookingId:int}/cancel")]
    public async Task<IActionResult> CancelBooking([FromRoute] int hotelId, [FromRoute] int bookingId)
    {
        var result = await bookingService.CancelBookingAsync(hotelId, bookingId);
        return ToActionResult(result);
    }

    [HttpPut("{bookingId:int}/admin/cancel")]
    [Authorize(Roles = "Hotel Admin, Administrator")]
    //[HotelOrSystemAdmin]
    public async Task<IActionResult> AdminCancelBooking([FromRoute] int hotelId, [FromRoute] int bookingId)
    {
        var result = await bookingService.AdminCancelBookingAsync(hotelId, bookingId);
        return ToActionResult(result);
    }

    [HttpPut("{bookingId:int}/admin/confirm")]
    [Authorize(Roles = "Hotel Admin, Administrator")]

    //[HotelOrSystemAdmin]
    public async Task<IActionResult> AdminConfirmBooking([FromRoute] int hotelId, [FromRoute] int bookingId)
    {
        var result = await bookingService.AdminConfirmBookingAsync(hotelId, bookingId);
        return ToActionResult(result);
    }
}
