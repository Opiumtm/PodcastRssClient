namespace Ipatov.PodcastRssClient.Podcast
{
    /// <summary>
    /// Enclosure.
    /// </summary>
    public interface IPodcastEpisodeEnclosure
    {
        /// <summary>
        /// Media url.
        /// </summary>
        string Url { get; }

        /// <summary>
        /// Length (bytes).
        /// </summary>
        ulong? Length { get; }

        /// <summary>
        /// Content type.
        /// </summary>
        string ContentType { get; }
    }
}