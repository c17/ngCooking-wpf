using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace NgCookingWPF
{
    /// <summary>
    /// Logique d'interaction pour UserProfile.xaml
    /// </summary>
    public partial class UserProfile : Page
    {
        string[] levels = { "Novice", "Intermediaire", "Expert" };
        private string _Id;
        public UserProfile()
        {
            InitializeComponent();
        }
        public UserProfile(string Id)
        {
            _Id = Id;
            InitializeComponent();
            apis.Client.ApiClient _apiClient = _apiClient = new apis.Client.ApiClient("http://localhost:5000/api");

            apis.Client.Models.User src = _apiClient.Get<apis.Client.Models.User>(String.Format("{0}{1}", "community/", Id)).Result;
            List<apis.Client.Models.Recette> recipes = _apiClient.Get<List<apis.Client.Models.Recette>>("recettes").Result;

            List<apis.Client.Models.Recette> userRecipes = new List<apis.Client.Models.Recette>();

            foreach(var item in recipes)
            {
                if (item.CreatorId == src.Id)
                {
                    userRecipes.Add(item);
                }
            }
            foreach (var item in userRecipes)
            {
                String u = String.Format("{0}/{1}", "http://localhost:5000/", item.Picture);
                var bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri(u);
                bi.EndInit();
                item.Img = bi;
            }
            UserRecipes.ItemsSource = userRecipes;
            String url = String.Format("{0}/{1}", "http://localhost:5000/", src.Picture);
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(url); ;
            bitmapImage.EndInit();
            src.Img = bitmapImage;
            Name.Text = String.Format("{0} {1}", src.FirstName, src.LastName);
            ProfileImg.Source = src.Img;
            Level.Text = levels[src.Level - 1];
            City.Text = src.City;
            Age.Text = (DateTime.Now.Year - src.BirthYear).ToString();
            Bio.Text = src.Bio;

        }
        private void RecetteGrid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            string url = String.Format("{0}{1}", "Communaute.xaml?parameter=", e.Source.ToString());
            NavigationService navService = NavigationService.GetNavigationService(this);
            navService.Navigate(new Communaute(((Grid)sender).Name));
        }
    }
}
