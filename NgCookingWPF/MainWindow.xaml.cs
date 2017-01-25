using System.Collections.Generic;
using System.Windows;

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
            List<Recettes> src = new List<Recettes>();
            src.Add(new Recettes { Name = "Tajine de poulet", Mark = 3, Image = "pack://application:,,,/img/recettes/tajine-de-poulet.jpg" });
            src.Add(new Recettes { Name = "Saumon echalotte", Mark = 3, Image = "pack://application:,,,/img/recettes/saumon-echalotte.jpg" });
            src.Add(new Recettes { Name = "Salade de fruit granité", Mark = 3, Image = "pack://application:,,,/img/recettes/salade-de-fruit-granite.jpg" });
            src.Add(new Recettes { Name = "Cake jambon olive", Mark = 3, Image = "pack://application:,,,/img/recettes/cake-jambon-olive.jpg" });
            BestRecipes.ItemsSource = src;
            NewRecipes.ItemsSource = src;
        }
    }
}
