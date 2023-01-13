using Lab4.Data.Models;

namespace Lab4.Services;

public interface IPostsService
{
	Task<IEnumerable<Post>?> GetAllAsync();
	Task<IEnumerable<Post>?> GetByUserAsync(int userId);
	Task CreateAsync(Post post);
	Task UpdateAsync(Post post);
	Task DeleteAsync(int id);
}
