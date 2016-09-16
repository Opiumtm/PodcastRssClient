using System.ComponentModel;

namespace Ipatov.PodcastRssClient.Catalog
{
    /// <summary>
    /// Additional search result properties for incremental load.
    /// </summary>
    public interface IPodcastSearchResultStatus : INotifyPropertyChanged
    {
        /// <summary>
        /// Incremental loading in progress.
        /// </summary>
        bool IsProgress { get; }

        /// <summary>
        /// Incremental loading error.
        /// </summary>
        bool IsError { get; }

        /// <summary>
        /// Error message.
        /// </summary>
        string Message { get; }
    }
}