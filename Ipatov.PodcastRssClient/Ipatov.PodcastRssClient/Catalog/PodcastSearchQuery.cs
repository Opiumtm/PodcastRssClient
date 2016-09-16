namespace Ipatov.PodcastRssClient.Catalog
{
    /// <summary>
    /// Podcast search query.
    /// </summary>
    public abstract class PodcastSearchQuery
    {
        /// <summary>
        /// Search term.
        /// </summary>
        public string Term { get; set; }                 
    }
}