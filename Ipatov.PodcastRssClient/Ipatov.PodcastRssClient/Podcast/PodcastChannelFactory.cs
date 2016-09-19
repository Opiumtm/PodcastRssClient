using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Web.Http;
using Ipatov.PodcastRssClient.Podcast.Internal;

namespace Ipatov.PodcastRssClient.Podcast
{
    /// <summary>
    /// Channel factory.
    /// </summary>
    public static class PodcastChannelFactory
    {
        /// <summary>
        /// Create channel object.
        /// </summary>
        /// <param name="document">XML document.</param>
        /// <param name="originalLink">Original link.</param>
        /// <returns>Podcast channel object.</returns>
        public static IPodcastChannel Create(XDocument document, string originalLink = null)
        {
            return new PodcastChannel(document, originalLink);
        }

        /// <summary>
        /// Create channel object.
        /// </summary>
        /// <param name="document">XML document.</param>
        /// <param name="originalLink">Original link.</param>
        /// <returns>Podcast channel object.</returns>
        public static IPodcastChannel Create(string document, string originalLink = null)
        {
            return new PodcastChannel(XDocument.Parse(document), originalLink);
        }

        /// <summary>
        /// Create channel object.
        /// </summary>
        /// <param name="stream">Data stream.</param>
        /// <param name="originalLink">Original link.</param>
        /// <returns>Podcast channel object.</returns>
        public static IPodcastChannel Create(Stream stream, string originalLink = null)
        {
            return new PodcastChannel(XDocument.Load(stream), originalLink);
        }

        /// <summary>
        /// Create channel object.
        /// </summary>
        /// <param name="reader">Text reader.</param>
        /// <param name="originalLink">Original link.</param>
        /// <returns>Podcast channel object.</returns>
        public static IPodcastChannel Create(TextReader reader, string originalLink = null)
        {
            return new PodcastChannel(XDocument.Load(reader), originalLink);
        }

        private static readonly HttpClient SharedClient = new HttpClient();

        /// <summary>
        /// Loda podcast using standard http options.
        /// </summary>
        /// <param name="feedUri">RSS Feed uri</param>
        /// <returns>Podcast channel object.</returns>
        public static async Task<IPodcastChannel> Load(Uri feedUri)
        {
            var str = await SharedClient.GetStringAsync(feedUri);
            return new PodcastChannel(XDocument.Parse(str));
        }
    }
}