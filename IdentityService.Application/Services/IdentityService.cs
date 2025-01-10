using AutoMapper;
using IdentityService.Application.Resources;
using IdentityService.Domain.Dto.UserDto;
using IdentityService.Domain.Entities;
using IdentityService.Domain.Interfaces.Extantions;
using IdentityService.Domain.Interfaces.Repositories;
using IdentityService.Domain.Interfaces.Services;
using IdentityService.Domain.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Application.Services;

public class IdentityService : IIdentityService
{
    private readonly IRegistrationRepository<User> _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtGenerator _jwtGenerator;

    public IdentityService(IRegistrationRepository<User> userRepository, IMapper mapper, IPasswordHasher passwordHasher,
        IJwtGenerator jwtGenerator)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
        _jwtGenerator = jwtGenerator;
    }

    public async Task<BaseResult> RegistrationUserAsync(RegistrationDto registrationDto)
    {
        var emailResult = await _userRepository.GetAll().AnyAsync(x => x.Email == registrationDto.Email);

        if (emailResult)
        {
            return new BaseResult()
            {
                ErrorMessage = ErrorMessage.EmailAlreadyExist,
                StatusCode = StatusCodes.Status400BadRequest
            };
        }

        var user = new User()
        {
            FirstName = registrationDto.FirstName,
            LastName = registrationDto.LastName,
            Email = registrationDto.Email,
            Password = new Password()
            {
                PasswordHash = await _passwordHasher.HashPasswordAsync(registrationDto.Password)
            },
        };

        await _userRepository.AddByEntityAsync(user);
        await _userRepository.SaveChangesAsync();

        return new BaseResult();
    }

    public async Task<DataBaseResult<LoginResult>> LoginUserAsync(LoginDto loginDto, string accessToken)
    {
        var user = await _userRepository.GetAll()
            .Where(x => x.Email == loginDto.Email)
            .Include(x => x.Password)
            .Select(x => new { x.Id, x.Password.PasswordHash })
            .SingleAsync();

        var refreshToken = _jwtGenerator.GetRefreshTokenAsync();
        
        
        
        if (await _passwordHasher.VerifyPasswordAsync(loginDto.Password, user.PasswordHash))
        {
            return new DataBaseResult<LoginResult>()
            {
                Data = new LoginResult(_jwtGenerator.GetAccessTokenAsync(user.Id.ToString()), refreshToken
                    )
            };
        }
        
        return new DataBaseResult<LoginResult>()
        {
            ErrorMessage = ErrorMessage.PasswordUnavailable,
            StatusCode = StatusCodes.Status400BadRequest
        };
    }
}