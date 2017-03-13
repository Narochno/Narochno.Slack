# Narochno.Slack [![Build status](https://ci.appveyor.com/api/projects/status/ru0cu26ooe7bbigk/branch/master?svg=true)](https://ci.appveyor.com/project/Narochno/narochno-slack/branch/master)
A simple Slack client for posting messages with fields and attachments, narochno.

## Example Usage
```csharp
var config = new SlackConfig
{
    WebHookUrl = "your webhook URL"
};

var message = new Message
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
    await slackClient.PostMessage(message);
}
```