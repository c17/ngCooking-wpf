﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace NgCookingWPF
{
    /// <summary>
    /// Logique d'interaction pour Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();
            apis.Client.ApiClient _apiClient = _apiClient = new apis.Client.ApiClient("http://localhost:5000/api");

            List<apis.Client.Models.Recette> src = _apiClient.Get<List<apis.Client.Models.Recette>>("recettes").Result;
            src = src.OrderBy(o => o.Name).ToList();
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
