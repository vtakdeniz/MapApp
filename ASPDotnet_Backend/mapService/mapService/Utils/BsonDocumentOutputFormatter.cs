using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace mapService.Utils
{
    public class BsonDocumentOutputFormatter : TextOutputFormatter
    {
        public BsonDocumentOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/json"));

            SupportedEncodings.Add(Encoding.UTF8);
        }

        protected override bool CanWriteType(Type type)
        {
            if ((type==typeof(BsonDocument))||type==typeof(List<BsonDocument>)) {
                return base.CanWriteType(type);
            }
            return false;
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            List<BsonDocument> resObj = new List<BsonDocument>();
            if (context.Object == null)
            {
                response.StatusCode = StatusCodes.Status204NoContent;
                return;
            }

            //Cast context object into bson document list
            if (typeof(List<BsonDocument>) != context.Object.GetType())
            {
                resObj.Add((BsonDocument)context.Object);
            }
            else {
                resObj.AddRange((List<BsonDocument>)context.Object);
            }
            var len = 0;
            await response.WriteAsync("[ ");
            foreach (BsonDocument doc in resObj) {
                {
                    len++;
                    if (doc.TryGetValue("httpStatusCode", out BsonValue status))
                    {
                        // If status code is 204 then exit immediately
                        if (status.AsInt32 == StatusCodes.Status204NoContent) return;

                        response.StatusCode = status.AsInt32;
                        doc.Remove("httpStatusCode");
                    }
                }
                
                try
                {
                    if (len != resObj.Count)
                    {
                        await response.WriteAsync(JsonSerializer.Serialize(BsonTypeMapper.MapToDotNetValue(doc)) + " ,");
                    }
                    else {
                        await response.WriteAsync(JsonSerializer.Serialize(BsonTypeMapper.MapToDotNetValue(doc)) );
                    }
                    
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
            await response.WriteAsync(" ]");

        }
    }
}
