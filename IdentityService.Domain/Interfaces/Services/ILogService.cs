namespace IdentityService.Domain.Interfaces.Services;

public interface ILogService
{
    Task AddLogAsync(long? userId, string methodName, DateTime dateTime, string exceptionMessage = null);
}