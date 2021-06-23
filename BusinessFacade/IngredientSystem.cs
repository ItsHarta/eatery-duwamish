using BusinessRule;
using Common.Data;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessFacade
{
    public class IngredientSystem
    {
        public List<IngredientData> GetIngredientListByRecipeID(int RecipeID)
        {
            try
            {
                return new IngredientDB().GetIngredientListByRecipeID(RecipeID);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IngredientData GetIngredientByIngredientID(int IngredientID)
        {
            try
            {
                return new IngredientDB().GetIngredientByIngredientID(IngredientID);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int InsertUpdateIngredient(IngredientData Ingredient)
        {
            try
            {
                return new IngredientRule().InsertUpdateIngredient(Ingredient);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int DeleteIngredients(IEnumerable<int> IngredientIDs)
        {
            try
            {
                return new IngredientRule().DeleteIngredients(IngredientIDs);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
