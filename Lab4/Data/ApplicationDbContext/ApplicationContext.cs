using Lab4.Data.Models;

using Microsoft.EntityFrameworkCore;

namespace Lab4.Data.ApplicationDbContext;

public sealed class ApplicationContext : DbContext
{
	public DbSet<User> Users { get; set; }
	public DbSet<Post> Posts { get; set; }
	public ApplicationContext(DbContextOptions<ApplicationContext> contextOptions)
		: base(contextOptions) { }
}
