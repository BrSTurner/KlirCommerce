using Klir.TechChallenge.Core.Notifications;
using Klir.TechChallenge.Web.Api.Models;
using System.Text.Json;

namespace Klir.TechChallenge.Web.Api.Middlewares
{
    public class NotificationMiddleware
    {
        private readonly RequestDelegate _next;

        public NotificationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, INotificator notificator)
        {
            using (var responseBodyStream = new MemoryStream())
            {
                var bodyStream = context.Response.Body;

                try
                {
                    context.Response.Body = responseBodyStream;

                    await _next(context);

                    responseBodyStream.Seek(0, SeekOrigin.Begin);
                    var responseBody = new StreamReader(responseBodyStream).ReadToEnd();

                    if (notificator.HasErrors)
                    {
                        var notifications = notificator.GetErrorMessages();
                        var newReponse = CustomResponse.ErrorResponse(notifications);

                        responseBody = JsonSerializer.Serialize(newReponse);
                    }

                    using (var newStream = new MemoryStream())
                    {
                        var sw = new StreamWriter(newStream);
                        sw.Write(responseBody);
                        sw.Flush();

                        newStream.Seek(0, SeekOrigin.Begin);

                        await newStream.CopyToAsync(bodyStream);
                    }
                }
                finally
                {
                    context.Response.Body = bodyStream;
                }
            }
        }
    }
}
