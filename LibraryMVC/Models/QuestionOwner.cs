using Newtonsoft.Json;

namespace LibraryMVC.Models
{
    public class QuestionOwner
    {
        [JsonProperty("account_id")]
        public int AccountId { get; set; }

        [JsonProperty("reputation")]
        public int Reputation { get; set; }

        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [JsonProperty("user_type")]
        public string UserType { get; set; }

        [JsonProperty("accept_rate")]
        public int AcceptRate { get; set; }

        [JsonProperty("profile_image")]
        public string ProfileImage { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }
    }
}
