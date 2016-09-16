using System.Collections.Generic;
using System.Threading.Tasks;
using Ipatov.PodcastRssClient.Catalog.DigitalPodcasts.Internal;

namespace Ipatov.PodcastRssClient.Catalog.DigitalPodcasts
{
    /// <summary>
    /// Digital Podcast service catalog.
    /// </summary>
    public sealed class DigitalPodcastCatalog : IPodcastSearchProvider<DigitalPodcastsSearchQuery>
    {
        /// <summary>
        /// Current Application Id.
        /// </summary>
        public static string AppId { get; set; }

        /// <summary>
        /// Query for podcasts.
        /// </summary>
        /// <param name="arg">Search argument.</param>
        /// <returns>Search result.</returns>
        public Task<ICollection<IPodcastCatalogEntry>> Search(DigitalPodcastsSearchQuery arg)
        {
            return Task.FromResult(new OpmlSearchResult(arg) as ICollection<IPodcastCatalogEntry>);
        }

        /// <summary>
        /// Query for podcasts.
        /// </summary>
        /// <param name="term">Search term.</param>
        /// <returns>Search result.</returns>
        public Task<ICollection<IPodcastCatalogEntry>> Search(string term)
        {
            return Search(new DigitalPodcastsSearchQuery() {Term = term, AppId = AppId });
        }
    }
}