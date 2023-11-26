using MediatR;
using System.Text;
using System.Linq;

namespace Bigon.Infrastructure.Middlewares
{
    public class LoggingBehaviourSimple<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                var response = await next();

                return response;
            }
            catch (Exception ex)
            {
                while (ex.InnerException!=null)
                {
                    ex = ex.InnerException;
                }

                using (var fs = new FileStream($"applogs{DateTime.Now:yyyyMMdd}.txt", FileMode.OpenOrCreate, FileAccess.Write))
                using (var sw = new StreamWriter(fs))
                {
                    var sb = new StringBuilder();

                    sb.AppendLine($"Request: {typeof(TRequest)}");
                    sb.AppendLine($"Error Time: {DateTime.Now:HH:mm:ss.ffff}");
                    sb.AppendLine($"Source: {ex.Source}");
                    sb.AppendLine($"StackTrace: {ex.StackTrace}");
                    //sb.AppendLine($"Error: {ex.Data.}");
                    sb.AppendLine($"Error: {ex.Message}");
                    sb.Append(Environment.NewLine);



                    sw.WriteLine(sb.ToString());
                }


                throw;
            }
        }
    }
}
