using Lab4.Data.ApplicationDbContext;
using Lab4.Data.Models;

using Microsoft.EntityFrameworkCore;

namespace Lab4.Services;

public class UsersService : IUsersService
{
	private readonly ApplicationContext _applicationContext;

	public UsersService(ApplicationContext applicationContext) 
		=> _applicationContext = applicationContext;

	public async Task<IEnumerable<User>?> GetAllAsync()
		=> await _applicationContext.Users.ToListAsync();

	public async Task<User?> GetAsync(int id)
		=> await _applicationContext.Users
		.FirstOrDefaultAsync(u => u.Id == id);
}
