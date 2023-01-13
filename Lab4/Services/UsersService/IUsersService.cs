using Lab4.Data.Models;

namespace Lab4.Services;

public interface IUsersService
{
	Task<IEnumerable<User>?> GetAllAsync();

	Task<User?> GetAsync(int id);
}
