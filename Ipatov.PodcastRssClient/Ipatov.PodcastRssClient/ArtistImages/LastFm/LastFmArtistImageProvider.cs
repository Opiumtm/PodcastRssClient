using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Web.Http;

namespace Ipatov.PodcastRssClient.ArtistImages.LastFm
{
    /// <summary>
    /// LastFM artist image provider.
    /// </summary>
    public sealed class LastFmArtistImageProvider : IArtistImageProvider
    {
        private const string UriFormat = "http://ws.audioscrobbler.com/2.0/?method=artist.getinfo&artist={0}&api_key={1}";

        /// <summary>
        /// API key.
        /// </summary>
        public static string ApiKey { get; set; }

        public async Task<string> QueryImageUrl(string term, ArtistImageSize? desiredSize = null)
        {
            var uriStr = string.Format(UriFormat, WebUtility.UrlEncode(term), ApiKey);
            var uri = new Uri(uriStr);
            using (var client = new HttpClient())
            {
                var str = await client.GetStringAsync(uri);
                var xml = XDocument.Parse(str);
                if (!"ok".Equals(xml.Root.Attribute("status").Value, StringComparison.OrdinalIgnoreCase))
                {
                    throw new WebException("Invalid LastFM status code");
                }
                var el = xml.Root.Element("artist");
                if (el == null) throw new WebException("LastFM xml error");
                var images = el.Elements("image").Where(e => e.Attribute("size") != null).ToArray();
                XElement image = null;
                if (desiredSize != null)
                {
                    image = images.Where(e => GetSizePriority(el) <= GetSizePriority(desiredSize.Value)).OrderBy(GetSizePriority).FirstOrDefault();
                }
                if (image == null)
                {
                    image = images.Where(e => e.Attribute("size") != null).OrderBy(GetSizePriority).FirstOrDefault();
                }
                return image?.Value;
            }
        }

        private static int GetSizePriority(XElement el)
        {
            var a = el.Attribute("size");
            if (a == null) return 999;
            var v = a.Value;
            if (ImageSizes.ContainsKey(v))
            {
                return ImageSizes[v];
            }
            return 999;
        }

        private static int GetSizePriority(ArtistImageSize size)
        {
            if (ImageSizes2.ContainsKey(size))
            {
                return ImageSizes2[size];
            }
            return 999;
        }

        private static readonly Dictionary<string, int> ImageSizes = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            {"mega", 0},
            {"extralarge", 1},
            {"large", 2},
            {"medium", 3},
            {"small", 4},
        };

        private static readonly Dictionary<ArtistImageSize, int> ImageSizes2 = new Dictionary<ArtistImageSize, int>(StringComparer.OrdinalIgnoreCase)
        {
            {ArtistImageSize.Mega, 0},
            {ArtistImageSize.ExtraLarge, 1},
            {ArtistImageSize.Large, 2},
            {ArtistImageSize.Medium, 3},
            {ArtistImageSize.Small, 4},
        };

        private static readonly Dictionary<string, ArtistImageSize> ImageSizesNames = new Dictionary<string, ArtistImageSize>(StringComparer.OrdinalIgnoreCase)
        {
            {"mega", ArtistImageSize.Mega},
            {"extralarge", ArtistImageSize.ExtraLarge},
            {"large", ArtistImageSize.Large},
            {"medium", ArtistImageSize.Medium},
            {"small", ArtistImageSize.Small},
        };
    }
}