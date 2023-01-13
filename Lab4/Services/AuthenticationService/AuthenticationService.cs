using Lab4.Data.ApplicationDbContext;
using Lab4.Data.Models;
using Lab4.Exceptions;

using Blazored.LocalStorage;

using Microsoft.EntityFrameworkCore;

namespace Lab4.Services;

public sealed class AuthenticationService : IAuthenticationService
{
	private readonly ApplicationContext _applicationContext;
	private readonly IPasswordHasher _passwordHasher;
	private readonly ILocalStorageService _localStorageService;
	public AuthenticationService(
		ApplicationContext applicationContext, 
		IPasswordHasher passwordHasher,
		ILocalStorageService localStorageService)
	{
		_applicationContext = applicationContext;
		_passwordHasher = passwordHasher;
		_localStorageService = localStorageService;
	}

	public async Task SignInAsync(string username, string password)
	{
		await _localStorageService.RemoveItemAsync("user");
		var user = await _applicationContext.Users.FirstOrDefaultAsync(u => u.Username == username);
		if (user is null)
		{
			throw new ServiceException(ExceptionType.NotFound,
				$"User with username {username} does not exists");
		}
		if (!_passwordHasher.VerifyPassword(password, user.Password))
		{
			throw new ServiceException(ExceptionType.Unauthorized, "Password is incorrect");
		}
		await _localStorageService.SetItemAsync("user", user);
	}

	public async Task SignOutAsync()
	{
		await _localStorageService.RemoveItemAsync("user");
	}

	public async Task SignUpAsync(string username, string password)
	{
		await _localStorageService.RemoveItemAsync("user");
		var user = await _applicationContext.Users.FirstOrDefaultAsync(u => u.Username == username);
		if (user is not null)
		{
			throw new ServiceException(ExceptionType.AlreadyExists,
				 $"User with username {username} already exists");
		}
		await _applicationContext.Users.AddAsync(new User
		{
			Username = username,
			Password = _passwordHasher.HashPassword(password),
		});
		await _applicationContext.SaveChangesAsync();
		user = await _applicationContext.Users.FirstOrDefaultAsync(u => u.Username == username);
		if (user is null)
		{
			throw new ServiceException(ExceptionType.InternalError, "User has not been created");
		}
		await _localStorageService.SetItemAsync("user", user);
	}
}