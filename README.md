# Narochno.Slack [![Build status](https://ci.appveyor.com/api/projects/status/ru0cu26ooe7bbigk/branch/master?svg=true)](https://ci.appveyor.com/project/Narochno/narochno-slack/branch/master) [![NuGet](https://img.shields.io/nuget/v/Narochno.Slack.svg)](https://www.nuget.org/packages/Narochno.Slack/)

A simple Slack client for posting messages with fields and attachments, narochno.

## Example Usage

```csharp
var config = new SlackConfig
{
    WebHookUrl = "your webhook URL"
};

var message = new IncomingWebHookRequest
{
    Text = "test",
    Channel = "#test",
    UserName = "a user",
    IconEmoji = ":ghost:",
    Attachments = new List<Attachment>
    {
        new Attachment
        {
            Title = "tite",
            Color = "#993",
            Fallback = "fallback"
        }
    }
};

# Optionally dispose
using (var slackClient = new SlackClient(config))
{
    await slackClient.IncomingWebHook(message);
}
```

ASP.NET Core DI setup

```csharp
var services  = new ServiceCollection();
services.AddSlack(new SlackConfig{ ... });
var provider = services.BuildServiceProvider();
```

Or completely manually to control building of HTTP client:

```csharp
var services = new ServiceCollection();

services.AddSingleton(new SlackConfig{ ... })
services.AddHttpClient<ISlackClient, SlackClient>()
            .AddPolicyHandler(GetRetryPolicy()); // defined in the consumer's code

var provider = services.BuildServiceProvider();

var slackClient = provider.GetRequiredService<ISlackClient>();
```
