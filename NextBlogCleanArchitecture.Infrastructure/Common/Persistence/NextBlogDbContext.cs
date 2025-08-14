using Microsoft.EntityFrameworkCore;
using NextBlogCleanArchitecture.Application.Abstractions;
using NextBlogCleanArchitecture.Domain.Comments;
using NextBlogCleanArchitecture.Domain.Posts;
using System.Reflection;

namespace NextBlogCleanArchitecture.Infrastructure.Common.Persistence
{
    public class NextBlogDbContext : DbContext, IUnitOfWork
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public NextBlogDbContext(DbContextOptions options) : base(options) { }

        public async Task CommitChangesAsync(CancellationToken cancellationToken = default)
        {
            await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
