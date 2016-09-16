using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel.Channels;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Foundation;
using Windows.UI.Xaml.Data;
using Windows.Web.Http;

namespace Ipatov.PodcastRssClient.Catalog.DigitalPodcasts.Internal
{
    /// <summary>
    /// OPML catalog search result.
    /// </summary>
    public sealed class OpmlSearchResult : ObservableCollection<IPodcastCatalogEntry>, ISupportIncrementalLoading, IPodcastSearchResultStatus
    {
        private readonly XmlSerializer _serializer = new XmlSerializer(typeof(OPML));

        private readonly DigitalPodcastsSearchQuery _arg;

        private OPML _lastResult;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="arg">Argument.</param>
        public OpmlSearchResult(DigitalPodcastsSearchQuery arg)
        {
            this._arg = arg;
        }

        /// <summary>
        /// Initializes incremental loading from the view.
        /// </summary>
        /// <returns>
        /// The wrapped results of the load operation.
        /// </returns>
        /// <param name="count">The number of items to load.</param>
        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            return AsyncInfo.Run(cts => Load(cts, count));
        }

        private async Task<LoadMoreItemsResult> Load(CancellationToken token, uint count)
        {
            IsProgress = true;
            try
            {
                var start = (uint)(_lastResult != null ? _lastResult.head.startIndex + _lastResult.body.Length : 0);
                var uri = new Uri(_arg.ToUrl(50, start), UriKind.Absolute);
                using (var client = new HttpClient())
                {
                    var str = await client.GetStringAsync(uri).AsTask(token);
                    using (var rd = new StringReader(str))
                    {
                        var res = ((OPML)_serializer.Deserialize(rd));
                        var data =
                            res.body.Select(
                                o =>
                                    new DigitalPodcastSearchResult(o)).ToArray();
                        foreach (var d in data)
                        {
                            Add(d);
                        }
                        _lastResult = res;
                        return new LoadMoreItemsResult() { Count = (uint)data.Length };
                    }
                }
            }
            catch (Exception ex)
            {
                IsError = true;
                Message = ex.Message;
                return new LoadMoreItemsResult() { Count = 0 };
            }
            finally
            {
                IsProgress = false;
            }
        }

        /// <summary>
        /// Gets a sentinel value that supports incremental loading implementations.
        /// </summary>
        /// <returns>
        /// true if additional unloaded items remain in the view; otherwise, false.
        /// </returns>
        public bool HasMoreItems
        {
            get
            {
                if (IsError)
                {
                    return false;
                }
                if (_lastResult == null)
                {
                    return true;
                }
                return (_lastResult.head.startIndex + _lastResult.body.Length) < _lastResult.head.totalResults;
            }
        }

        private bool _isProgress;

        /// <summary>
        /// Incremental loading in progress.
        /// </summary>
        public bool IsProgress
        {
            get { return _isProgress; }
            private set
            {
                _isProgress = value;
                base.OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsProgress)));
            }
        }

        private string _message;

        /// <summary>
        /// Error message.
        /// </summary>
        public string Message
        {
            get { return _message; }
            private set
            {
                _message = Message;
                base.OnPropertyChanged(new PropertyChangedEventArgs(nameof(Message)));
            }
        }

        private bool _isError;

        /// <summary>
        /// Incremental loading error.
        /// </summary>
        public bool IsError
        {
            get { return _isError; }
            private set
            {
                _isError = value;
                base.OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsError)));
            }
        }
    }
}