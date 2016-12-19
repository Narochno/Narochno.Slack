using System.Runtime.Serialization;

namespace Narochno.Slack.Entities
{
    public enum SlackCode
    {
        [EnumMember(Value = "ok")]
        Ok,

        [EnumMember(Value = "invalid_payload")]
        InvalidPayload,

        [EnumMember(Value = "user_not_found")]
        UserNotFound,

        [EnumMember(Value = "channel_not_found")]
        ChannelNotFound,

        [EnumMember(Value = "channel_is_archived")]
        ChannelIsArchived,

        [EnumMember(Value = "action_prohibited")]
        ActionProhibited,

        [EnumMember(Value = "posting_to_general_channel_denied")]
        PostingToGeneralChannelDenied,

        [EnumMember(Value = "too_many_attachments")]
        TooManyAttachments,
    }
}