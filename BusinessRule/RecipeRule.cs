using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data;
using DataAccess;
using SystemFramework;

namespace BusinessRule
{
    public class RecipeRule
    {
        public int InsertUpdateRecipe(RecipeData Recipe)
        {
            SqlConnection SqlConn = null;
            SqlTransaction SqlTrans = null;
            try
            {
                SqlConn = new SqlConnection(SystemConfigurations.EateryConnectionString);
                SqlConn.Open();
                SqlTrans = SqlConn.BeginTransaction();
                int rowsAffected = new RecipeDB().InsertUpdateRecipe(Recipe, SqlTrans);
                SqlTrans.Commit();
                SqlConn.Close();
                return rowsAffected;
            }
            catch (Exception)
            {
                SqlTrans.Rollback();
                SqlConn.Close();
                throw;
            }
        }
        public int DeleteRecipes(IEnumerable<int> RecipeIDs)
        {
            SqlConnection SqlConn = null;
            SqlTransaction SqlTrans = null;
            try
            {
                SqlConn = new SqlConnection(SystemConfigurations.EateryConnectionString);
                SqlConn.Open();
                SqlTrans = SqlConn.BeginTransaction();
                int rowsAffected = new RecipeDB().DeleteRecipes(String.Join(",", RecipeIDs), SqlTrans);
                SqlTrans.Commit();
                SqlConn.Close();
                return rowsAffected;
            }
            catch (Exception)
            {
                SqlTrans.Rollback();
                SqlConn.Close();
                throw;
            }
        }
    }
}
