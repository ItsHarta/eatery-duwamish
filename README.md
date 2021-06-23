# eatery-duwamish
THIS IS AN APPLICATION OF ASP.NET WEBFORM (DUWAMISH)

STEP-STEP:
1. RUN init_query.sql
2. SETUP web.config TO CONNECT LOCAL DB
3. EXECUTE

TASK:
- DEVELOP MORE FEATURES BASED ON THE FOLLOWING PROTOTYPE:
  https://prototype-eatery.azurewebsites.net/

CHANGELOG:
- 09/06
  - adjust connection string

- 15/06
  - added database ERD file
  - change init sql script according to ERD

- 16/06
  - added files to be used
  - created RecipeData and IngredientData
  - added menu to recipes page on dish page
  - created recipe page
  - created get recipe and get recipe by dish id:
    - recipe page will show all dishes if there is no dish id parameter
    - recipe page will show recipe by dish id if dish id parameter is specified
- 20/06
  - fixed recipe table not loaded properly in aspx page
  - added conditional and error handling :
    - prevent new recipe to be created if there is no dish id parameter
    - fixed several error messages
  - added form for recipe insert, edit and delete
  - added recipe rule for:
    - insert update recipe
    - delete recipe(s) by recipe id
  - added recipe system for :
    - get recipe by recipe id
    - insert update recipe
    - delete recipe(s) by recipe id
  - added dish id property to recipe data
  - added recipe db for:
     - modified get recipe list by dish id
        - allow recipe description to be null
     - added get recipe by recipe id : [dbo.Recipe_GetByRecipeID]
     - added insert update recipe : [dbo.Recipe_InsertUpdate]
     - added delete recipe : [dbo.Recipe_Delete]
- 21/06
  - ERD change : set RecipeDescription to allow null
  - updated init sql according to ERD
- 22/06
  - changed recipe description to not allow null (changed to empty string) : updated ERD and init sql
  - added ingredient db for :
    - get ingredient by ingredient id
    - get ingredient list by recipe id
    - insert update ingredient
    - delete ingredient
  - added ingredient rule for :
    - insert update ingredient
    - delete ingredient
  - added ingredient system for :
    - get ingredient list by recipe id
    - get ingredient by ingredient id
  - added recipe page and codebehind (aspx)
  - prevent recipe page to be accessed without parameter