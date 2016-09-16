using System.Net;
using System.Text;

namespace Ipatov.PodcastRssClient.Catalog.DigitalPodcasts.Internal
{
    /// <summary>
    /// Search URL builder.
    /// </summary>
    public static class DigitalPodcastsSearchUrlBuilder
    {
        private const string BaseUri = "http://api.digitalpodcast.com/v2r/search/?";

        /// <summary>
        /// Build digital podcast search url.
        /// </summary>
        /// <param name="query">Search query.</param>
        /// <param name="results">Results count.</param>
        /// <param name="start">Skip count.</param>
        /// <returns></returns>
        public static string ToUrl(this DigitalPodcastsSearchQuery query, uint results, uint start)
        {
            var sb = new StringBuilder();
            sb.Append(BaseUri);
            sb.Append("appid=" + WebUtility.UrlEncode(DigitalPodcastCatalog.AppId));
            sb.Append("&keywords=" + WebUtility.UrlEncode(query?.Term ?? ""));
            if (results > 50)
            {
                results = 50;
            }
            if (results < 1)
            {
                results = 1;
            }
            sb.Append("&start=" + start);
            sb.Append("&results=" + results);
            if (query != null)
            {
                sb.Append("&sort=" + query.Sort);
                sb.Append("&searchsource=" + query.Source);
                sb.Append("&contentfilter=" + query.Filter);
            }
            sb.Append("&format=rssopml");
            return sb.ToString();
        }
    }
}