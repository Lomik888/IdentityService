using Dapper;
using Npgsql;

namespace IdentityService.DAL.Extensions;

public static class DynamicParametersExtensions
{
    public static List<string> GetDynamicParameters<T>(this DynamicParameters parameters, T entity)
    {
        var listPropertyNames = new List<string>();
        var type = typeof(T);
        var properties = type.GetProperties();

        foreach (var p in properties)
        {
            var value = p.GetValue(entity);
            if (value != null)
            {
                listPropertyNames.Add(p.Name);
                parameters.Add(p.Name, value);
            }
        }

        return listPropertyNames;
    }

    public static List<string> GetDynamicParameters<T>(this List<NpgsqlParameter> parameters, T entity)
    {
        var listPropertyNames = new List<string>();
        var type = typeof(T);
        var properties = type.GetProperties();

        foreach (var p in properties)
        {
            var value = p.GetValue(entity);
            if (value != null)
            {
                listPropertyNames.Add(p.Name);
                parameters.Add(new NpgsqlParameter(p.Name, value));
            }
        }

        return listPropertyNames;
    }
}