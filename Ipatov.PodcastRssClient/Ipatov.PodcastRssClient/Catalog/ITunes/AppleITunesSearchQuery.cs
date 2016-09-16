namespace Ipatov.PodcastRssClient.Catalog.ITunes
{
    /// <summary>
    /// Apple iTunes search query.
    /// </summary>
    public sealed class AppleITunesSearchQuery : PodcastSearchQuery
    {
        /// <summary>
        /// Limit result count.
        /// </summary>
        public int? Limit { get; set; }         
    }
}