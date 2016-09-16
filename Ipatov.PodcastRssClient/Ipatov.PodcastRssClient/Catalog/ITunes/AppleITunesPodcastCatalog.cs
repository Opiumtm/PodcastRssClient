using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Windows.Web.Http;
using Ipatov.PodcastRssClient.Catalog.ITunes.Internal;
using Newtonsoft.Json;

namespace Ipatov.PodcastRssClient.Catalog.ITunes
{
    /// <summary>
    /// Apple iTunes podcast catalog.
    /// </summary>
    public class AppleITunesPodcastCatalog : IPodcastSearchProvider<AppleITunesSearchQuery>
    {
        private const string BaseUri = "https://itunes.apple.com/search";

        /// <summary>
        /// Query for podcasts.
        /// </summary>
        /// <param name="arg">Search argument.</param>
        /// <returns>Search result.</returns>
        public async Task<ICollection<IPodcastCatalogEntry>> Search(AppleITunesSearchQuery arg)
        {
            string uri;
            var limit = arg?.Limit;
            var term = arg?.Term;
            if (limit != null)
            {
                uri = string.Format("{0}?term={1}&media=podcast&limit={2}", BaseUri, WebUtility.UrlEncode(term ?? ""), limit.Value);
            }
            else
            {
                uri = string.Format("{0}?term={1}&media=podcast", BaseUri, WebUtility.UrlEncode(term ?? ""));
            }
            var uriObj = new Uri(uri, UriKind.Absolute);
            using (var client = new HttpClient())
            {
                var str = await client.GetStringAsync(uriObj);
                var result = JsonConvert.DeserializeObject<ITunesPodcastSearchResult>(str);
                if (result?.Results == null)
                {
                    return new List<IPodcastCatalogEntry>();
                }
                return result.Results.Where(r => r != null).Select(r => new ITunesPodcastCatalogEntry(r)).OfType<IPodcastCatalogEntry>().ToList();
            }
        }

        /// <summary>
        /// Query for podcasts.
        /// </summary>
        /// <param name="term">Search term.</param>
        /// <returns>Search result.</returns>
        public Task<ICollection<IPodcastCatalogEntry>> Search(string term)
        {
            return Search(new AppleITunesSearchQuery() {Term = term});
        }
    }
}