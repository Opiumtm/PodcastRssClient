namespace Ipatov.PodcastRssClient.Catalog.DigitalPodcasts
{
    /// <summary>
    /// Digital podcasts search query.
    /// </summary>
    public sealed class DigitalPodcastsSearchQuery : PodcastSearchQuery
    {
        /// <summary>
        /// Application identifier.
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// Sort type.
        /// </summary>
        public SortType Sort { get; set; } = SortType.rel;

        /// <summary>
        /// Search source.
        /// </summary>
        public SearchSource Source { get; set; } = SearchSource.all;

        /// <summary>
        /// Search filter.
        /// </summary>
        public SearchFilter Filter { get; set; } = SearchFilter.nofilter;

        /// <summary>
        /// Search source.
        /// </summary>
        public enum SearchSource
        {
            all,
            title
        }

        /// <summary>
        /// Sort type.
        /// </summary>
        public enum SortType
        {
            rel,
            alpha,
            hits,
            votes,
            subs,
            rating,
            @new
        }

        /// <summary>
        /// Search filter.
        /// </summary>
        public enum SearchFilter
        {
            nofilter,
            noadult,
            noexplicit,
            clean,
            excplicit,
            adult
        }
    }
}