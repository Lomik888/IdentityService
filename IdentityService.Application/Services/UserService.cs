using AutoMapper;
using IdentityService.Application.Resources;
using IdentityService.Domain.Dto.UserDto;
using IdentityService.Domain.Entities;
using IdentityService.Domain.Interfaces.Extensions;
using IdentityService.Domain.Interfaces.Repositories.UserRepository;
using IdentityService.Domain.Interfaces.Services;
using IdentityService.Domain.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Application.Services;

public class UserService : IUserService
{
    #region DI ctor

    private readonly IUserRepository<User> _userRepository;
    private readonly IUserRedisRepository _userRedisRepository;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IMapper _mapper;

    public UserService(IUserRepository<User> userRepository, IMapper mapper, IJwtGenerator jwtGenerator,
        IUserRedisRepository userRedisRepository)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _jwtGenerator = jwtGenerator;
        _userRedisRepository = userRedisRepository;
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
        var userId = Convert.ToInt64(_jwtGenerator.GetIdFromAccessToken(accessToken));

        var userExist = await _userRepository.GetAll()
            .AnyAsync(x => x.Id == userId);

        if (!userExist)
        {
            return new BaseResult()
            {
                ErrorMessage = ErrorMessage.UserNotExist,
                StatusCode = StatusCodes.Status400BadRequest
            };
        }

        await _userRepository.DapperRemoveUserByIdAsync(userId);

        return new BaseResult()
        {
            StatusCode = StatusCodes.Status200OK
        };
    }

    public async Task<BaseResult> ModifiedUserAsync(UserModifiedDto userModifiedDto, string accessToken)
    {
        var userId = Convert.ToInt64(_jwtGenerator.GetIdFromAccessToken(accessToken));

        var userExist = await _userRepository.GetAll()
            .AnyAsync(x => x.Id == userId);

        if (!userExist)
        {
            return new BaseResult()
            {
                ErrorMessage = ErrorMessage.UserNotExist,
                StatusCode = StatusCodes.Status400BadRequest
            };
        }

        var user = _mapper.Map<User>(userModifiedDto);
        user.Id = userId;

        // await _userRepository.DapperUpdateByEntityAsync(user);
        await _userRepository.EFCoreUpdateByEntityAsync(user);
        return new BaseResult()
        {
            StatusCode = StatusCodes.Status200OK
        };
    }
}