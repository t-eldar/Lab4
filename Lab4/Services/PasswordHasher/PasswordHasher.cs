using System.Security.Cryptography;

namespace Lab4.Services;

public sealed class PasswordHasher : IPasswordHasher
{
	private readonly static int s_saltSize = 16;
	private readonly static int s_keySize = 32;
	private readonly static int s_iterations = 100000;
	private readonly static HashAlgorithmName s_algorithm = HashAlgorithmName.SHA256;

	private char _segmentDelimiter = ':';

	public string HashPassword(string input)
	{
		byte[] salt = RandomNumberGenerator.GetBytes(s_saltSize);
		byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
			input,
			salt,
			s_iterations,
			s_algorithm,
			s_keySize
		);
		return string.Join(
			_segmentDelimiter,
			Convert.ToHexString(hash),
			Convert.ToHexString(salt),
			s_iterations,
			s_algorithm
		);
	}

	public bool VerifyPassword(string input, string hashed)
	{
		var segments = hashed.Split(_segmentDelimiter);
		var hash = Convert.FromHexString(segments[0]);
		var salt = Convert.FromHexString(segments[1]);
		var iterations = int.Parse(segments[2]);
		var algorithm = new HashAlgorithmName(segments[3]);
		var inputHash = Rfc2898DeriveBytes.Pbkdf2(
			input,
			salt,
			iterations,
			algorithm,
			hash.Length
		);
		return CryptographicOperations.FixedTimeEquals(inputHash, hash);
	}
}
