using Narochno.Slack.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Narochno.Slack.Entities.Requests;

namespace Narochno.Slack.Tester
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                DoWork().Wait();
            }
            catch (AggregateException e)
            {
                throw e.Flatten();
            }
        }

        public static async Task DoWork()
        {
            Console.WriteLine("Posting messages to slack");

            var provider = new ServiceCollection()
                .AddSlack(new SlackConfig { WebHookUrl = "<your webhook url>" })
                .BuildServiceProvider();

            var slackClient = provider.GetService<ISlackClient>();

            /// Post some raw text
            await slackClient.PostText("test");

            // Post some markdown
            await slackClient.PostMarkdown("_test_");

            // Post a more complicated message
            await slackClient.IncomingWebHook(new IncomingWebHookRequest
            {
                Text = "test",
                Channel = "#test",
                Username = "a user",
                Emoji = ":ghost:",
                Attachments = new List<Attachment>
                {
                    new Attachment
                    {
                        Title = "tite",
                        Color = "#993",
                        Fallback = "testing",
                        Fields = new List<Field>
                        {
                            new Field
                            {
                                Title = "title",
                                Short = true,
                                Value = "value"
                            }
                        }
                    }
                }
            });

            Console.WriteLine("Finished posting messages to slack");
            Console.ReadLine();
        }
    }
}
