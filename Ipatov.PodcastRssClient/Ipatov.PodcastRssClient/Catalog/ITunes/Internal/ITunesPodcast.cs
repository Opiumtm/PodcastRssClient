using Newtonsoft.Json;

namespace Ipatov.PodcastRssClient.Catalog.ITunes.Internal
{
    /// <summary>
    /// ITunes podcast.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class ITunesPodcast
    {
        [JsonProperty("wrapperType")]
        public string WrapperType { get; set; }

        [JsonProperty("kind")]
        public string Kind { get; set; }

        [JsonProperty("collectionId")]
        public int CollectionId { get; set; }

        [JsonProperty("trackId")]
        public int TrackId { get; set; }

        [JsonProperty("artistName")]
        public string ArtistName { get; set; }

        [JsonProperty("collectionName")]
        public string CollectionName { get; set; }

        [JsonProperty("trackName")]
        public string TrackName { get; set; }

        [JsonProperty("collectionCensoredName")]
        public string CollectionCensoredName { get; set; }

        [JsonProperty("trackCensoredName")]
        public string TrackCensoredName { get; set; }

        [JsonProperty("collectionViewUrl")]
        public string CollectionViewUrl { get; set; }

        [JsonProperty("feedUrl")]
        public string FeedUrl { get; set; }

        [JsonProperty("trackViewUrl")]
        public string TrackViewUrl { get; set; }

        [JsonProperty("artworkUrl30")]
        public string ArtworkUrl30 { get; set; }

        [JsonProperty("artworkUrl60")]
        public string ArtworkUrl60 { get; set; }

        [JsonProperty("artworkUrl100")]
        public string ArtworkUrl100 { get; set; }

        [JsonProperty("releaseDate")]
        public string ReleaseDate { get; set; }

        [JsonProperty("collectionExplicitness")]
        public string CollectionExplicitness { get; set; }

        [JsonProperty("trackExplicitness")]
        public string TrackExplicitness { get; set; }

        [JsonProperty("trackCount")]
        public int TrackCount { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("primaryGenreName")]
        public string PrimaryGenreName { get; set; }

        [JsonProperty("radioStationUrl")]
        public string RadioStationUrl { get; set; }

        [JsonProperty("artworkUrl600")]
        public string ArtworkUrl600 { get; set; }

        [JsonProperty("genreIds")]
        public string[] GenreIds { get; set; }

        [JsonProperty("genres")]
        public string[] Genres { get; set; }
    }

    // ReSharper disable once InconsistentNaming
}