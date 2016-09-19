namespace Ipatov.PodcastRssClient.Podcast
{
    /// <summary>
    /// Channel owner.
    /// </summary>
    public interface IPodcastChannelOwner
    {
        /// <summary>
        /// Owner name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Email.
        /// </summary>
        string Email { get; }
    }
}