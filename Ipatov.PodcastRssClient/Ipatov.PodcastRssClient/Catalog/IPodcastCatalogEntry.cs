using System.Collections.Generic;
using Windows.Foundation;

namespace Ipatov.PodcastRssClient.Catalog
{
    /// <summary>
    /// Podcast catalog entry.
    /// </summary>
    public interface IPodcastCatalogEntry
    {
        /// <summary>
        /// RSS feed URL.
        /// </summary>
        string FeedUrl { get; }

        /// <summary>
        /// Artist.
        /// </summary>
        string Artist { get; }

        /// <summary>
        /// Name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Description.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Radio station URL.
        /// </summary>
        string StationUrl { get; }

        /// <summary>
        /// Collection view URL.
        /// </summary>
        string CollectionUrl { get; }

        /// <summary>
        /// Genre.
        /// </summary>
        string PrimaryGenre { get; }

        /// <summary>
        /// Genres.
        /// </summary>
        ICollection<string> Genres { get; }

        /// <summary>
        /// Track count.
        /// </summary>
        int? TrackCount { get; }

        /// <summary>
        /// Get artwork URL for desired size.
        /// </summary>
        /// <param name="desiredSize">Desired size.</param>
        /// <returns>Artwork URL.</returns>
        string GetArtworkUrl(Size desiredSize);

        /// <summary>
        /// Artwork URL with the better image quality.
        /// </summary>
        string ArtworkUrl { get; }
    }
}