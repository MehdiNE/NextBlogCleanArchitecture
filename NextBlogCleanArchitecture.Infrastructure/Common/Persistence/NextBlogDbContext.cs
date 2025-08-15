using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NextBlogCleanArchitecture.Application.Abstractions;
using NextBlogCleanArchitecture.Domain.Comments;
using NextBlogCleanArchitecture.Domain.Follows;
using NextBlogCleanArchitecture.Domain.Posts;
using NextBlogCleanArchitecture.Domain.Users;
using System.Reflection;

namespace NextBlogCleanArchitecture.Infrastructure.Common.Persistence
{
    public class NextBlogDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>, IUnitOfWork
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> DomainUsers { get; set; }
        public DbSet<Follow> Follow { get; set; }

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
