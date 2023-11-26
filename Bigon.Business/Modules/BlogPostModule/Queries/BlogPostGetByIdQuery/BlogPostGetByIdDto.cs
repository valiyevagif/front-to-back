namespace Bigon.Business.Modules.BlogPostModule.Queries.BlogPostGetByIdQuery
{
    public class BlogPostGetByIdDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public string Body { get; set; }
        public string Slug { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public DateTime? PublishedAt { get; set; }
        public int? PublishedBy { get; set; }
        public string[] Tags { get; set; }
        public int Comments { get; set; }
    }
}
