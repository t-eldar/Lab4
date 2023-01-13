namespace Lab4.Data.Models;

public sealed class User
{
	public int Id { get; set; }
	public required string Username { get; set; }
	public required string Password { get; set; } 
}
