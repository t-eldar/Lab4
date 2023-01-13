using Lab4.Data.Models;

namespace Lab4.Services;

public interface IAuthenticationService
{
	Task SignInAsync(string username, string password);
	Task SignUpAsync(string username, string password);
	Task SignOutAsync();
}
