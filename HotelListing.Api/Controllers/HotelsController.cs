using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.Api.Data;
using HotelListing.Api.DTOs.Hotel;
using System.Linq;
using HotelListing.Api.Services;
using HotelListing.Api.Contacts;

namespace HotelListing.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HotelsController(IHotelsService hotelsService) : ControllerBase
{

    // GET: api/Hotels
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetHotelDto>>> GetHotels()
    {
        // SELECT * FROM Hotels LEFT JOIN Countries ON Hotels.CountryId = Countries.CountryId
        

       var hotels = await hotelsService.GetHotelsAsync();

        return Ok(hotels);
    }

    // GET: api/Hotels/5
    [HttpGet("{id}")]
    public async Task<ActionResult<GetHotelDto>> GetHotel(int id)
    {
        var hotel = await hotelsService.GetHotelAsync(id);
        if (hotel == null)
            return NotFound();

        return hotel;
    }

    // POST: api/Hotels
    [HttpPost]
    public async Task<ActionResult<Hotel>> CreateHotel(CreateHotelDto hotelDto)
    {
       var hotel =  await hotelsService.CreateHotelAsync(hotelDto);

        return CreatedAtAction(nameof(GetHotel), new { id = hotel.Id }, hotel);
    }

    // PUT: api/Hotels/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHotel(int id, UpdateHotelDto hotelDto)
    {
        if (id != hotelDto.Id)
            return BadRequest();

        
        try
        {
            await hotelsService.UpdateHotelAsync(id,hotelDto);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await hotelsService.HotelExistsAsync(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();

        
    }

    // DELETE: api/Hotels/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHotel(int id)
    {
        await hotelsService.DeleteHotelAsync(id);
        return NoContent();
    }
}
