using Common.Data;
using BusinessRule;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessFacade
{
    public class RecipeSystem
    {
        public List<RecipeData> GetRecipeList()
        {
            try
            {
                return new RecipeDB().GetRecipeList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<RecipeData> GetRecipeListByDishID(int DishID)
        {
            try
            {
                return new RecipeDB().GetRecipeListByDishID(DishID);
            }
            catch(Exception)
            {
                throw;
            }
        }
        public RecipeData GetRecipeByRecipeID(int RecipeID)
        {
            try
            {
                return new RecipeDB().GetRecipeByRecipeID(RecipeID);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int InsertUpdateRecipe(RecipeData Recipe)
        {
            try
            {
                return new RecipeRule().InsertUpdateRecipe(Recipe);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int DeleteRecipes(IEnumerable<int> RecipeIDs)
        {
            try
            {
                return new RecipeRule().DeleteRecipes(RecipeIDs);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
