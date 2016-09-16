using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ipatov.PodcastRssClient.Catalog
{
    /// <summary>
    /// Podcast search provider.
    /// </summary>
    public interface IPodcastSearchProvider
    {
        /// <summary>
        /// Query for podcasts.
        /// </summary>
        /// <param name="term">Search term.</param>
        /// <returns>Search result.</returns>
        Task<ICollection<IPodcastCatalogEntry>> Search(string term);
    }

    /// <summary>
    /// Podcast search provider.
    /// </summary>
    /// <typeparam name="TArg">Search argument type.</typeparam>
    public interface IPodcastSearchProvider<in TArg> : IPodcastSearchProvider where TArg : PodcastSearchQuery
    {
        /// <summary>
        /// Query for podcasts.
        /// </summary>
        /// <param name="arg">Search argument.</param>
        /// <returns>Search result.</returns>
        Task<ICollection<IPodcastCatalogEntry>> Search(TArg arg);
    }
}