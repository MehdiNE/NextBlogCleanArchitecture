using FluentValidation;

namespace NextBlogCleanArchitecture.Application.Posts.Commands.CreatePost
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MinimumLength(1);

            RuleFor(x => x.Content).NotEmpty().MinimumLength(1);
        }
    }
}
