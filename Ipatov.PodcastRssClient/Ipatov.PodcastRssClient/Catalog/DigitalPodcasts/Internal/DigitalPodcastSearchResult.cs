using System.Collections.Generic;
using Windows.Foundation;

namespace Ipatov.PodcastRssClient.Catalog.DigitalPodcasts.Internal
{
    /// <summary>
    /// Digital podcast search result.
    /// </summary>
    public sealed class DigitalPodcastSearchResult : IPodcastCatalogEntry
    {
        private readonly OPMLoutline _dataEntry;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dataEntry">Data entry.</param>
        public DigitalPodcastSearchResult(OPMLoutline dataEntry)
        {
            _dataEntry = dataEntry;
        }

        /// <summary>
        /// RSS feed URL.
        /// </summary>
        public string FeedUrl => _dataEntry?.xmlUrl;

        /// <summary>
        /// Artist.
        /// </summary>
        public string Artist => null;

        /// <summary>
        /// Name.
        /// </summary>
        public string Name => _dataEntry?.text;

        /// <summary>
        /// Description.
        /// </summary>
        public string Description => _dataEntry?.description;

        /// <summary>
        /// Radio station URL.
        /// </summary>
        public string StationUrl => _dataEntry?.htmlUrl;

        /// <summary>
        /// Collection view URL.
        /// </summary>
        public string CollectionUrl => null;

        /// <summary>
        /// Genre.
        /// </summary>
        public string PrimaryGenre => null;

        /// <summary>
        /// Genres.
        /// </summary>
        public ICollection<string> Genres => null;

        /// <summary>
        /// Track count.
        /// </summary>
        public int? TrackCount => null;

        /// <summary>
        /// Get artwork URL for desired size.
        /// </summary>
        /// <param name="desiredSize">Desired size.</param>
        /// <returns>Artwork URL.</returns>
        public string GetArtworkUrl(Size desiredSize)
        {
            return null;
        }

        /// <summary>
        /// Artwork URL with the better image quality.
        /// </summary>
        public string ArtworkUrl => null;
    }
}