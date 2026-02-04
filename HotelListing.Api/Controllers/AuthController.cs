using HotelListing.Api.Contracts;
using HotelListing.Api.Data;
using HotelListing.Api.DTOs.Auth;
using HotelListing.Api.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static HotelListing.Api.Results.Results;

namespace HotelListing.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class AuthController(IUsersService usersService) : BaseApiController
{
    [HttpPost("register")]
    public async Task<ActionResult<RegisteredUserDto>> Register(RegisterUserDto registerUserDto)
    {
        var result = await usersService.RegisterAsync(registerUserDto);

        return ToActionResult(result);

        //var user = new ApplicationUser
        //{
        //    UserName = registerUserDto.Email,
        //    Email = registerUserDto.Email,
        //    FirstName = registerUserDto.FirstName,
        //    LastName = registerUserDto.LastName
        //};

        //var result = await userManager.CreateAsync(user, registerUserDto.Password);

        //if (!result.Succeeded)
        //{
        //    var errors = result.Errors.Select(e => new Error(ErrorCodes.BadRequest, e.Description)).ToArray();

        //    return MapErrorsToResponse(errors);
        //}
        //return Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(LoginUserDto loginUserDto)
    {
        var result = await usersService.LoginAsync(loginUserDto);

        return ToActionResult(result);

        //var user = await userManager.FindByEmailAsync(loginUserDto.Email);

        //if (user == null)
        //{
        //    return Unauthorized(new { message = "Invalid Credentials"});
        //}

        //var isPasswordValid = await userManager.CheckPasswordAsync(user, loginUserDto.Password);

        //if (!isPasswordValid)
        //{
        //    return Unauthorized(new { message = "Invalid Credentials"});
        //}

        //return Ok();
    }
}
