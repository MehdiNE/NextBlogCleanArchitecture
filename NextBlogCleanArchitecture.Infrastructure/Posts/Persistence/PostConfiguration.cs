using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NextBlogCleanArchitecture.Domain.Post;

namespace NextBlogCleanArchitecture.Infrastructure.Posts.Persistence
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.AuthorId)
                .HasColumnType("nvarchar(max)");

        }
    }
}
