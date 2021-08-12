using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using MongoDB.Bson;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace mapService.Utils
{
    public class BsonDocumentInputFormatter : TextInputFormatter
    {
        public BsonDocumentInputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/json"));

            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanReadType(Type type)
        {
            return type == typeof(BsonDocument) ? base.CanReadType(type) : false;
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (encoding == null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            var request = context.HttpContext.Request;

            using var reader = new StreamReader(request.Body, encoding);
            try
            {
                var str = await reader.ReadToEndAsync();
                var doc = BsonDocument.Parse(str);

                return await InputFormatterResult.SuccessAsync(doc);
            }
            catch
            {
                return await InputFormatterResult.FailureAsync();
            }
        }
    }
}
