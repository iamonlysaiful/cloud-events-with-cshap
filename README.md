# Explore CloudEvents With C#

CloudEvents is a specification for describing event data in a common way. CloudEvents seeks to dramatically simplify event declaration and delivery across services, platforms, and beyond!

CloudEvents is a new effort and it's still under active development. However, its working group has received a surprising amount of industry interest, ranging from major cloud providers to popular SaaS companies. The specification is now under the Cloud Native Computing Foundation. [Read more...](https://cloudevents.io)


## Create Cloud Event 

The CloudEvent class reflects the event envelope defined by the CNCF CloudEvents 1.0 specification. It supports version 1.0 of CloudEvents by default. It can also handle the pre-release versions 0.1, 0.2, and 0.3 of the CloudEvents specification.

The strongly typed API reflects the 1.0 standard. Here are all properties of 1.0 standard CloudEvent class. [Reade more…](https://github.com/cloudevents/sdk-csharp)

![image](https://user-images.githubusercontent.com/25750962/115988345-172a4880-a5db-11eb-9a40-218afe1919cc.png)

![image](https://user-images.githubusercontent.com/25750962/115988377-42ad3300-a5db-11eb-9e4e-9ed847b3aba5.png)

```
var cloudEvent = new CloudEvent()
{
    Id = Guid.NewGuid().ToString(),
    DataContentType = MediaTypeNames.Application.Json,
    Data = Newtonsoft.Json.JsonConvert.SerializeObject("{\"name\":\"morpheus\",\"job\":\"leade\"}"),
    Type = "com.example.myevent",
    Source = new Uri("urn:example-com:mysource:abc"),
};
```
This snippet shows how to create a CloudEvent using C# SDK. In the image we can see dependencies we used from SDK to create CloudEvent in our code.

N.B. To create CloudEvent I use C# Console application in my solution.

## Send CloudEvent

The C# SDK of CloudEvents helps with mapping CloudEvents to and from messages or transport frames of popular .NET clients in such a way as to be agnostic of your application's choices of how you want to send an event (be it via HTTP PUT or POST) or how you want to handle settlement of transfers in AMQP or MQTT. [Reade more…](https://github.com/cloudevents/sdk-csharp)

```
var content = cloudEvent.ToHttpContent(ContentMode.Structured, new JsonEventFormatter());

var httpClient = new HttpClient();
var result = (await httpClient.PostAsync(new Uri("https://localhost:44378/create-user"),content));
```
This snippet shows how to send a CloudEvent. Here we send a HTTP Post request with CloudEvent to an API.

## Receieve CloudEvent

![image](https://user-images.githubusercontent.com/25750962/115988556-23fb6c00-a5dc-11eb-8f70-9e89eadaa3d8.png)

```
[HttpPost("/create-user")]
public IActionResult ReceiveCloudEvent([FromBody] CloudEvent cloudEvent)
{
    var cloudE = JsonConvert.DeserializeObject(cloudEvent.Data.ToString());
    return Ok();
}
```
This snippet works as a receiver of CloudEvent in our API project. Here are the dependencies we used in image to configure receiver.

N.B. To create CloudEvent I use .NET Core 3.1 API application in my solution.
