using AutoMapper;
using IdentityService.Application.Resources;
using IdentityService.Domain.Dto.UserDto;
using IdentityService.Domain.Entities;
using IdentityService.Domain.Interfaces.Extensions;
using IdentityService.Domain.Interfaces.Repositories;
using IdentityService.Domain.Interfaces.Services;
using IdentityService.Domain.Result;
using IdentityService.Domain.Result.UserResult;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Application.Services;

public class IdentityService : IIdentityService
{
    #region DI and ctor

    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository<RefreshToken> _refreshTokenRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IMapper _mapper;

    public IdentityService(IUserRepository userRepository, IPasswordHasher passwordHasher,
        IJwtGenerator jwtGenerator, IMapper mapper, IRefreshTokenRepository<RefreshToken> refreshTokenRepository)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtGenerator = jwtGenerator;
        _mapper = mapper;
        _refreshTokenRepository = refreshTokenRepository;
    }

    #endregion

    public async Task<BaseResult> RegistrationUserByRegistrationDtoAsync(UserRegistrationDto userRegistrationDto)
    {
        var emailExist = await _userRepository.GetAll()
            .AnyAsync(x => x.Email == userRegistrationDto.Email);

        if (emailExist)
        {
            return new BaseResult()
            {
                ErrorMessage = ErrorMessage.EmailAlreadyExist,
                StatusCode = StatusCodes.Status400BadRequest
            };
        }

        var newUser = _mapper.Map<User>(userRegistrationDto);
        newUser.Password.PasswordHash = await _passwordHasher.HashPasswordAsync(userRegistrationDto.Password);

        await _userRepository.AddByEntityAsync(newUser);
        await _userRepository.SaveChangesAsync();

        return new BaseResult()
        {
            StatusCode = StatusCodes.Status201Created
        };
    }


    public async Task<DataBaseResult<LoginResult>> LoginUserAsync(string email, string password)
    {
        var user = await _userRepository.GetAll()
            .AsNoTracking()
            .Where(x => x.Email == email)
            .Include(x => x.Password)
            .Select(x => new { x.Id, x.Password.PasswordHash })
            .SingleOrDefaultAsync();

        if (user == null)
        {
            return new DataBaseResult<LoginResult>()
            {
                ErrorMessage = ErrorMessage.EmailNotExist,
                StatusCode = StatusCodes.Status400BadRequest
            };
        }

        if (!await _passwordHasher.VerifyPasswordAsync(password, user.PasswordHash))
        {
            return new DataBaseResult<LoginResult>()
            {
                ErrorMessage = ErrorMessage.InvalidPassword,
                StatusCode = StatusCodes.Status400BadRequest
            };
        }

        var refreshToken = _jwtGenerator.GetRefreshToken();
        refreshToken.UserId = user.Id;

        await _refreshTokenRepository.AddByEntityAsync(refreshToken);
        await _refreshTokenRepository.SaveChangesAsync();

        return new DataBaseResult<LoginResult>()
        {
            Data = new LoginResult()
            {
                AccessToken = _jwtGenerator.GetAccessToken(user.Id),
                RefreshToken = refreshToken.Token,
                StatusCode = StatusCodes.Status200OK
            }
        };
    }
}