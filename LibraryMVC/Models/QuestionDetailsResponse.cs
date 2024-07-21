using Newtonsoft.Json;

namespace LibraryMVC.Models
{
    public class QuestionDetailsResponse
    {
        [JsonProperty("tags")]
        public List<string> Tags { get; set; }

        [JsonProperty("owner")]
        public QuestionOwner Owner { get; set; }

        [JsonProperty("is_answered")]
        public bool IsAnswered { get; set; }

        [JsonProperty("view_count")]
        public int ViewCount { get; set; }

        [JsonProperty("answer_count")]
        public int AnswerCount { get; set; }

        [JsonProperty("score")]
        public int Score { get; set; }

        [JsonProperty("last_activity_date")]
        public long LastActivityDate { get; set; }

        [JsonProperty("creation_date")]
        public long CreationDate { get; set; }

        [JsonProperty("question_id")]
        public int QuestionId { get; set; }

        [JsonProperty("content_license")]
        public string ContentLicense { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
