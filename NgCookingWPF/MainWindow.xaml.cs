using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace NgCookingWPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            apis.Client.ApiClient _apiClient = _apiClient = new apis.Client.ApiClient("http://localhost:5000/api");

            List<apis.Client.Models.Recette> src = _apiClient.Get<List<apis.Client.Models.Recette>>("recettes").Result;
            src = src.OrderBy(o => o.Name).ToList();

            //List<Recettes> src = new List<Recettes>();
            //src.Add(new Recettes { Name = "Tajine de poulet", Mark = 3, Image = "pack://application:,,,/img/recettes/tajine-de-poulet.jpg" });
            //src.Add(new Recettes { Name = "Saumon echalotte", Mark = 3, Image = "pack://application:,,,/img/recettes/saumon-echalotte.jpg" });
            //src.Add(new Recettes { Name = "Salade de fruit granité", Mark = 3, Image = "pack://application:,,,/img/recettes/salade-de-fruit-granite.jpg" });
            //src.Add(new Recettes { Name = "Cake jambon olive", Mark = 3, Image = "pack://application:,,,/img/recettes/cake-jambon-olive.jpg" });
            foreach (var item in src)
            {
                String url = String.Format("{0}/{1}", "http://localhost:5000/", item.Picture);
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(url); ;
                bitmapImage.EndInit();
                item.Img = bitmapImage;
            }
            BestRecipes.ItemsSource = src;
            NewRecipes.ItemsSource = src;
        }
    }
}
