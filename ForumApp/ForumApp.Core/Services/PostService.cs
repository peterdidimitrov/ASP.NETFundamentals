using ForumApp.Core.Contracts;
using ForumApp.Core.Models;
using ForumApp.Infrastructure.Data;
using ForumApp.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ForumApp.Core.Services
{
    public class PostService : IPostService
    {
        private readonly ForumAppDbContext context;

        private readonly ILogger<PostService> logger;

        public PostService(ForumAppDbContext _context,
            ILogger<PostService> _logger)
        {
            context = _context;
            logger = _logger;
        }

        public async Task AddAsync(PostModel model)
        {
            var entity = new Post()
            {
                Title = model.Title,
                Content = model.Content
            };

            try
            {
                await context.AddAsync(entity);

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                logger.LogError(ex, "PostService.AddAsync");

                throw new ApplicationException("Operation failed. Please, try again");
            }
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetEntityByIdAsync(id);

            context.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task EditAsync(PostModel model)
        {
            var entity = await GetEntityByIdAsync(model.Id);

            entity.Title = model.Title;
            entity.Content = model.Content;

            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PostModel>> GetAllPosts()
        {
            return await context.Posts
                .AsNoTracking()
                .Select(p => new PostModel()
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content,
                })
                .ToListAsync();
        }

        public async Task<PostModel?> GetByIdAsync(int id)
        {
            return await context.Posts
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => new PostModel()
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content
                })
                .FirstOrDefaultAsync();
        }

        private async Task<Post> GetEntityByIdAsync(int id)
        {
            var entity = await context.FindAsync<Post>(id);

            if (entity == null)
            {
                throw new ApplicationException("Invalid Post");
            }

            return entity;
        }
    }
}
