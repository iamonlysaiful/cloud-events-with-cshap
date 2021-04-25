

using CloudNative.CloudEvents;
using CloudNative.CloudEvents.Http;
using CloudNative.CloudEvents.NewtonsoftJson;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Mime;

namespace CloudEventsWithCShap
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            //var cloudEvent = new CloudEvent("com.example.myevent", new Uri("urn:example-com:mysource"))
            //{
            //    DataContentType = new ContentType(MediaTypeNames.Application.Json),
            //    Data = JsonConvert.SerializeObject("{\"name\":\"morpheus\",\"job\":\"leader\"}")
            //};

            //var content = new CloudEventContent(cloudEvent,
            //                                     ContentMode.Structured,
            //                                     new JsonEventFormatter());

            //var httpClient = new HttpClient();
            //var result = (await httpClient.PostAsync(new Uri("https://reqres.in/api/users"), content));
            //Console.WriteLine(result.IsSuccessStatusCode);
            //Console.ReadLine();

            var cloudEvent = new CloudEvent()
            {
                Id = Guid.NewGuid().ToString(),
                DataContentType = MediaTypeNames.Application.Json,
                Data = Newtonsoft.Json.JsonConvert.SerializeObject("{\"name\":\"morpheus\",\"job\":\"leade\"}"),
                Type = "com.example.myevent",
                Source = new Uri("urn:example-com:mysource:abc"),
            };

            var content = cloudEvent.ToHttpContent(ContentMode.Structured, new JsonEventFormatter());

            var httpClient = new HttpClient();
            var result = (await httpClient.PostAsync(new Uri("https://localhost:44378/create-user"), content));
            if (HttpStatusCode.OK == result.StatusCode)
            {
                Console.WriteLine(result.StatusCode);
            }

        }
    }
}
