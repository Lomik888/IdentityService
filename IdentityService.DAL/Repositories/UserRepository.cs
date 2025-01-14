using System.Text;
using Dapper;
using IdentityService.DAL.Extensions;
using IdentityService.Domain.Entities;
using IdentityService.Domain.Interfaces.Repositories;

namespace IdentityService.DAL.Repositories;

public class UserRepository : IUserRepository<User>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DapperDbContext _dapperDbContext;

    public UserRepository(ApplicationDbContext dbContext, DapperDbContext dapperDbContext)
    {
        _dbContext = dbContext;
        _dapperDbContext = dapperDbContext;
    }

    public IQueryable<User> GetAll()
    {
        return _dbContext.Set<User>();
    }

    public async Task AddByEntityAsync(User entity)
    {
        await _dbContext.AddAsync(entity);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task RemoveUserByIdAsync(long userId)
    {
        var parameter = new { Id = userId };

        var query = "DELETE FROM public.\"Users\" WHERE \"Id\" = @Id";

        using (var connection = _dapperDbContext.CreateConnection())
        {
            await connection.QueryAsync(query, parameter);
        }
    }

    public async Task UpdateByEntityAsync(User user)
    {
        var queryBuilder = new StringBuilder("UPDATE public.\"Users\" SET ");
        var parameters = new DynamicParameters();
        var listPropertyNames = parameters.GetDynamicParameters(user);
        listPropertyNames.Remove("Id");

        foreach (var l in listPropertyNames)
        {
            queryBuilder.Append($"\"{l}\" = @{l}, ");
        }

        queryBuilder.Length -= 2;
        queryBuilder.Append($" WHERE \"Id\" = @Id");

        using (var connection = _dapperDbContext.CreateConnection())
        {
            await connection.QueryAsync(queryBuilder.ToString(), parameters);
        }
    }
}