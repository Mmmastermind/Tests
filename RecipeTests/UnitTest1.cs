using RecipesModul;
using System.Collections.Generic;

namespace RecipeTests;

public class Tests
{
    private readonly RecipeManager _recipeManager;


    public Tests()
    {
        _recipeManager = new RecipeManager();
    }


    public class RecipeFixture
    {

        public RecipeManager RecipeManager { get; private set; }


        public RecipeFixture()
        {
            RecipeManager = new RecipeManager();
            Recipe NewRecipe = new Recipe { Id = 1, Name = "Омлет", Type = "Завтрак", Ingredients = new List<string> { "Яйца", "Молоко" }, Difficulty = "Легкий", PreparationTime = 10 });
            // RecipeManager.AddRecipe(new Recipe { Id = 2, Name = "Салат Цезарь", Type = "Обед", Ingredients = new List<string> { "Салат", "Курица" }, Difficulty = "Средний", PreparationTime = 20 });
            _recipeManager = new RecipeManager(NewRecipe);
        }
    }

    [Test]
    public void AddRecipe_ValidRecipe_AddsRecipeToList()
    {
        
        Recipe recipe = new Recipe { Name = "Chicken Soup", Ingredients = new List<string> { "Chicken", "Vegetables" } };
        _recipeManager.AddRecipe(recipe);
        Assert.Contains(recipe, _recipeManager.GetAllRecipes());
        
    }

    [Test]
    public void AddRecipe_NullRecipe_ThrowsArgumentNullException()
    {
        Recipe recipe1 = null; 
        Assert.Throws<ArgumentNullException>(() => _recipeManager.AddRecipe(recipe1));
    }

    [Test]
    public void AddRecipe_RecipeWithEmptyName_AddsRecipe()
    {
        Recipe recipe = new Recipe { Name = "", Ingredients = new List<string> { "Ingredient1" } };
        _recipeManager.AddRecipe(recipe);
        Assert.IsNotEmpty(_recipeManager.GetAllRecipes());

    }

    [Test]
    public void DeleteRecipe_ExistingRecipeId_DeletesRecipeFromList()
    {

        _recipeManager.DeleteRecipe(1);
        _recipeManager.DeleteRecipe(2);

        Assert.IsEmpty(_recipeManager.GetAllRecipes());
    }
}