using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace NGCookingWPF
{
    /// <summary>
    /// Logique d'interaction pour Recettes.xaml
    /// </summary>
    public partial class Recettes : Page
    {
        public string NameToLookUp = "";
        public string CategoryToLookUp = "";
        public int CalMinToLookUp = 0;
        public int CalMaxToLookUp = 0;
        public string SelectedSort = "";

        public Recettes()
        {
            InitializeComponent();

            NameFilter.TextChanged += NameTextChangedHandler;
            CategoryFilter.TextChanged += CategoryTextChangedHandler;
            CaloriesMin.TextChanged += CaloriesMinTextChangedHandler;
            CaloriesMax.TextChanged += CaloriesMaxTextChangedHandler;
            comboBox.SelectionChanged += ComboBoxSelectionChangedHandler;
        }

        private void NameTextChangedHandler(object sender, TextChangedEventArgs e)
        {
            NameToLookUp = NameFilter.Text;
            FindRecipe();
        }

        private void CategoryTextChangedHandler(object sender, TextChangedEventArgs e)
        {
            CategoryToLookUp = CategoryFilter.Text;
            FindRecipe();
        }

        private void CaloriesMinTextChangedHandler(object sender, TextChangedEventArgs e)
        {
            string strToEvaluate = CaloriesMin.Text;
            if (strToEvaluate != "" && strToEvaluate.Length < 5)
            {
                bool isNumeric = int.TryParse(strToEvaluate, out CalMinToLookUp);
                if (isNumeric && CalMinToLookUp > 0)
                {
                    FindRecipe();
                }
            }
        }

        private void CaloriesMaxTextChangedHandler(object sender, TextChangedEventArgs e)
        {
            string strToEvaluate = CaloriesMax.Text;
            if (strToEvaluate != "" && strToEvaluate.Length < 5)
            {
                bool isNumeric = int.TryParse(strToEvaluate, out CalMaxToLookUp);
                if (isNumeric && CalMaxToLookUp > 0)
                {
                    FindRecipe();
                }
            }
        }

        private void ComboBoxSelectionChangedHandler(object sender, SelectionChangedEventArgs e)
        {
            SelectedSort = (((sender as ComboBox).SelectedItem as ComboBoxItem).Content as string);
            FindRecipe();
        }

        private void FindRecipe()
        {
            apis.Client.ApiClient _apiClient = _apiClient = new apis.Client.ApiClient("http://localhost:5000/api");
            List<apis.Client.Models.Recette> allRec = _apiClient.Get<List<apis.Client.Models.Recette>>("recettes").Result;
            allRec = allRec.OrderBy(o => o.Name).ToList();

            foreach (var item in allRec)
            {
                String url = String.Format("{0}/{1}", "http://localhost:5000", item.Picture);
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(url); ;
                bitmapImage.EndInit();
                item.Img = bitmapImage;
            }

            var src = new List<apis.Client.Models.Recette>();
            bool flag;

            foreach (var item in allRec)
            {
                flag = true;

                if (NameToLookUp != null)
                {
                    flag = false;
                    if (item.Name.ToLower().Contains(NameToLookUp.ToLower()))
                    {
                        flag = true;
                    }
                }

                if (flag && CategoryToLookUp != null)
                {
                    flag = false;
                    if (item.Id.ToLower().Contains(CategoryToLookUp.ToLower()))
                    {
                        flag = true;
                    }
                }

                if (flag && CalMinToLookUp < CalMaxToLookUp)
                {
                    flag = false;
                    if (CalMinToLookUp <= item.Calories && item.Calories <= CalMaxToLookUp)
                    {
                        flag = true;
                    }
                }

                if (flag)
                {
                    src.Add(item);
                }
            }

            Content.ItemsSource = src;
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

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        #endregion
    }
}