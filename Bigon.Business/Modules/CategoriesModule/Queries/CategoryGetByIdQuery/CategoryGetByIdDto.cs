namespace Bigon.Business.Modules.CategoriesModule.Queries.CategoryGetByIdQuery
{
    public class CategoryGetByIdDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public string ParentName { get; set; }
    }
}
