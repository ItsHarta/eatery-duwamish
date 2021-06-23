using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data
{
    public class IngredientData
    {
        private int _ingredientID;
        public int IngredientID { get => _ingredientID; set => _ingredientID = value; }
        private int _recipeID;
        public int RecipeID { get => _recipeID; set => _recipeID = value; }
        private string _ingredientName;
        public string IngredientName { get => _ingredientName; set => _ingredientName = value; }
        private int _ingredientQuantity;
        public int IngredientQuantity { get => _ingredientQuantity; set => _ingredientQuantity = value; }
        private string _ingredientUnit;
        public string IngredientUnit { get => _ingredientUnit; set => _ingredientUnit = value; }
    }
}
