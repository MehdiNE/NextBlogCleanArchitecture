using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NextBlogCleanArchitecture.Domain.Posts;

namespace NextBlogCleanArchitecture.Infrastructure.Posts.Persistence
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(x => x.Id);

            builder
            .HasMany(p => p.Comments)              // Post has many Comments
            .WithOne(c => c.Post)                  // Each Comment belongs to one Post
            .HasForeignKey(c => c.PostId)          // Foreign key in Comment table
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
