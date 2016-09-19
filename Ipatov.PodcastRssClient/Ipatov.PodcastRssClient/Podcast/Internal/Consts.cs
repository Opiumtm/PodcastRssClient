using System.Xml.Linq;

namespace Ipatov.PodcastRssClient.Podcast.Internal
{
    /// <summary>
    /// Constants.
    /// </summary>
    public static class Consts
    {
        /// <summary>
        /// itunes namespace.
        /// </summary>
        private const string ItunesNamespace = "http://www.itunes.com/dtds/podcast-1.0.dtd";

        /// <summary>
        /// atom namespace.
        /// </summary>
        private const string AtomNamespace = "http://www.w3.org/2005/Atom";

        /// <summary>
        /// Namespaces.
        /// </summary>
        public static class Namespaces
        {
            /// <summary>
            /// Itunes namespace.
            /// </summary>
            public static readonly XNamespace Itunes = ItunesNamespace;

            /// <summary>
            /// Atom namespace.
            /// </summary>
            public static readonly XNamespace Atom = AtomNamespace;
        }
    }
}