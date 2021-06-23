using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data
{
    public class RecipeData
    {
        private int _dishID;
        public int DishID { get => _dishID; set => _dishID = value; }
        private int _recipeID;
        public int RecipeID { get => _recipeID; set => _recipeID = value; }
        private string _recipeName;
        public string RecipeName { get => _recipeName; set => _recipeName = value; }
        private string _recipeDescription;
        public string RecipeDescription { get => _recipeDescription; set => _recipeDescription = value; }
    }
}
