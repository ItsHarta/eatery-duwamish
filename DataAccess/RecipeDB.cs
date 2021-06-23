using Common.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemFramework;

namespace DataAccess
{
    public class RecipeDB
    {
        public List<RecipeData> GetRecipeList()
        {
            try
            {
                string SpName = "dbo.Recipe_Get";
                List<RecipeData> ListRecipe = new List<RecipeData>();
                using (SqlConnection SqlConn = new SqlConnection())
                {
                    SqlConn.ConnectionString = SystemConfigurations.EateryConnectionString;
                    SqlConn.Open();
                    SqlCommand SqlCmd = new SqlCommand(SpName, SqlConn);
                    SqlCmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader Reader = SqlCmd.ExecuteReader())
                    {
                        if (Reader.HasRows)
                        {
                            while (Reader.Read())
                            {
                                RecipeData Recipe = new RecipeData();
                                Recipe.DishID = Convert.ToInt32(Reader["DishID"]);
                                Recipe.RecipeID = Convert.ToInt32(Reader["RecipeID"]);
                                Recipe.RecipeName = Convert.ToString(Reader["RecipeName"]);
                                Recipe.RecipeDescription = Convert.ToString(Reader["RecipeDescription"]);
                                ListRecipe.Add(Recipe);
                            }
                        }
                        SqlConn.Close();
                    }
                    return ListRecipe;
                }
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
                string SpName = "dbo.Recipe_GetByDishID";
                List<RecipeData> ListRecipe = new List<RecipeData>();
                using (SqlConnection SqlConn = new SqlConnection())
                {
                    SqlConn.ConnectionString = SystemConfigurations.EateryConnectionString;
                    SqlConn.Open();
                    SqlCommand SqlCmd = new SqlCommand(SpName, SqlConn);
                    SqlCmd.CommandType = CommandType.StoredProcedure;
                    SqlCmd.Parameters.Add(new SqlParameter("@DishID", DishID));
                    using(SqlDataReader Reader = SqlCmd.ExecuteReader())
                    {
                        if (Reader.HasRows)
                        {
                            while (Reader.Read())
                            {
                                RecipeData Recipe = new RecipeData();
                                Recipe.DishID = Convert.ToInt32(Reader["DishID"]);
                                Recipe.RecipeID = Convert.ToInt32(Reader["RecipeID"]);
                                Recipe.RecipeName = Convert.ToString(Reader["RecipeName"]);
                                Recipe.RecipeDescription = Convert.ToString(Reader["RecipeDescription"]);
                                ListRecipe.Add(Recipe);
                            }
                        }
                        SqlConn.Close();
                    }
                    return ListRecipe;
                }
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
                string SpName = "dbo.Recipe_GetByRecipeID";
                RecipeData Recipe = new RecipeData();
                using (SqlConnection SqlConn = new SqlConnection())
                {
                    SqlConn.ConnectionString = SystemConfigurations.EateryConnectionString;
                    SqlConn.Open();
                    SqlCommand SqlCmd = new SqlCommand(SpName, SqlConn);
                    SqlCmd.CommandType = CommandType.StoredProcedure;
                    SqlCmd.Parameters.Add(new SqlParameter("@RecipeID", RecipeID));
                    using (SqlDataReader Reader = SqlCmd.ExecuteReader())
                    {
                        if (Reader.HasRows)
                        {
                            while (Reader.Read())
                            {
                                Recipe.DishID = Convert.ToInt32(Reader["DishID"]);
                                Recipe.RecipeID = Convert.ToInt32(Reader["RecipeID"]);
                                Recipe.RecipeName = Convert.ToString(Reader["RecipeName"]);
                                Recipe.RecipeDescription = Convert.ToString(Reader["RecipeDescription"]);
                            }
                        }
                        SqlConn.Close();
                    }
                    return Recipe;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int InsertUpdateRecipe(RecipeData Recipe, SqlTransaction SqlTrans)
        {
            try
            {
                string SpName = "dbo.Recipe_InsertUpdate";
                SqlCommand SqlCmd = new SqlCommand(SpName, SqlTrans.Connection, SqlTrans);
                SqlCmd.CommandType = CommandType.StoredProcedure;
                SqlParameter RecipeID = new SqlParameter("@RecipeID", Recipe.RecipeID);
                RecipeID.Direction = ParameterDirection.InputOutput;
                SqlCmd.Parameters.Add(RecipeID);

                SqlCmd.Parameters.Add(new SqlParameter("@DishID", Recipe.DishID));
                SqlCmd.Parameters.Add(new SqlParameter("@RecipeName", Recipe.RecipeName));
                SqlCmd.Parameters.Add(new SqlParameter("@RecipeDescription", Recipe.RecipeDescription));
                return SqlCmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int DeleteRecipes(string RecipeIDs, SqlTransaction SqlTrans)
        {
            try
            {
                string SpName = "dbo.Recipe_Delete";
                SqlCommand SqlCmd = new SqlCommand(SpName, SqlTrans.Connection, SqlTrans);
                SqlCmd.CommandType = CommandType.StoredProcedure;
                SqlCmd.Parameters.Add(new SqlParameter("@RecipeIDs", RecipeIDs));
                return SqlCmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
