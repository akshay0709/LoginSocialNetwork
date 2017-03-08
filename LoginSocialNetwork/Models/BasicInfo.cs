using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InstagramAPI.Models
{
    public class BasicInfo
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("user")]
        public User UserInfo { get; set; }

        public class User
        {
            [JsonProperty("bio")]
            public string Bio { get; set; }
            [JsonProperty("id")]
            public string Id { get; set; }
            [JsonProperty("website")]
            public string Website { get; set; }
            [JsonProperty("username")]
            public string Username { get; set; }
            [JsonProperty("full_name")]
            public string Fullname { get; set; }
            [JsonProperty("profile_picture")]
            public string ProfilePicture { get; set; }
        }
    }
}