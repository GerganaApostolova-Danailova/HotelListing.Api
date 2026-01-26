using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.Api.Data;

namespace HotelListing.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountriesController(HotelListingDbContext context) : ControllerBase
{

    // GET: api/Countries
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
    {
        var contries = await context.Countries.ToListAsync();
        return contries;
    }

    // GET: api/Countries/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Country>> GetCountry(int id)
    {
        var country = await context.Countries.FindAsync(id);

        if (country == null)
            return NotFound();

        return country;
    }

    // POST: api/Countries
    [HttpPost]
    public async Task<ActionResult<Country>> PostCountry(Country country)
    {
        context.Countries.Add(country);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCountry), new { id = country.CountryId }, country);
    }

    // PUT: api/Countries/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCountry(int id, Country country)
    {
        if (id != country.CountryId)
            return BadRequest();

        context.Entry(country).State = EntityState.Modified;
        try
        {
            await context.SaveChangesAsync();

        }
        catch (DbUpdateConcurrencyException)
        {
            if (! await CountryExistsAsync(id))
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


    // DELETE: api/Countries/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCountry(int id)
    {
        var country = await context.Countries.FindAsync(id);

        if (country == null)
            return NotFound();

        context.Countries.Remove(country);
        await context.SaveChangesAsync();

        return NoContent();
    }
    private async Task<bool> CountryExistsAsync(int id)
    {
        return await context.Countries.AnyAsync(e => e.CountryId == id);
    }
}
