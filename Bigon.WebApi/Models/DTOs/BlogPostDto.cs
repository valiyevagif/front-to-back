namespace Bigon.WebApi.Models.DTOs
{
    public class BlogPostDto
    {
        public int Id { get; set; }
        public string Body { get; set; }

        public string Name { get; set; }
        public string Image { get; set; }
        public string Slug { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string PublishedAt { get; set; }
    }
}
