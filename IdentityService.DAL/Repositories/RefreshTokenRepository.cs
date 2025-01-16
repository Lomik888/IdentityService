using Dapper;
using IdentityService.Domain.Entities;
using IdentityService.Domain.Interfaces.Repositories.RefreshTokenRepository;

namespace IdentityService.DAL.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository<RefreshToken>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DapperDbContext _dapperDbContext;

    public RefreshTokenRepository(ApplicationDbContext dbContext, DapperDbContext dapperDbContext)
    {
        _dbContext = dbContext;
        _dapperDbContext = dapperDbContext;
    }

    public IQueryable<RefreshToken> GetAll()
    {
        return _dbContext.Set<RefreshToken>();
    }

    public async Task AddByEntityAsync(RefreshToken entity)
    {
        await _dbContext.AddAsync(entity);
    }

    public async Task DapperUpdateRefreshTokenActive(long id, bool isActive)
    {
        var parameters = new
        {
            Id = id,
            IsActive = isActive
        };

        var query = "UPDATE public.\"RefreshTokens\" SET \"IsActive\" = @IsActive WHERE \"Id\" = @Id";

        using (var connection = _dapperDbContext.CreateConnection())
        {
            await connection.QueryAsync(query, parameters);
        }
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}