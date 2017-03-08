using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InstagramAPI.Models
{
    public class FacebookInfo
    {
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [JsonProperty("picture")]
        public Picture UserPicture { get; set; }
        [JsonProperty("id")]
        public string ID { get; set; }

        public class Data
        {
            [JsonProperty("is_silhouette")]
            public bool IsSilhouette { get; set; }
            [JsonProperty("url")]
            public string Url { get; set; }
        }

        public class Picture
        {
            [JsonProperty("data")]
            public Data PictureData { get; set; }
        }
    }
}