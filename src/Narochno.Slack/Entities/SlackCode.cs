using System.Runtime.Serialization;

namespace Narochno.Slack.Entities
{
    public enum SlackCode
    {
        /// <summary>
        /// Message was sent successfully.
        /// </summary>
        [EnumMember(Value = "ok")]
        Success,
        /// <summary>
        /// Typically indicates that received request is malformed — perhaps the JSON is structured incorrectly,
        /// or the message text is not properly escaped. The request should not be retried without correction.
        /// </summary>
        [EnumMember(Value = "invalid_payload")]
        InvalidPayload,
        /// <summary>
        /// Indicates that the user being addressed does not exist or is invalid.
        /// The request should not be retried without modification or until the indicated user is set up.
        /// </summary>
        [EnumMember(Value = "user_not_found")]
        UserNotFound,
        /// <summary>
        /// Indicates that the channel being addressed does not exist or is invalid.
        /// The request should not be retried without modification or until the indicated channel is set up.
        /// </summary>
        [EnumMember(Value = "channel_not_found")]
        ChannelNotFound,
        /// <summary>
        /// Indicates the specified channel has been archived and is no longer accepting new messages.
        /// </summary>
        [EnumMember(Value = "channel_is_archived")]
        ChannelIsArchived,
        /// <summary>
        /// Usually means that a team admin has placed some kind of restriction on this avenue of
        /// posting messages and that, at least for now, the request should not be attempted again.
        /// </summary>
        [EnumMember(Value = "action_prohibited")]
        ActionProhibited,
        /// <summary>
        /// is thrown when an incoming webhook attempts to post to the "#general" channel for a team where posting to that channel is
        /// 1) restricted and
        /// 2) the creator of the same incoming webhook is not authorized to post there.
        /// You'll receive this error with a HTTP 403.
        /// </summary>
        [EnumMember(Value = "posting_to_general_channel_denied")]
        PostingToGeneralChannelDenied,
        /// <summary>
        /// Is thrown when an incoming webhook attempts to post a message with greater than 100 attachments.
        /// A message can have a maximum of 100 attachments associated with it.
        /// </summary>
        [EnumMember(Value = "too_many_attachments")]
        TooManyAttachments,
    }
}