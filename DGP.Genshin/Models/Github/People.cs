﻿using Newtonsoft.Json;

namespace DGP.Genshin.Models.Github
{
    public class People
    {
        [JsonProperty("login")] public string Login { get; set; }
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("avatar_url")] public string AvatarUrl { get; set; }
        [JsonProperty("url")] public string Url { get; set; }
        [JsonProperty("html_url")] public string HtmlUrl { get; set; }
        [JsonProperty("followers_url")] public string FollowersUrl { get; set; }
        [JsonProperty("following_url")] public string FollowingUrl { get; set; }
        [JsonProperty("gists_url")] public string GistsUrl { get; set; }
        [JsonProperty("starred_url")] public string StarredUrl { get; set; }
        [JsonProperty("subscriptions_url")] public string SubscriptionsUrl { get; set; }
        [JsonProperty("organizations_url")] public string OrganizationsUrl { get; set; }
        [JsonProperty("repos_url")] public string ReposUrl { get; set; }
        [JsonProperty("events_url")] public string EventsUrl { get; set; }
        [JsonProperty("received_events_url")] public string ReceivedEventsUrl { get; set; }
        [JsonProperty("type")] public string Type { get; set; }
    }
}
