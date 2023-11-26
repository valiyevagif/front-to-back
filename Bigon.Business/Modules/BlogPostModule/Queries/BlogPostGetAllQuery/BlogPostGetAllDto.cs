namespace Bigon.Business.Modules.BlogPostModule.Queries.BlogPostGetAllQuery
{
    public class BlogPostGetAllDto
    {
        public int Id { get; set; }
        public string Body { get; set; }

        public string Title { get; set; }
        public string ImagePath { get; set; }
        public string Slug { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public DateTime? PublishedAt { get; set; }
    }
}
