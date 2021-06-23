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
    public class IngredientDB
    {
        public List<IngredientData> GetIngredientListByRecipeID(int RecipeID)
        {
            try
            {
                string SpName = "dbo.Ingredient_GetByRecipeID";
                List<IngredientData> ListIngredient = new List<IngredientData>();
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
                                IngredientData Ingredient = new IngredientData();
                                Ingredient.IngredientID = Convert.ToInt32(Reader["IngredientID"]);
                                Ingredient.RecipeID = Convert.ToInt32(Reader["RecipeID"]);
                                Ingredient.IngredientName = Convert.ToString(Reader["IngredientName"]);
                                Ingredient.IngredientQuantity = Convert.ToInt32(Reader["IngredientQuantity"]);
                                Ingredient.IngredientUnit = Convert.ToString(Reader["IngredientUnit"]);
                                ListIngredient.Add(Ingredient);
                            }
                        }
                        SqlConn.Close();
                    }
                    return ListIngredient;
                }
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
                string SpName = "dbo.Ingredient_GetByIngredientID";
                IngredientData Ingredient = new IngredientData();
                using (SqlConnection SqlConn = new SqlConnection())
                {
                    SqlConn.ConnectionString = SystemConfigurations.EateryConnectionString;
                    SqlConn.Open();
                    SqlCommand SqlCmd = new SqlCommand(SpName, SqlConn);
                    SqlCmd.CommandType = CommandType.StoredProcedure;
                    SqlCmd.Parameters.Add(new SqlParameter("@IngredientID", IngredientID));
                    using (SqlDataReader Reader = SqlCmd.ExecuteReader())
                    {
                        if (Reader.HasRows)
                        {
                            while (Reader.Read())
                            {
                                Ingredient.IngredientID = Convert.ToInt32(Reader["IngredientID"]);
                                Ingredient.RecipeID = Convert.ToInt32(Reader["RecipeID"]);
                                Ingredient.IngredientName = Convert.ToString(Reader["IngredientName"]);
                                Ingredient.IngredientQuantity = Convert.ToInt32(Reader["IngredientQuantity"]);
                                Ingredient.IngredientUnit = Convert.ToString(Reader["IngredientUnit"]);
                            }
                        }
                        SqlConn.Close();
                    }
                return Ingredient;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int InsertUpdateIngredient(IngredientData Ingredient, SqlTransaction SqlTrans)
        {
            try
            {
                string SpName = "dbo.Ingredient_InsertUpdate";
                SqlCommand SqlCmd = new SqlCommand(SpName, SqlTrans.Connection, SqlTrans);
                SqlCmd.CommandType = CommandType.StoredProcedure;
                SqlParameter IngredientID = new SqlParameter("@IngredientID", Ingredient.IngredientID);
                IngredientID.Direction = ParameterDirection.InputOutput;
                SqlCmd.Parameters.Add(IngredientID);

                SqlCmd.Parameters.Add(new SqlParameter("@RecipeID", Ingredient.RecipeID));
                SqlCmd.Parameters.Add(new SqlParameter("@IngredientName", Ingredient.IngredientName));
                SqlCmd.Parameters.Add(new SqlParameter("@IngredientQuantity", Ingredient.IngredientQuantity));
                SqlCmd.Parameters.Add(new SqlParameter("@IngredientUnit", Ingredient.IngredientUnit));
                return SqlCmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int DeleteIngredients(string IngredientIDs, SqlTransaction SqlTrans)
        {
            try
            {
                string SpName = "dbo.Ingredient_Delete";
                SqlCommand SqlCmd = new SqlCommand(SpName, SqlTrans.Connection, SqlTrans);
                SqlCmd.CommandType = CommandType.StoredProcedure;
                SqlCmd.Parameters.Add(new SqlParameter("@IngredientIDs", IngredientIDs));
                return SqlCmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
