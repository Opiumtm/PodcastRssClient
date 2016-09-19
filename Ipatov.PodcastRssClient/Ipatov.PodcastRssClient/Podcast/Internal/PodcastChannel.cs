using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

namespace Ipatov.PodcastRssClient.Podcast.Internal
{
    /// <summary>
    /// Podcast channel.
    /// </summary>
    public sealed class PodcastChannel : IPodcastChannel
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="document">XML document.</param>
        /// <param name="originalLink">Original link.</param>
        public PodcastChannel(XDocument document, string originalLink = null)
        {
            if (document?.Root == null) throw new ArgumentNullException(nameof(document));
            var channel = document.Root.Element("channel");
            if (channel == null)
            {
                throw new ArgumentException("Invalid RSS XML");                                
            }
            if (originalLink != null)
            {
                ChannelUri = originalLink;
            }
            else
            {
                var atomLink = channel.Elements(Consts.Namespaces.Atom + "link").FirstOrDefault(e => "self".Equals(e.Attribute("rel")?.Value, StringComparison.OrdinalIgnoreCase) && e.Attribute("href") != null);
                if (atomLink != null)
                {
                    ChannelUri = atomLink.Attribute("href")?.Value;
                }
            }
            Link = channel.Element("link")?.Value;
            Description = channel.Element("description")?.Value;
            Copyright = channel.Element("copyright")?.Value;
            LastBuildDate = RssValuesHelper.ConvertDateTime(channel.Element("lastBuildDate")?.Value);
            PubDate = RssValuesHelper.ConvertDateTime(channel.Element("pubDate")?.Value);
            Category = channel.Element(Consts.Namespaces.Itunes + "category")?.Attribute("text")?.Value ?? channel.Element("category")?.Value;
            Author = channel.Element(Consts.Namespaces.Itunes + "author")?.Value;
            Summary = channel.Element(Consts.Namespaces.Itunes + "summary")?.Value;
            Subtitle = channel.Element(Consts.Namespaces.Itunes + "subtitle")?.Value;
            Onwer = new Owner(channel.Element(Consts.Namespaces.Itunes + "owner"));
            var imageElement = channel.Element("image");
            ChannelImage = imageElement != null ? new ChannelImageObj(imageElement) : new ChannelImageObj(channel.Element(Consts.Namespaces.Itunes + "image")?.Attribute("href")?.Value);
            Keywords = 
                (channel.Element(Consts.Namespaces.Itunes + "keywords")?.Value ?? "").Split(',').Select(k => k.Trim()).Where(k => !string.IsNullOrWhiteSpace(k))
                .Concat((channel.Element(Consts.Namespaces.Itunes + "keyword")?.Value ?? "").Split(',').Select(k => k.Trim()).Where(k => !string.IsNullOrWhiteSpace(k)))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();
            var itemElements = channel.Elements("item").ToArray();
            Episodes = new List<IPodcastEpisode>();
            for (int i = 0; i < itemElements.Length; i++)
            {
                Episodes.Add(new Episode(i, itemElements[i]));
            }
        }

        /// <summary>
        /// Channel uri.
        /// </summary>
        public string ChannelUri { get; }

        /// <summary>
        /// Podcast channel link.
        /// </summary>
        public string Link { get; }

        /// <summary>
        /// Channel description.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Copyright.
        /// </summary>
        public string Copyright { get; }

        /// <summary>
        /// Channel last build date.
        /// </summary>
        public DateTime? LastBuildDate { get; }

        /// <summary>
        /// Publish date.
        /// </summary>
        public DateTime? PubDate { get; }

        /// <summary>
        /// Channel category.
        /// </summary>
        public string Category { get; }

        /// <summary>
        /// Author.
        /// </summary>
        public string Author { get; }

        /// <summary>
        /// Summary.
        /// </summary>
        public string Summary { get; }

        /// <summary>
        /// Subtitle.
        /// </summary>
        public string Subtitle { get; }

        /// <summary>
        /// Podcast channel owner.
        /// </summary>
        public IPodcastChannelOwner Onwer { get; }

        /// <summary>
        /// Channel image.
        /// </summary>
        public string ChannelImageUrl => ChannelImage?.ImageUrl;

