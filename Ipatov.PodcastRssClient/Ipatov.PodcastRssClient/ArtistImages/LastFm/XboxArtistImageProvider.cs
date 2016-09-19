using System.Threading.Tasks;
using Microsoft.Xbox.Music.Platform.Client;
using Microsoft.Xbox.Music.Platform.Contract.DataModel;

namespace Ipatov.PodcastRssClient.ArtistImages.LastFm
{
    /// <summary>
    /// XBOX Client image provider.
    /// </summary>
    public sealed class XboxArtistImageProvider : IArtistImageProvider
    {
        /// <summary>
        /// XBOX Client ID.
        /// </summary>
        public static string ClientId { get; set; }

        /// <summary>
        /// XBOX Client secret.
        /// </summary>
        public static string ClientSecret { get; set; }

        /// <summary>
        /// Query image.
        /// </summary>
        /// <param name="term">Serach term.</param>
        /// <param name="desiredSize">Desired size.</param>
        /// <returns>Image url.</returns>
        public async Task<string> QueryImageUrl(string term, ArtistImageSize? desiredSize = null)
        {
            using (var client = XboxMusicClientFactory.CreateXboxMusicClient(ClientId, ClientSecret))
            {
                var result = await client.SearchAsync(Namespace.music, term, ContentSource.Catalog, SearchFilter.Artists);
                var url = result?.Artists?.Items?[0]?.ImageUrl;
                return url;
            }
        }
    }
}