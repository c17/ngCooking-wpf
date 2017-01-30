using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace NgCookingWPF
{
    /// <summary>
    /// Logique d'interaction pour Communaute.xaml
    /// </summary>
    public partial class Communaute : Page
    {
        private string _param { get; set; }
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


        // POUR TESTER AVEC REDIRECTION A LA MAIN
        public Communaute(string param)
        {
            InitializeComponent();
            _param = param;
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
        private void CommunauteGrid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            string url = String.Format("{0}{1}", "Communaute.xaml?parameter=", e.Source.ToString());
            NavigationService navService = NavigationService.GetNavigationService(this);
            navService.Navigate(new UserProfile(((Grid)sender).Uid));
        }
    }
}