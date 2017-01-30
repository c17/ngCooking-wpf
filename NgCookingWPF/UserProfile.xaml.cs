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
        private apis.Client.Models.User _user;
        private apis.Client.ApiClient _apiClient = new apis.Client.ApiClient("http://localhost:5000/api");
        public UserProfile()
        {
            InitializeComponent();
        }
        public UserProfile(string Id)
        {
            _Id = Id;
            _user = _apiClient.Get<apis.Client.Models.User>(String.Format("{0}{1}", "community/", Id)).Result;
            init();
        }
        public UserProfile(apis.Client.Models.User user)
        {
            _user = user;
            init();
        }

        public void init()
        {
            InitializeComponent();
            List<apis.Client.Models.Recette> recipes = _apiClient.Get<List<apis.Client.Models.Recette>>("recettes").Result;
            List<apis.Client.Models.Recette> userRecipes = new List<apis.Client.Models.Recette>();

            foreach (var item in recipes)
            {
                if (item.CreatorId == _user.Id)
                {
                    userRecipes.Add(item);
                }
            }
            foreach (var item in userRecipes)
            {
                String u = String.Format("{0}/{1}", "http://localhost:5000", item.Picture);
                var bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri(u);
                bi.EndInit();
                item.Img = bi;
            }
            if (userRecipes != null)
                UserRecipes.ItemsSource = userRecipes;
            String url = String.Format("{0}/{1}", "http://localhost:5000", _user.Picture);
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(url); ;
            bitmapImage.EndInit();
            _user.Img = bitmapImage;
            Name.Text = String.Format("{0} {1}", _user.FirstName, _user.LastName);
            ProfileImg.Source = _user.Img;
            Level.Text = levels[_user.Level - 1];
            City.Text = _user.City;
            Age.Text = (DateTime.Now.Year - _user.BirthYear).ToString();
            Bio.Text = _user.Bio;
        }

        private void RecetteGrid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            string url = String.Format("{0}{1}", "Communaute.xaml?parameter=", e.Source.ToString());
            NavigationService navService = NavigationService.GetNavigationService(this);
            navService.Navigate(new Communaute(((Grid)sender).Name));
        }
    }
}
