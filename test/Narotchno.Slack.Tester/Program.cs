using Narochno.Slack;
using Narochno.Slack.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Narotchno.Slack.Tester
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

            var config = new SlackConfig
            {
                WebHookUrl = "your webhook URL"
            };

            using (var slackClient = new SlackClient(config))
            {
                Console.WriteLine(await slackClient.PostMessage(new Message { Text = "test" }));

                Console.WriteLine(await slackClient.PostMessage(new Message
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
                }));
            }

            Console.WriteLine("Finished posting messages to slack");
            Console.ReadLine();
        }
    }
}
