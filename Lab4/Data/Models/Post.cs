namespace Lab4.Data.Models;

public sealed class Post
{
	public int Id { get; set; }
	public required string Title { get; set; }
	public required string Description { get; set; }
	public required int UserId { get; set; }
	public User? User { get; set; }
}
