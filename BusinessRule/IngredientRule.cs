using Common.Data;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemFramework;

namespace BusinessRule
{
    public class IngredientRule
    {
        public int InsertUpdateIngredient(IngredientData Ingredient)
        {
            SqlConnection SqlConn = null;
            SqlTransaction SqlTrans = null;
            try
            {
                SqlConn = new SqlConnection(SystemConfigurations.EateryConnectionString);
                SqlConn.Open();
                SqlTrans = SqlConn.BeginTransaction();
                int rowsAffected = new IngredientDB().InsertUpdateIngredient(Ingredient, SqlTrans);
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
        public int DeleteIngredients(IEnumerable<int> IngredientIDs)
        {
            SqlConnection SqlConn = null;
            SqlTransaction SqlTrans = null;
            try
            {
                SqlConn = new SqlConnection(SystemConfigurations.EateryConnectionString);
                SqlConn.Open();
                SqlTrans = SqlConn.BeginTransaction();
                int rowsAffected = new IngredientDB().DeleteIngredients(String.Join(",", IngredientIDs), SqlTrans);
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
