namespace Ipatov.PodcastRssClient.Podcast
{
    /// <summary>
    /// Podcast channel image.
    /// </summary>
    public interface IPodcastChannelImage
    {
        /// <summary>
        /// Image URL.
        /// </summary>
        string ImageUrl { get; }

        /// <summary>
        /// Title.
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Link.
        /// </summary>
        string Link { get; }

        /// <summary>
        /// Width.
        /// </summary>
        int? Width { get; }

        /// <summary>
        /// Height.
        /// </summary>
        int? Height { get; }
    }
}