using System;
using System.Collections.Generic;

namespace Ipatov.PodcastRssClient.Podcast
{
    /// <summary>
    /// Podcast episode.
    /// </summary>
    public interface IPodcastEpisode
    {
        /// <summary>
        /// Order in feed (0 - most new).
        /// </summary>
        int Order { get; }

        /// <summary>
        /// Publication date.
        /// </summary>
        DateTime? PubDate { get; }

        /// <summary>
        /// Title.
        /// </summary>
        string Title { get; }         

        /// <summary>
        /// Link.
        /// </summary>
        string Link { get; }

        /// <summary>
        /// Unique id.
        /// </summary>
        string Guid { get; }

        /// <summary>
        /// Description.
        /// </summary>
        string DescriptionHtml { get; }

        /// <summary>
        /// Categories.
        /// </summary>
        ICollection<string> Category { get; }

        /// <summary>
        /// Author.
        /// </summary>
        string Author { get; }

        /// <summary>
        /// Subtitle.
        /// </summary>
        string Subtitle { get; }

        /// <summary>
        /// Summary.
        /// </summary>
        string Summary { get; }

        /// <summary>
        /// Duration.
        /// </summary>
        TimeSpan? Duration { get; }

        /// <summary>
        /// Keywords.
        /// </summary>
        ICollection<string> Keywords { get; }

        /// <summary>
        /// Media enclosure.
        /// </summary>
        IPodcastEpisodeEnclosure Enclosure { get; }

        /// <summary>
        /// Episode image.
        /// </summary>
        string ImageUrl { get; }
    }
}