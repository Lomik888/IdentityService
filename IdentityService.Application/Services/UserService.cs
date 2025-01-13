using AutoMapper;
using IdentityService.Application.Resources;
using IdentityService.Domain.Dto.UserDto;
using IdentityService.Domain.Interfaces.Extensions;
using IdentityService.Domain.Interfaces.Repositories;
using IdentityService.Domain.Interfaces.Services;
using IdentityService.Domain.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Application.Services;

public class UserService : IUserService
{
    #region DI ctor

    private readonly IUserRepository _userRepository;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper, IJwtGenerator jwtGenerator)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _jwtGenerator = jwtGenerator;
    }

    #endregion

    public async Task<CollectionBaseResult<List<UserDto>>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAll()
            .AsNoTracking()
            .Select(x => new UserDto(x.FirstName, x.LastName))
            .ToListAsync();

        return new CollectionBaseResult<List<UserDto>>()
        {
            Data = users,
            Count = users.Count,
            StatusCode = StatusCodes.Status200OK
        };
    }

    public async Task<BaseResult> RemoveUserByUserIdAsync(string accessToken)
    {
        var userId = _jwtGenerator.GetIdFromAccessToken(accessToken);

        var userExist = await _userRepository.GetAll()
            .AnyAsync(x => x.Id.ToString() == userId);

        if (!userExist)
        {
            return new BaseResult()
            {
                ErrorMessage = ErrorMessage.UserNotExist,
                StatusCode = StatusCodes.Status400BadRequest
            };
        }

        _userRepository.RemoveUserById(Convert.ToInt64(userId));
        await _userRepository.SaveChangesAsync();

        return new BaseResult()
        {
            StatusCode = StatusCodes.Status200OK
        };
    }

    public async Task<BaseResult> ModifiedUserAsync(UserModifiedDto userModifiedDto, string accessToken)
    {
        var userId = _jwtGenerator.GetIdFromAccessToken(accessToken);
        
        var user = await _userRepository.GetAll().SingleAsync(x => x.Id.ToString() == userId);

        _mapper.Map(userModifiedDto, user);

        _userRepository.UpdateByEntityAttach(user);
        await _userRepository.SaveChangesAsync();

        return new BaseResult()
        {
            StatusCode = StatusCodes.Status200OK
        };
    }
}