        /// <summary>
        /// Channel image.
        /// </summary>
        public IPodcastChannelImage ChannelImage { get; }

        /// <summary>
        /// Episodes.
        /// </summary>
        public ICollection<IPodcastEpisode> Episodes { get; }

        /// <summary>
        /// Keywords.
        /// </summary>
        public ICollection<string> Keywords { get; }

        private sealed class Owner : IPodcastChannelOwner
        {
            public Owner(XElement document)
            {
                if (document != null)
                {
                    Name = document.Element(Consts.Namespaces.Itunes + "name")?.Value;
                    Email = document.Element(Consts.Namespaces.Itunes + "email")?.Value;
                }
            }

            public string Name { get; }

            public string Email { get; }
        }

        private sealed class ChannelImageObj : IPodcastChannelImage
        {
            public ChannelImageObj(string imageUrl)
            {
                ImageUrl = imageUrl;
            }

            public ChannelImageObj(XElement document)
            {
                if (document != null)
                {
                    ImageUrl = document.Element("url")?.Value;
                    Title = document.Element("title")?.Value;
                    Link = document.Element("link")?.Value;
                    Width = RssValuesHelper.ConvertInteger(document.Element("width")?.Value);
                    Height = RssValuesHelper.ConvertInteger(document.Element("height")?.Value);
                }
            }

            public string ImageUrl { get; }
            public string Title { get; }
            public string Link { get; }
            public int? Width { get; }
            public int? Height { get; }
        }

        private sealed class Episode : IPodcastEpisode
        {
            public Episode(int order, XElement document)
            {
                Order = order;
                if (document != null)
                {
                    PubDate = RssValuesHelper.ConvertDateTime(document.Element("pubDate")?.Value);
                    Title = document.Element("title")?.Value;
                    Link = document.Element("link")?.Value;
                    Guid = document.Element("guid")?.Value;
                    DescriptionHtml = document.Element("description")?.Value;
                    Category = document.Elements("category").Select(e => e.Value).ToList();
                    Author = document.Element(Consts.Namespaces.Itunes + "author")?.Value;
                    Subtitle = document.Element(Consts.Namespaces.Itunes + "subtitle")?.Value;
                    Summary = document.Element(Consts.Namespaces.Itunes + "summary")?.Value;
                    Duration = RssValuesHelper.ConvertDuration(document.Element(Consts.Namespaces.Itunes + "duration")?.Value);
                    Keywords = (document.Element(Consts.Namespaces.Itunes + "keywords")?.Value ?? "").Split(',').Select(k => k.Trim()).Where(k => !string.IsNullOrWhiteSpace(k))
                        .Concat((document.Element(Consts.Namespaces.Itunes + "keyword")?.Value ?? "").Split(',').Select(k => k.Trim()).Where(k => !string.IsNullOrWhiteSpace(k)))
                        .Distinct(StringComparer.OrdinalIgnoreCase)
                        .ToList();
                    ImageUrl = document.Element(Consts.Namespaces.Itunes + "image")?.Attribute("href")?.Value;
                    Enclosure = new EnclosureObj(document.Element("enclosure"));
                }
            }

            public int Order { get; }
            public DateTime? PubDate { get; }
            public string Title { get; }
            public string Link { get; }
            public string Guid { get; }
            public string DescriptionHtml { get; }
            public ICollection<string> Category { get; }
            public string Author { get; }
            public string Subtitle { get; }
            public string Summary { get; }
            public TimeSpan? Duration { get; }
            public ICollection<string> Keywords { get; }
            public IPodcastEpisodeEnclosure Enclosure { get; }
            public string ImageUrl { get; }

            private class EnclosureObj : IPodcastEpisodeEnclosure
            {
                public EnclosureObj(XElement document)
                {
                    if (document != null)
                    {
                        Url = document.Attribute("url")?.Value;
                        Length = RssValuesHelper.ConvertUlong(document.Attribute("length")?.Value);
                        ContentType = document.Attribute("type")?.Value;
                    }
                }

                public string Url { get; }
                public ulong? Length { get; }
                public string ContentType { get; }
            }
        }
    }
}