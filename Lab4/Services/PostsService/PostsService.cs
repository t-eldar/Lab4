using Lab4.Data.ApplicationDbContext;
using Lab4.Data.Models;
using Lab4.Exceptions;

using Microsoft.EntityFrameworkCore;

namespace Lab4.Services;

public sealed class PostsService : IPostsService
{
	private readonly ApplicationContext _applicationContext;
	public PostsService(ApplicationContext applicationContext)
	{
		_applicationContext = applicationContext;
	}
	public async Task<IEnumerable<Post>?> GetAllAsync()
		=> await _applicationContext.Posts
			.Include(p => p.User)
			.ToListAsync();
	public async Task<IEnumerable<Post>?> GetByUserAsync(int userId)
		=> await _applicationContext.Posts
		.Include(p => p.User)
		.Where(p => p.UserId == userId)
		.ToListAsync();
	public async Task CreateAsync(Post post)
	{
		_applicationContext.Posts.Add(post);
		await _applicationContext.SaveChangesAsync();
	}
	public async Task DeleteAsync(int id)
	{
		var post = await _applicationContext.Posts.FirstOrDefaultAsync(p => p.Id == id);
		if (post is null)
		{
			throw new ServiceException(ExceptionType.NotFound, $"No post with id = {id}");
		}
		_applicationContext.Posts.Remove(post);
		await _applicationContext.SaveChangesAsync();
	}
	public async Task UpdateAsync(Post post)
	{
		var entity = await _applicationContext.Posts.FirstOrDefaultAsync(p => p.Id == post.Id);
		if (entity is null)
		{
			throw new ServiceException(ExceptionType.NotFound, $"No post with id = {post.Id}");
		}
		entity.Title = post.Title;
		entity.Description = post.Description;
		await _applicationContext.SaveChangesAsync();
	}
}
