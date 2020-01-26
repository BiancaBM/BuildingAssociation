using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Website.Helpers
{
    public abstract class FileActionResult : IHttpActionResult
    {
        private string MediaType { get; }
        private string FileName { get; }
        private Stream Data { get; }

        protected FileActionResult(string data, string fileName, string mediaType)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(data);
            writer.Flush();
            stream.Position = 0;

            Data = stream;
            FileName = fileName;
            MediaType = mediaType;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            Data.Position = 0;
            var response = new HttpResponseMessage
            {
                Content = new StreamContent(Data)
            };
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(MediaType);
            response.Content.Headers.ContentDisposition.FileName = FileName;
            response.Content.Headers.ContentLength = Data.Length;

            return Task.FromResult(response);
        }

    }

    public class ExcelFileActionResult : FileActionResult
    {
        public ExcelFileActionResult(string data, string name) : base(data, name +".xls", "application/vnd.ms-excel")
        {
        }
    }
}