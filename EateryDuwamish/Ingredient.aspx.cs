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
    public partial class Ingredient : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if(Request.QueryString["RecipeID"] == null)
                {
                    string BackToPreviousUrl = Request.UrlReferrer == null ? "Home.aspx" : Request.UrlReferrer.ToString();
                    Response.Redirect(BackToPreviousUrl);
                }
                else
                {
                    LoadIngredientTable();
                    LoadRecipeTitleAndDescription();
                    ShowNotificationIfExists();
                }
            }
        }
        #region FORM MANAGEMENT
        private void FillForm(IngredientData Ingredient)
        {
            hdfIngredientId.Value = Ingredient.IngredientID.ToString();
            hdfRecipeId.Value = Ingredient.RecipeID.ToString();
            txtIngredientName.Text = Ingredient.IngredientName;
            txtIngredientQuantity.Text = Ingredient.IngredientQuantity.ToString();
            txtIngredientUnit.Text = Ingredient.IngredientUnit;
        }
        private void ResetForm()
        {
            hdfIngredientId.Value = String.Empty;
            hdfRecipeId.Value = Request.QueryString["RecipeID"].ToString();
            txtIngredientName.Text = String.Empty;
            txtIngredientQuantity.Text = String.Empty;
            txtIngredientUnit.Text = String.Empty;
        }
        private IngredientData GetFormData()
        {
            IngredientData Ingredient = new IngredientData();
            Ingredient.IngredientID = String.IsNullOrEmpty(hdfIngredientId.Value) ? 0 : Convert.ToInt32(hdfIngredientId.Value);
            Ingredient.RecipeID = Convert.ToInt32(hdfRecipeId.Value);
            Ingredient.IngredientName = txtIngredientName.Text;
            Ingredient.IngredientQuantity = Convert.ToInt32(txtIngredientQuantity.Text);
            Ingredient.IngredientUnit = txtIngredientUnit.Text;
            return Ingredient;
        }
        #endregion
        #region DATA TABLE MANAGEMENT
        private void LoadIngredientTable()
        {
            try
            {
                int RecipeID = Convert.ToInt32(Request.QueryString["RecipeID"]);
                List<IngredientData> ListIngredient = new IngredientSystem().GetIngredientListByRecipeID(RecipeID);
                rptIngredient.DataSource = ListIngredient;
                rptIngredient.DataBind();
            }
            catch (Exception ex)
            {
                notifIngredient.Show($"ERROR LOAD TABLE: {ex.Message}", NotificationType.Danger);
            }
        }
        protected void rptIngredient_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                IngredientData Ingredient = (IngredientData)e.Item.DataItem;
                Literal litIngredientName = (Literal)e.Item.FindControl("litIngredientName");
                Literal litIngredientQuantity = (Literal)e.Item.FindControl("litIngredientQuantity");
                Literal litIngredientUnit = (Literal)e.Item.FindControl("litIngredientUnit");
                LinkButton lbEdit = (LinkButton)e.Item.FindControl("lbEdit");

                litIngredientName.Text = Ingredient.IngredientName;
                litIngredientQuantity.Text = Ingredient.IngredientQuantity.ToString();
                litIngredientUnit.Text = Ingredient.IngredientUnit;

                lbEdit.Text = "Edit";
                lbEdit.CommandArgument = Ingredient.IngredientID.ToString();

                CheckBox chkChoose = (CheckBox)e.Item.FindControl("chkChoose");
                chkChoose.Attributes.Add("data-value", Ingredient.IngredientID.ToString()) ;
            }
        }
        protected void rptIngredient_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "EDIT")
            {
                Literal litIngredientName = (Literal)e.Item.FindControl("litIngredientName");
                Literal litIngredientQuantity = (Literal)e.Item.FindControl("litIngredientQuantity");
                Literal litIngredientUnit = (Literal)e.Item.FindControl("litIngredientUnit");

                int IngredientID = Convert.ToInt32(e.CommandArgument.ToString());
                IngredientData Ingredient = new IngredientSystem().GetIngredientByIngredientID(IngredientID);
                FillForm(new IngredientData
                {
                    IngredientID = Ingredient.IngredientID,
                    RecipeID = Ingredient.RecipeID,
                    IngredientName = Ingredient.IngredientName,
                    IngredientQuantity = Ingredient.IngredientQuantity,
                    IngredientUnit = Ingredient.IngredientUnit
                });
                litFormType.Text = $"UBAH: {litIngredientName.Text}";
                pnlFormIngredient.Visible = true;
            }
        }
        #endregion
        #region RECIPE DESCRIPTION MANAGEMENT
        private void LoadRecipeTitleAndDescription()
        {
            Literal litTableTitle = this.litTableTitle;
            int RecipeID = Convert.ToInt32(Request.QueryString["RecipeID"]);
            RecipeData Recipe = new RecipeSystem().GetRecipeByRecipeID(RecipeID);

            litTableTitle.Text = Recipe.RecipeName;
            txtRecipeDescription.Text = Recipe.RecipeDescription == null ? String.Empty : Recipe.RecipeDescription;
        }
        #endregion
        #region BUTTON EVENT MANAGEMENT
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                IngredientData Ingredient = GetFormData();
                int rowAffected = new IngredientSystem().InsertUpdateIngredient(Ingredient);
                if (rowAffected <= 0)
                    throw new Exception("No Data Recorded");
                Session["save-success"] = 1;
                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ex)
            {
                notifIngredient.Show($"ERROR SAVE DATA: {ex.Message}", NotificationType.Danger);
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ResetForm();
            litFormType.Text = "TAMBAH";
            pnlFormIngredient.Visible = true;
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string strDeletedIDs = hdfDeletedIngredients.Value;
                IEnumerable<int> deletedIDs = strDeletedIDs.Split(',').Select(Int32.Parse);
                int rowAffected = new IngredientSystem().DeleteIngredients(deletedIDs);
                if (rowAffected <= 0)
                    throw new Exception("No Data Deleted");
                Session["delete-success"] = 1;
                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ex)
            {
                notifIngredient.Show($"ERROR DELETE DATA: {ex.Message}", NotificationType.Danger);
            }
        }
        protected void btnEditRecipeDesc_Click(object sender, EventArgs e)
        {
            if(txtRecipeDescription.Enabled == false)
                txtRecipeDescription.Enabled = true;
        }
        protected void btnSaveRecipeDesc_Click(object sender, EventArgs e)
        {
            try
            {
                int RecipeID = Convert.ToInt32(Request.QueryString["RecipeID"]);
                RecipeData Recipe = new RecipeSystem().GetRecipeByRecipeID(RecipeID);
                Recipe.RecipeDescription = txtRecipeDescription.Text;
                int rowAffected = new RecipeSystem().InsertUpdateRecipe(Recipe);
                if (rowAffected <= 0)
                    throw new Exception("No Data Recorded");
                Session["save-success"] = 1;
                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ex)
            {
                notifIngredient.Show($"ERROR SAVE DATA: {ex.Message}", NotificationType.Danger);
            }
        }
        #endregion
        #region NOTIFICATION MANAGEMENT
        private void ShowNotificationIfExists()
        {
            if (Session["save-success"] != null)
            {
                notifIngredient.Show("Data sukses disimpan", NotificationType.Success);
                Session.Remove("save-success");
            }
            if (Session["delete-success"] != null)
            {
                notifIngredient.Show("Data sukses dihapus", NotificationType.Success);
                Session.Remove("delete-success");
            }
        }
        #endregion
    }
}