using Bigon.Infrastructure.Entities;

namespace Bigon.Infrastructure.Extensions
{
    public static partial class Extension
    {
        public static IEnumerable<Category> GetHierarchy(this IEnumerable<Category> categories, Category parent)
        {
            if (parent.ParentId != null)
                yield return parent;

            foreach (var item in categories.Where(m => m.ParentId == parent.Id).SelectMany(m => categories.GetHierarchy(m)))
            {
                yield return item;
            }
        }
    }
}
