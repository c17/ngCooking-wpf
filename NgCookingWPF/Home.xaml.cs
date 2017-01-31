using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace NgCookingWPF
{
    /// <summary>
    /// Logique d'interaction pour Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        apis.Client.ApiClient _apiClient;
        apis.Client.Models.User _connectUser;
        public Home()
        {
            InitializeComponent();
            _apiClient = _apiClient = new apis.Client.ApiClient("http://localhost:5000/api");
            List<apis.Client.Models.Recette> src = _apiClient.Get<List<apis.Client.Models.Recette>>("recettes").Result;

            src = src.OrderBy(o => o.Name).ToList();
            foreach (var item in src)
            {
                String url = String.Format("{0}/{1}", "http://localhost:5000", item.Picture);
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(url); ;
                bitmapImage.EndInit();
                item.Img = bitmapImage;
            }
            
            BestRecipes.ItemsSource = src;
            NewRecipes.ItemsSource = src;

            ConnexionButton.Click += connexionClick;
            ValidateButton.Click += validateClick;
            DeconnexionButton.Click += deconnexionClick;
            ProfileButton.Click += profileConnexion;
        }
        private void validateClick(object sender, RoutedEventArgs e)
        {
            List<apis.Client.Models.User> users = _apiClient.Get<List<apis.Client.Models.User>>("community").Result;
            HttpResponseMessage res = _apiClient.Post<LogginUser>("Authenticate", new LogginUser { email = UserName.Text, password = Password.Password }).Result;
            if (res.StatusCode == System.Net.HttpStatusCode.OK)
            {
                DeconnexionButton.Visibility = Visibility.Visible;
                ProfileButton.Visibility = Visibility.Visible;
                ConnexionButton.Visibility = Visibility.Hidden;
            }
            Form.IsOpen = false;
        }
        private void profileConnexion(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            navService.Navigate(new UserProfile(_connectUser));
        }

        private void deconnexionClick(object sender, RoutedEventArgs e)
        {
            _connectUser = null;
            DeconnexionButton.Visibility = Visibility.Hidden;
            ProfileButton.Visibility = Visibility.Hidden;
            ConnexionButton.Visibility = Visibility.Visible;
        }

        private void RecetteGrid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
        private void connexionClick(object sender, RoutedEventArgs e)
        {
            Form.IsOpen = true;
        }

    }
}
