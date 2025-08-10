namespace NextBlogCleanArchitecture.Application.Posts.Queries
{
    public class GetAllPostQueryParams
    {
        public string? SearchTerms { get; init; }
        public string? SortColumn { get; init; }
        public string? SortOrder { get; init; }
        public int Page { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }
}
