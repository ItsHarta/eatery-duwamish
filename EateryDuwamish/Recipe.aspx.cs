using BusinessFacade;
using Common.Data;
using Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EateryDuwamish
{
    public partial class Recipe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowNotificationIfExists();
                LoadRecipeTable();
            }
        }
        #region FORM MANAGEMENT
        private void FillForm(RecipeData Recipe)
        {
            hdfDishId.Value = Recipe.DishID.ToString();
            hdfRecipeId.Value = Recipe.RecipeID.ToString();
            txtRecipeName.Text = Recipe.RecipeName;
        }
        private void ResetForm()
        {
            hdfRecipeId.Value = String.Empty;
            hdfDishId.Value = Convert.ToString(Request.QueryString["DishID"]);
            txtRecipeName.Text = String.Empty;
        }
        private RecipeData GetFormData()
        {
            RecipeData Recipe = new RecipeData();
            Recipe.RecipeID = String.IsNullOrEmpty(hdfRecipeId.Value)? 0 : Convert.ToInt32(hdfRecipeId.Value);
            Recipe.DishID = Convert.ToInt32(hdfDishId.Value);
            Recipe.RecipeName = txtRecipeName.Text;
            Recipe.RecipeDescription = String.Empty;
            return Recipe;
        }
        #endregion
        #region DATA TABLE MANAGEMENT
        private void LoadRecipeTable()
        {
            try
            {
                List<RecipeData> ListRecipe = null;
                if (Request.QueryString["DishID"] != null)
                {
                    int DishID = int.Parse(Request.QueryString["DishID"]);
                    ListRecipe = new RecipeSystem().GetRecipeListByDishID(DishID);
                }
                else
                {
                    ListRecipe = new RecipeSystem().GetRecipeList();
                }
                rptRecipe.DataSource = ListRecipe;
                rptRecipe.DataBind();
            }
            catch(Exception ex)
            {
                notifRecipe.Show($"ERROR LOADING TABLE: {ex.Message}", NotificationType.Danger);
            }
        }
        protected void rptRecipe_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                RecipeData Recipe = (RecipeData)e.Item.DataItem;
                LinkButton lbRecipeName = (LinkButton)e.Item.FindControl("lbRecipeName");
                LinkButton lbDetail = (LinkButton)e.Item.FindControl("lbDetail");

                lbRecipeName.Text = Recipe.RecipeName;
                lbRecipeName.CommandArgument = Recipe.RecipeID.ToString();

                lbDetail.Text = "Detail";
                lbDetail.CommandArgument = Recipe.RecipeID.ToString();

                CheckBox chkChoose = (CheckBox)e.Item.FindControl("chkChoose");
                chkChoose.Attributes.Add("data-value", Recipe.RecipeID.ToString());
            }
        }
        protected void rptRecipe_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if(e.CommandName == "TO_DETAIL")
            {
                LinkButton lbDetail = (LinkButton)e.Item.FindControl("lbDetail");
                int RecipeID = Convert.ToInt32(e.CommandArgument.ToString());
                Response.Redirect("Ingredient.aspx?RecipeID=" + RecipeID);
            }
            else if(e.CommandName == "EDIT")
            {
                LinkButton lbRecipeName = (LinkButton)e.Item.FindControl("lbRecipeName");
                int RecipeID = Convert.ToInt32(e.CommandArgument.ToString());
                RecipeData Recipe = new RecipeSystem().GetRecipeByRecipeID(RecipeID);
                FillForm(new RecipeData
                {
                    RecipeID = Recipe.RecipeID,
                    DishID = Recipe.DishID,
                    RecipeName = Recipe.RecipeName,
                });
                litFormType.Text = $"UBAH: { lbRecipeName.Text}";
                pnlFormRecipe.Visible = true;
                txtRecipeName.Focus();
            }
        }
        #endregion
        #region BUTTON EVENT MANAGEMENT
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                RecipeData Recipe = GetFormData();
                int rowAffected = new RecipeSystem().InsertUpdateRecipe(Recipe);
                if (rowAffected <= 0)
                    throw new Exception("No Data Recorded");
                Session["save-success"] = 1;
                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ex)
            {
                notifRecipe.Show($"ERROR SAVE DATA: {ex.Message}", NotificationType.Danger);
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["DishID"] == null)
            {
                notifRecipe.Show("ERROR ADD DATA: Please Add Recipe From Dish Page", NotificationType.Danger);
            }
            else
            {
                ResetForm();
                litFormType.Text = "TAMBAH";
                pnlFormRecipe.Visible = true;
                txtRecipeName.Focus();
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string strDeletedIDs = hdfDeletedRecipes.Value;
                IEnumerable<int> deletedIDs = strDeletedIDs.Split(',').Select(Int32.Parse);
                int rowAffected = new RecipeSystem().DeleteRecipes(deletedIDs);
                if (rowAffected <= 0)
                    throw new Exception("No Data Deleted");
                Session["delete-success"] = 1;
                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ex)
            {
                notifRecipe.Show($"ERROR DELETE DATA: {ex.Message}", NotificationType.Danger);
            }
        }
        #endregion
        #region NOTIFICATION MANAGEMENT
        private void ShowNotificationIfExists()
        {
            if (Session["save-success"] != null)
            {
                notifRecipe.Show("Data sukses disimpan", NotificationType.Success);
                Session.Remove("save-success");
            }
            if(Session["delete-success"] != null)
            {
                notifRecipe.Show("Data sukses dihapus", NotificationType.Success);
                Session.Remove("delete-success");
            }
            
        }
        #endregion
    }
}