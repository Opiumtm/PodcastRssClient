using Newtonsoft.Json;

namespace Ipatov.PodcastRssClient.Catalog.ITunes.Internal
{
    /// <summary>
    /// ITunes podcast search result.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class ITunesPodcastSearchResult
    {
        /// <summary>
        /// Result count.
        /// </summary>
        [JsonProperty("resultCount")]
        public int ResultCount { get; set; }

        /// <summary>
        /// Results.
        /// </summary>
        [JsonProperty("results")]
        public ITunesPodcast[] Results { get; set; }
    }
}