using System.Threading.Tasks;

namespace Ipatov.PodcastRssClient.ArtistImages
{
    /// <summary>
    /// Artist image provider.
    /// </summary>
    public interface IArtistImageProvider
    {
        /// <summary>
        /// Query image.
        /// </summary>
        /// <param name="term">Serach term.</param>
        /// <param name="desiredSize">Desired size.</param>
        /// <returns>Image url.</returns>
        Task<string> QueryImageUrl(string term, ArtistImageSize? desiredSize = null);
    }
}