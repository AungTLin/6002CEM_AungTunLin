using Fungry.API.Data.Entities;
using Fungry.API.Data;
using Fungry.API.Services;
using fungry.lib.Dtos;
using Microsoft.EntityFrameworkCore;

public class AuthService(DataContext context, TokenService tokenService, PasswordService passwordService)

{

    private readonly DataContext _context = context;

    private readonly TokenService _tokenService = tokenService;

    private readonly PasswordService _passwordService = passwordService;

    public async Task<ResultWithDataDto<AuthResponseDto>> SignupAsync(SignupRequestDto dto)
    {

        if (await _context.Users.AsNoTracking().AnyAsync(u => u.Email == dto.Email))
        {

            return ResultWithDataDto<AuthResponseDto>.Failure("Sorry, this Email exists");

        }

        var user = new User { Email = dto.Email, Name = dto.Name, Address = dto.Address, };

        (user.Salt, user.Hash) = _passwordService.GenerateSaltAndHash(dto.Password);

        try

        {

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return GenerateAuthenticationResponse(user);

        }

        catch (Exception ex)
        {

            return ResultWithDataDto<AuthResponseDto>.Failure(ex.Message);

        }

    }


    public async Task<ResultWithDataDto<AuthResponseDto>> SigninAsync(SigninRequestDto dto)

    {

        var dbUser = await _context.Users
            .AsNoTracking()

                .FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (dbUser is null)

            return ResultWithDataDto<AuthResponseDto>.Failure("Sorry,This user does not exist");

        if (!_passwordService.AreEqual(dto.Password, dbUser.Salt, dbUser.Hash))

            return ResultWithDataDto<AuthResponseDto>.Failure("Incorrect password");

        return GenerateAuthenticationResponse(dbUser);


    }

    private ResultWithDataDto<AuthResponseDto> GenerateAuthenticationResponse(User user)

    {

        var loggedInUser = new LoggedInUser(user.Id, user.Name, user.Email, user.Address);

        var token = _tokenService.GenerateJWT(loggedInUser);

        var authResponse = new AuthResponseDto(loggedInUser, token);

        return ResultWithDataDto<AuthResponseDto>.Success(authResponse);

    }

}