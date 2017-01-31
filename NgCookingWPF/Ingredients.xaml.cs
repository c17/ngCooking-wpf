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
                String url = String.Format("{0}/img/ingredients/{1}", "http://localhost:5000", item.Picture);
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
            string strToEvaluate = CaloriesMin.Text;
            if (strToEvaluate != "" && strToEvaluate.Length < 5)
            {
                bool isNumeric = int.TryParse(strToEvaluate, out CalMinToLookUp);
                if (isNumeric && CalMinToLookUp > 0)
                {
                    IngredientSort();
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
                    IngredientSort();
                }
            }
        }

        private void IngredientSort()
        {
            var src = new List<apis.Client.Models.Ingredient>();
            bool flag;

            foreach (var item in allIng)
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
                    if (item.CategoryId.ToLower().Contains(CategoryToLookUp.ToLower()))
                    {
                        flag = true;
                    }
                }

                if (flag && CalMinToLookUp >= 0 && CalMaxToLookUp >= 0 && CalMinToLookUp < CalMaxToLookUp)
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
        #endregion
    }
}