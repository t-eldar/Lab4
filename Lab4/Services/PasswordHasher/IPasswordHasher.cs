namespace Lab4.Services;

public interface IPasswordHasher
{
	string HashPassword(string password);
	bool VerifyPassword(string input, string hashed);
}

