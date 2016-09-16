using System;
using System.Collections.Generic;
using Windows.Foundation;

namespace Ipatov.PodcastRssClient.Catalog.ITunes.Internal
{
    /// <summary>
    /// Apple iTunes podcast catalog entry.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public sealed class ITunesPodcastCatalogEntry : IPodcastCatalogEntry
    {
        private readonly ITunesPodcast _dataEntry;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dataEntry">Data entry.</param>
        public ITunesPodcastCatalogEntry(ITunesPodcast dataEntry)
        {
            _dataEntry = dataEntry;
        }

        /// <summary>
        /// RSS feed URL.
        /// </summary>
        public string FeedUrl => _dataEntry?.FeedUrl;

        /// <summary>
        /// Artist.
        /// </summary>
        public string Artist => _dataEntry?.ArtistName;

        /// <summary>
        /// Name.
        /// </summary>
        public string Name => _dataEntry?.CollectionName;

        /// <summary>
        /// Description.
        /// </summary>
        public string Description => null;

        /// <summary>
        /// Radio station URL.
        /// </summary>
        public string StationUrl => _dataEntry?.RadioStationUrl;

        /// <summary>
        /// Collection view URL.
        /// </summary>
        public string CollectionUrl => _dataEntry?.CollectionViewUrl;

        /// <summary>
        /// Genre.
        /// </summary>
        public string PrimaryGenre => _dataEntry?.PrimaryGenreName;

        /// <summary>
        /// Genres.
        /// </summary>
        public ICollection<string> Genres => _dataEntry?.Genres;

        /// <summary>
        /// Track count.
        /// </summary>
        public int? TrackCount => _dataEntry?.TrackCount;

        /// <summary>
        /// Get artwork URL for desired size.
        /// </summary>
        /// <param name="desiredSize">Desired size.</param>
        /// <returns>Artwork URL.</returns>
        public string GetArtworkUrl(Size desiredSize)
        {
            if (desiredSize.Width > 100.0)
            {
                return _dataEntry?.ArtworkUrl600 ?? _dataEntry?.ArtworkUrl100 ?? _dataEntry?.ArtworkUrl60 ?? _dataEntry?.ArtworkUrl30;
            }
            if (desiredSize.Width > 60.0)
            {
                return _dataEntry?.ArtworkUrl100 ?? _dataEntry?.ArtworkUrl600 ?? _dataEntry?.ArtworkUrl60 ?? _dataEntry?.ArtworkUrl30;
            }
            if (desiredSize.Width > 30.0)
            {
                return _dataEntry?.ArtworkUrl60 ?? _dataEntry?.ArtworkUrl100 ?? _dataEntry?.ArtworkUrl600 ?? _dataEntry?.ArtworkUrl30;
            }
            return _dataEntry?.ArtworkUrl30 ?? _dataEntry?.ArtworkUrl60 ?? _dataEntry?.ArtworkUrl100 ?? _dataEntry?.ArtworkUrl600;
        }

        /// <summary>
        /// Artwork URL with the better image quality.
        /// </summary>
        public string ArtworkUrl => _dataEntry?.ArtworkUrl600 ?? _dataEntry?.ArtworkUrl100 ?? _dataEntry?.ArtworkUrl60 ?? _dataEntry?.ArtworkUrl30;
    }
}