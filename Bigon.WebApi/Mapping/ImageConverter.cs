using AutoMapper;

namespace Bigon.WebApi.Mapping
{
    public class ImageConverter : IValueConverter<string, string>
    {
        public string Convert(string sourceMember, ResolutionContext context)
        {
            var host = context.Items["host"];
            return $"{host}/files/images/{sourceMember}";
        }
    }
}
