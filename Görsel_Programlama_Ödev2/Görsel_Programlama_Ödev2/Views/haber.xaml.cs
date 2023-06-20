using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace Görsel_Programlama_Ödev2.Views
{
    public class FeedItem
    {
        public string Title { get; set; }
        public string PubDate { get; set; }
        public ImageSource ImageSource { get; set; }
    }

    public partial class haber : ContentPage, INotifyPropertyChanged
    {
        private List<FeedItem> _feedItems;
        public List<FeedItem> FeedItems
        {
            get { return _feedItems; }
            set
            {
                _feedItems = value;
                OnPropertyChanged(nameof(FeedItems));
            }
        }

        public haber()
        {
            InitializeComponent();
            BindingContext = this;

            LoadFeedItems();
        }

        private async void LoadFeedItems()
        {
            var rssUrl = "https://www.trthaber.com/manset_articles.rss";

            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(rssUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var items = ParseRssContent(content);
                        FeedItems = items;
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", $"RSS okunurken bir hata oluştu: {ex.Message}", "Tamam");
            }
        }

        private List<FeedItem> ParseRssContent(string content)
        {
            XDocument doc = XDocument.Parse(content);
            XNamespace ns = "http://www.w3.org/2005/Atom";

            var items = doc.Descendants("item").Select(x =>
            {
                var item = new FeedItem
                {
                    Title = x.Element("title")?.Value,
                    PubDate = x.Element("pubDate")?.Value
                };

                var imageUrl = x.Element("enclosure")?.Attribute("url")?.Value;
                if (!string.IsNullOrEmpty(imageUrl))
                {
                    item.ImageSource = ImageSource.FromUri(new Uri(imageUrl));
                }

                return item;
            }).ToList();

            return items;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
