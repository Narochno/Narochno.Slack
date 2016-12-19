using System.Collections.Generic;
using Newtonsoft.Json;

namespace Narochno.Slack.Entities
{
    public class Attachment
    {
        /// <summary>
        /// A valid URL to an image file that will be displayed inside a message attachment.
        /// </summary>
        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// A valid URL to an image file that will be displayed as a thumbnail on the right side of a message attachment.
        /// </summary>
        [JsonProperty("thumb_url")]
        public string ThumbUrl { get; set; }

        /// <summary>
        /// A plain-text summary of the attachment. This text will be used in clients that don't show
        /// formatted text (eg. IRC, mobile notifications) and should not contain any markup.
        /// </summary>
        [JsonProperty("fallback")]
        public string Fallback { get; set; }

        /// <summary>
        /// An optional value that can either be one of good, warning, danger, or any hex color code (eg. #439FE0).
        /// This value is used to color the border along the left side of the message attachment.
        /// </summary>
        [JsonProperty("color")]
        public string Color { get; set; }

        /// <summary>
        /// This is optional text that appears above the message attachment block.
        /// </summary>
        [JsonProperty("pretext")]
        public string Pretext { get; set; }

        /// <summary>
        /// Small text used to display the author's name.
        /// </summary>
        [JsonProperty("author_name")]
        public string AuthorName { get; set; }

        /// <summary>
        /// A valid URL that will hyperlink the author_name text mentioned above. Will only work if author_name is present.
        /// </summary>
        [JsonProperty("author_link")]
        public string AuthorLink { get; set; }

        /// <summary>
        /// A valid URL that displays a small 16x16px image to the left of the author_name text. Will only work if author_name is present.
        /// </summary>
        [JsonProperty("author_icon")]
        public string AuthorIcon { get; set; }

        /// <summary>
        /// The title is displayed as larger, bold text near the top of a message attachment.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// By passing a valid URL in the title_link parameter (optional), the title text will be hyperlinked.
        /// </summary>
        [JsonProperty("title_link ")]
        public string TitleLink { get; set; }

        /// <summary>
        /// This is the main text in a message attachment, and can contain standard message markup.
        /// The content will automatically collapse if it contains 700+ characters or 5+ linebreaks,
        /// and will display a "Show more..." link to expand the content.
        /// Links posted in the text field will not unfurl.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// Add some brief text to help contextualize and identify an attachment.
        /// Limited to 300 characters, and may be truncated further when displayed to users in environments with limited screen real estate.
        /// </summary>
        [JsonProperty("footer")]
        public string Footer { get; set; }

        /// <summary>
        /// To render a small icon beside your footer text, provide a publicly accessible URL string in the footer_icon field.
        /// You must also provide a footer for the field to be recognized.
        /// </summary>
        [JsonProperty("footer_icon")]
        public string FooterIcon { get; set; }

        /// <summary>
        /// By providing the ts field with an integer value in "epoch time", the attachment will
        /// display an additional timestamp value as part of the attachment's footer.
        /// </summary>
        [JsonProperty("ts")]
        public long Timestamp { get; set; }

        /// <summary>
        /// Additional fields to pass with this attachment
        /// </summary>
        [JsonProperty("fields")]
        public IList<Field> Fields { get; set; } = new List<Field>();

        /// <summary>
        /// The markdown to use
        /// </summary>
        [JsonProperty("mrkdwn_in")]
        public IList<string> MrkdwnIn { get; set; } = new List<string>();
    }
}