using AutoMapper;
using IdentityService.Application.Resources;
using IdentityService.Domain.Dto.UserDto;
using IdentityService.Domain.Entities;
using IdentityService.Domain.Interfaces.Repositories;
using IdentityService.Domain.Interfaces.Services;
using IdentityService.Domain.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

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

    public async Task<BaseResult> RemoveUserByUserIdAsync(long userId)
    {
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

        _userRepository.RemoveUserById(userId);
        await _userRepository.SaveChangesAsync();

        return new BaseResult()
        {
            StatusCode = StatusCodes.Status200OK
        };
    }

    public async Task<BaseResult> ModifiedUserAsync(UserModifiedDto userModifiedDto, long userId)
    {
        var userExist = await _userRepository.GetAll().AnyAsync(x => x.Id == userId);

        if (!userExist)
        {
            return new BaseResult()
            {
                ErrorMessage = ErrorMessage.UserNotExist,
                StatusCode = StatusCodes.Status400BadRequest
            };
        }

        var user = _mapper.Map<User>(userModifiedDto);

        _userRepository.UpdateByEntityAttach(user);
        await _userRepository.SaveChangesAsync();

        return new BaseResult()
        {
            StatusCode = StatusCodes.Status200OK
        };
    }
}