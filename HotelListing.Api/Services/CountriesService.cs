using HotelListing.Api.Contacts;
using HotelListing.Api.Data;
using HotelListing.Api.DTOs.Country;
using HotelListing.Api.DTOs.Hotel;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using static HotelListing.Api.Results.Results;

namespace HotelListing.Api.Services;

public class CountriesService(HotelListingDbContext context) : ICountriesService
{
    public async Task<Result<IEnumerable<GetCountriesDto>>> GetCountriesAsync()
    {
        var countries = await context.Countries
            .Select(c => new GetCountriesDto(c.CountryId, c.Name, c.ShortName))
            .ToListAsync();
        return Result<IEnumerable<GetCountriesDto>>.Success(countries);
    }

    public async Task<Result<GetCountryDto>> GetCountryAsync(int id)
    {
        var country = await context.Countries
            .Where(q => q.CountryId == id)
            .Select(c => new GetCountryDto(
                c.CountryId,
                c.Name,
                c.ShortName,
                c.Hotels!.Select(h => new GetHotelSlimDto(
                    h.Id,
                    h.Name,
                    h.Address,
                    h.Rating
                )).ToList()
            ))
            .FirstOrDefaultAsync();

        return country is null
            ? Result<GetCountryDto>.NotFound()
            : Result<GetCountryDto>.Success(country);
    }

    public async Task<Result<GetCountryDto>> CreateCountryAsync(CreateCountryDto createDto)
    {
        try
        {
            var exist = await CountryExistsAsync(createDto.Name);
            if (exist)
            {
                return Result<GetCountryDto>.Failure(new Error("Conflict", $"Country with name '{createDto.Name}' already exist"));
            }
            var country = new Country
            {
                Name = createDto.Name,
                ShortName = createDto.ShortName
            };
            context.Countries.Add(country);
            await context.SaveChangesAsync();

            var dto = new GetCountryDto(
                country.CountryId,
                country.Name,
                country.ShortName,
                []
            );

            return Result<GetCountryDto>.Success(dto);
        }
        catch (Exception)
        {
            return Result<GetCountryDto>.Failure();
        }
    }

    public async Task<Result> UpdateCountryAsync(int id, UpdateCountryDto updateDto)
    {
        try
        {
            if (id != updateDto.Id)
            {
                return Result.BadRequest(new Error("Validation", "Id route value does not match payload Id"));
            }
            var country = await context.Countries.FindAsync(id);

            if (country is null)
            {
                return Result.NotFound(new Error("NotFound", $"Country '{id}' was not found"));
            }

            country.Name = updateDto.Name;
            country.ShortName = updateDto.ShortName;
            context.Countries.Update(country);
            await context.SaveChangesAsync();

            return Result.Success();
        }
        catch (Exception)
        {

           return Result.Failure();
        }
    }

    public async Task<Result> DeleteCountryAsync(int id)
    {
        try
        {

        var country = await context.Countries.FindAsync(id);

        if (country is null)
        {
            return Result.NotFound(new Error("NotFound", $"Country '{id}' was not found"));
        }
        context.Countries.Remove(country);
        await context.SaveChangesAsync();
        return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure();
        }
    }

    public async Task<bool> CountryExistsAsync(int id)
    {
        return await context.Countries.AnyAsync(e => e.CountryId == id);
    }

    public async Task<bool> CountryExistsAsync(string name)
    {
        return await context.Countries.AnyAsync(e => e.Name == name);
    }
}

