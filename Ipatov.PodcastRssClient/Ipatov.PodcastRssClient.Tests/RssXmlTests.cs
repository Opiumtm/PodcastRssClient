using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Windows.Storage;
using Ipatov.PodcastRssClient.Podcast;
using Ipatov.PodcastRssClient.Podcast.Internal;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Ipatov.PodcastRssClient.Tests
{
    [TestClass]
    public class RssXmlTests
    {
        private const string AboveAndBeyondXml = "abovebeyond.xml";
        private const string EsessionsXml = "esessions.xml";
        private const string FraneticXml = "franetic.xml";

        private Uri GetTestXmlUri(string name)
        {
            return new Uri($"ms-appx:///Resources/{name}");
        }

        private async Task<IPodcastChannel> LoadChannel(string name)
        {
            StorageFile f = await StorageFile.GetFileFromApplicationUriAsync(GetTestXmlUri(name));
            var text = await FileIO.ReadTextAsync(f);
            return PodcastChannelFactory.Create(text);
        }

        [TestMethod]
        public async Task SuccessfulParsing()
        {
            var channel = await LoadChannel(AboveAndBeyondXml);
            Assert.IsNotNull(channel);
        }

        [TestMethod]
        public async Task BasicChannelProps()
        {
            var agbt = await LoadChannel(AboveAndBeyondXml);
            Assert.AreEqual(agbt.Link, "http://static.aboveandbeyond.nu/grouptherapy/podcast.xml");
            Assert.AreEqual(agbt.ChannelUri, "http://static.aboveandbeyond.nu/grouptherapy/podcast.xml");
            Assert.AreEqual(agbt.ChannelImageUrl, "http://static.aboveandbeyond.nu/assets/logos/Group_Therapy_Podcast_600x600.jpg");
            Assert.IsNotNull(agbt.ChannelImage);
            Assert.AreEqual(agbt.ChannelImage.ImageUrl, "http://static.aboveandbeyond.nu/assets/logos/Group_Therapy_Podcast_600x600.jpg");
            Assert.AreEqual(agbt.ChannelImage.Title, null);
            Assert.AreEqual(agbt.ChannelImage.Link, null);
            Assert.AreEqual(agbt.ChannelImage.Height, null);
            Assert.AreEqual(agbt.ChannelImage.Width, null);
            Assert.IsNotNull(agbt.LastBuildDate);
            Assert.AreEqual(agbt.LastBuildDate.Value.Year, 2016);
            Assert.AreEqual(agbt.LastBuildDate.Value.Month, 9);
            Assert.AreEqual(agbt.LastBuildDate.Value.Day, 19);
            Assert.AreEqual(agbt.Category, "Music");
            Assert.AreEqual(agbt.Author, "Above & Beyond");
            Assert.AreEqual(agbt.Summary, "Group Therapy is the weekly radio show from Above & Beyond also known as ABGT");
            Assert.IsNotNull(agbt.Onwer);
            Assert.AreEqual(agbt.Onwer.Name, "Above & Beyond");
            Assert.AreEqual(agbt.Onwer.Email, "contact@aboveandbeyond.nu");
            Assert.IsNotNull(agbt.Keywords);
            Assert.AreEqual(agbt.Keywords.Count, 4);
            CollectionAssert.Contains((ICollection)agbt.Keywords, "anjunabeats");
            Assert.IsNotNull(agbt.Episodes);
            Assert.IsTrue(agbt.Episodes.Count > 0);
        }

        [TestMethod]
        public async Task EpisodeProps()
        {
            var agbt = await LoadChannel(AboveAndBeyondXml);
            var episode = agbt.Episodes.FirstOrDefault(e => e.Order == 0);
            Assert.IsNotNull(episode);
            Assert.IsNotNull(episode.Title);
            Assert.IsNotNull(episode.DescriptionHtml);
            Assert.IsNotNull(episode.Author);
            Assert.IsNotNull(episode.Guid);
            Assert.AreEqual(episode.Guid, "abgt199");
            Assert.IsNotNull(episode.PubDate);
            Assert.AreEqual(episode.PubDate.Value.Year, 2016);
            Assert.IsNotNull(episode.Enclosure);
            Assert.AreEqual(episode.Enclosure.Url, "http://traffic.libsyn.com/anjunabeats/ABGT199_LiveShow160916.m4a");
            Assert.AreEqual(episode.Enclosure.Length, 115205480ul);
            Assert.AreEqual(episode.Enclosure.ContentType, "audio/mpeg");
            Assert.IsNotNull(episode.Duration);
            Assert.AreEqual(episode.Duration, new TimeSpan(2, 0, 0));

            foreach (var e in agbt.Episodes)
            {
                Assert.IsNotNull(e.Enclosure);
                Assert.IsNotNull(e.Enclosure.Url);
                Assert.IsTrue(e.Enclosure.Url.StartsWith("http://"));
            }
        }

        [TestMethod]
        public async Task AllEpisodesLoaded()
        {
            var esessions = await LoadChannel(EsessionsXml);
            var franetic = await LoadChannel(FraneticXml);
            var agbt = await LoadChannel(AboveAndBeyondXml);
            Assert.IsNotNull(agbt);
            Assert.IsNotNull(esessions);
            Assert.IsNotNull(franetic);
            Assert.IsNotNull(esessions.Episodes);
            Assert.IsTrue(esessions.Episodes.Count > 0);
            Assert.IsNotNull(franetic.Episodes);
            Assert.IsTrue(franetic.Episodes.Count > 0);
            Assert.IsNotNull(agbt.Episodes);
            Assert.IsTrue(agbt.Episodes.Count > 0);
        }

        [TestMethod]
        public async Task EpisodeImageUrl()
        {
            var franetic = await LoadChannel(FraneticXml);
            var episode = franetic.Episodes.FirstOrDefault(e => e.Order == 0);
            Assert.IsNotNull(episode);
            Assert.IsNotNull(episode.ImageUrl);
        }

        [TestMethod]
        public void TestDateParse()
        {
            var ds1 = "Fri, 11 Nov 2016 14:36:11 GMT";
            var ds2 = "Wed, 07 Dec 2016 00:00:00 EDT";
            var dt1 = RssValuesHelper.ConvertDateTime(ds1);
            var dt2 = RssValuesHelper.ConvertDateTime(ds2);
            Assert.IsNotNull(dt1, $"Fail to parse {ds1}");
            Assert.IsNotNull(dt2, $"Fail to parse {ds2}");
        }
    }
}