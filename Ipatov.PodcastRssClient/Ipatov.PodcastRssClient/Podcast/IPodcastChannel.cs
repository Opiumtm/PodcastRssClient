using System;
using System.Collections.Generic;

namespace Ipatov.PodcastRssClient.Podcast
{
    /// <summary>
    /// Podcast channel.
    /// </summary>
    public interface IPodcastChannel
    {
        /// <summary>
        /// Channel uri.
        /// </summary>
        string ChannelUri { get; }

        /// <summary>
        /// Podcast channel link.
        /// </summary>
        string Link { get; }
        
        /// <summary>
        /// Channel description.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Copyright.
        /// </summary>
        string Copyright { get; }

        /// <summary>
        /// Channel last build date.
        /// </summary>
        DateTime? LastBuildDate { get; }

        /// <summary>
        /// Publish date.
        /// </summary>
        DateTime? PubDate { get; }

        /// <summary>
        /// Channel category.
        /// </summary>
        string Category { get; }

        /// <summary>
        /// Author.
        /// </summary>
        string Author { get; }

        /// <summary>
        /// Summary.
        /// </summary>
        string Summary { get; }

        /// <summary>
        /// Subtitle.
        /// </summary>
        string Subtitle { get; }

        /// <summary>
        /// Podcast channel owner.
        /// </summary>
        IPodcastChannelOwner Onwer { get; }

        /// <summary>
        /// Channel image.
        /// </summary>
        string ChannelImageUrl { get; }

        /// <summary>
        /// Channel image.
        /// </summary>
        IPodcastChannelImage ChannelImage { get; }

        /// <summary>
        /// Episodes.
        /// </summary>
        ICollection<IPodcastEpisode> Episodes { get; }

        /// <summary>
        /// Keywords.
        /// </summary>
        ICollection<string> Keywords { get; }
    }
}