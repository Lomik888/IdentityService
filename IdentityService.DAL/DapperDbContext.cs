using System.Data;
using DotNetEnv;
using Npgsql;

namespace IdentityService.DAL;

public class DapperDbContext
{
    private readonly string _connectionString;

    public DapperDbContext()
    {
        _connectionString = Env.GetString("DAPPER_CONNECTIONSTRING");
    }

    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}