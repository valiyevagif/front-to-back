using AutoMapper;

namespace Bigon.WebApi.Mapping
{
    public class DateConverter : IValueConverter<DateTime?, string>
    {
        public string Convert(DateTime? sourceMember, ResolutionContext context)
        {
            if (sourceMember is not null && context.Items.ContainsKey("dateFormat") && context.Items["dateFormat"] is string df && !string.IsNullOrWhiteSpace(df)) 
                return sourceMember.Value.ToString(df);


            return sourceMember?.ToString();
        }
    }
}
