using System;

namespace apis.Client.Models
{
    public class IngredientRecette
    {
        public String IngredientId { get; set; }

        public Ingredient Ingredient { get; set; }

        public String RecetteId { get; set; }

        public Recette Recette { get; set; }
    }
}
