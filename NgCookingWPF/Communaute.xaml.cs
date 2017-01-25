using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace NgCookingWPF
{
    /// <summary>
    /// Logique d'interaction pour Communaute.xaml
    /// </summary>
    public partial class Communaute : Page
    {
        public Communaute()
        {
            InitializeComponent();
            apis.Client.ApiClient _apiClient = _apiClient = new apis.Client.ApiClient("http://localhost:5000/api");

            List<apis.Client.Models.User> src = _apiClient.Get<List<apis.Client.Models.User>>("community").Result;
            src = src.OrderBy(o => o.FirstName).ToList();
            foreach (var item in src)
            {
                String url = String.Format("{0}/{1}", "http://localhost:5000/", item.Picture);
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(url); ;
                bitmapImage.EndInit();
                item.Img = bitmapImage;
            }
            Users.ItemsSource = src;
        }
    }
}