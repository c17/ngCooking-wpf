using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NGCookingWPF
{
    /// <summary>
    /// Logique d'interaction pour Ingredients.xaml
    /// </summary>
    public partial class Ingredients : Page
    {
        class Ingredient
        {
            public string Name { get; set; }
            public string Category { get; set; }
            public string ImgPath { get; set; }
            public string Calories { get; set; }
            public List<string> SimiIng { get; set; }

            public Ingredient(string name, string cat, string img, int cal)
            {
                Name = name;
                Category = cat;
                ImgPath = img;
                Calories = cal.ToString() + " kcal";
                SimiIng = new List<string>();
            }
        }

        public Ingredients()
        {
            InitializeComponent();

            Ingredient Ing1 = new Ingredient("Boeuf", "Viande", "/img/ingredients/boeuf.jpg", 310);
            Ingredient Ing2 = new Ingredient("Poulet", "Viande", "/img/ingredients/poulet.jpg", 230);
            Ingredient Ing3 = new Ingredient("Pomme", "Fruit", "/img/ingredients/pomme.jpg", 80);

            Ing1.SimiIng.Add("/img/ingredients/poulet.jpg");
            Ing2.SimiIng.Add("/img/ingredients/boeuf.jpg");
            Ing3.SimiIng.Add("/img/ingredients/poire.jpg");
            Ing3.SimiIng.Add("/img/ingredients/ananas.jpg");
            Ing3.SimiIng.Add("/img/ingredients/kiwi.jpg");

            List<Ingredient> IngList = new List<Ingredient>();

            IngList.Add(Ing1);
            IngList.Add(Ing2);
            IngList.Add(Ing3);

            Content.ItemsSource = IngList;
        }

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
    }
}
