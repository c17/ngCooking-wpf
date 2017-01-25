using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace NGCookingWPF
{
    /// <summary>
    /// Logique d'interaction pour Ingredients.xaml
    /// </summary>
    public partial class Ingredients : Page
    {
        // DEBUG
        //public class Ingredient
        //{
        //    public string Name { get; set; }
        //    public string Category { get; set; }
        //    public string ImgPath { get; set; }
        //    public string Calories { get; set; }
        //    public List<string> SimiIng { get; set; }

        //    public Ingredient(string name, string cat, string img, int cal)
        //    {
        //        Name = name;
        //        Category = cat;
        //        ImgPath = img;
        //        Calories = cal.ToString() + " kcal";
        //        SimiIng = new List<string>();
        //    }
        //}

        public Ingredients()
        {
            InitializeComponent();

            apis.Client.ApiClient _apiClient = _apiClient = new apis.Client.ApiClient("http://localhost:5000/api");
            List<apis.Client.Models.Ingredient> src = _apiClient.Get<List<apis.Client.Models.Ingredient>>("ingredients").Result;
            src = src.OrderBy(o => o.Name).ToList();

            foreach (var item in src)
            {
                String url = String.Format("{0}/img/ingredients/{1}", "http://localhost:5000/", item.Picture);
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(url); ;
                bitmapImage.EndInit();
                item.Img = bitmapImage;

            }

            foreach (var item in src)
            {
                item.SimiIng = new List<BitmapImage>();
                foreach (var ingredients in src)
                {
                    if (item.CategoryId == ingredients.CategoryId)
                    {
                        item.SimiIng.Add(ingredients.Img);
                    }
                }
            }

            NameFilter.LostFocus += TargetUpdatedHandler;
            CategoryFilter.LostFocus += TargetUpdatedHandler;
            CaloriesMin.LostFocus += TargetUpdatedHandler;
            CaloriesMax.LostFocus += TargetUpdatedHandler;

            Content.ItemsSource = src;
        }

        private void TargetUpdatedHandler(object sender, RoutedEventArgs e)
        {
            // DEBUG
            //MessageBox.Show("Modification de " + sender);
        }

        #region UIElements
        private void NameFilter_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CategoryFilter_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CaloriesMin_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CaloriesMax_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        #endregion
    }
}
