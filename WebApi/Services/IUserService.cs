using Shared.Models;

namespace WebApi.Services;

public interface IUserService
{
    Task<User> GetUser(string username, string password);
    Task RegisterUser(User user);
}