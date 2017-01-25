using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace apis.Client.Models
{
    public class Ingredient
    {
        [JsonProperty("id")]
        public String Id { get; set; }

        [JsonProperty("name")]
        public String Name { get; set; }

        [JsonProperty("isAvailable")]
        public Boolean IsAvailable { get; set; }

        [JsonProperty("picture")]
        public String Picture { get; set; }

        [JsonProperty("category")]
        public String CategoryId { get; set; }

        [JsonIgnore]
        public Categorie Category { get; set; }

        [JsonProperty("calories")]
        public Int32 Calories { get; set; }

        [JsonIgnore]
        public ICollection<IngredientRecette> IngredientsRecettes { get; set; }
        public BitmapImage Img { get; set; }
        public List<BitmapImage> SimiIng { get; set; }
    }
}
