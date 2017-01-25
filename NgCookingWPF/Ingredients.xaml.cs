using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace NGCookingWPF
{
    /// <summary>
    /// Logique d'interaction pour Ingredients.xaml
    /// </summary>
    public partial class Ingredients : Page
    {
        public string NameToLookUp = "";
        public string CategoryToLookUp = "";
        public int CalMinToLookUp = 0;
        public int CalMaxToLookUp = 0;

        List<apis.Client.Models.Ingredient> allIng; // Contains ALL ingredients

        public Ingredients()
        {
            InitializeComponent();

            apis.Client.ApiClient _apiClient = _apiClient = new apis.Client.ApiClient("http://localhost:5000/api");
            List<apis.Client.Models.Ingredient> src = _apiClient.Get<List<apis.Client.Models.Ingredient>>("ingredients").Result;
            src = src.OrderBy(o => o.Name).ToList();
            allIng = src; // Reference for the sort fct

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

            NameFilter.TextChanged += NameTextChangedHandler;
            CategoryFilter.TextChanged += CategoryTextChangedHandler;
            CaloriesMin.TextChanged += CaloriesMinTextChangedHandler;
            CaloriesMax.TextChanged += CaloriesMaxTextChangedHandler;

            Content.ItemsSource = src;
        }
        private void NameTextChangedHandler(object sender, TextChangedEventArgs e)
        {
            NameToLookUp = NameFilter.Text;
            IngredientSort();
        }

        private void CategoryTextChangedHandler(object sender, TextChangedEventArgs e)
        {
            CategoryToLookUp = CategoryFilter.Text;
            IngredientSort();
        }

        private void CaloriesMinTextChangedHandler(object sender, TextChangedEventArgs e)
        {
            // TODO : Add more security
            if (CaloriesMin.Text != "")
            {
                CalMinToLookUp = Convert.ToInt32(CaloriesMin.Text);
                IngredientSort();
            }
        }

        private void CaloriesMaxTextChangedHandler(object sender, TextChangedEventArgs e)
        {
            // TODO : Add more security
            if (CaloriesMax.Text != "")
            {
                CalMinToLookUp = Convert.ToInt32(CaloriesMax.Text);
                IngredientSort();
            }
        }

        private void IngredientSort()
        {
            var src = new List<apis.Client.Models.Ingredient>();

            foreach (var item in allIng)
            {
                if (NameToLookUp != null)
                {
                    if (item.Name.ToLower().Contains(NameToLookUp.ToLower()))
                    {
                        src.Add(item);
                    }
                }

                //if (CategoryToLookUp != null)
                //{
                //    if (item.CategoryId.ToLower().Contains(CategoryToLookUp.ToLower()))
                //    {
                //        src.Add(item);
                //    }
                //}

                //if (CalMinToLookUp >= 0 && CalMaxToLookUp >= 0 && CalMinToLookUp < CalMaxToLookUp)
                //{
                //    if (CalMinToLookUp <= item.Calories && item.Calories <= CalMaxToLookUp)
                //    {
                //        src.Add(item);
                //    }
                //}
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
        #endregion
    }
}