using RecipesModul;
using System.Collections.Generic;

namespace RecipeTests;

public class Tests
{
    private readonly RecipeManager _recipeManager = new RecipeManager(); //создаем поле для хранения копии класса RecipeManager

    

    //тест для проверки успешного добавления рецепта
    [Test]
    public void AddRecipe_ValidRecipe_AddsRecipeToList()
    {
        
        Recipe recipe = new Recipe { Name = "Chicken Soup", Ingredients = new List<string> { "Chicken", "Vegetables" } };
        _recipeManager.AddRecipe(recipe);
        Assert.Contains(recipe, _recipeManager.GetAllRecipes()); //проверка, что рецепт существует
        
    }

    //тест для проверки  добавления  пустого рецепта
    [Test]
    public void AddRecipe_NullRecipe_ThrowsArgumentNullException()
    {
        Recipe recipe1 = null; 
        Assert.Throws<ArgumentNullException>(() => _recipeManager.AddRecipe(recipe1)); // Проверка, что выбрасывается исключение
    }


    //тест для проверки успешного удаления рецепта
    [Test]
    public void DeleteRecipe_ExistingRecipeId_DeletesRecipeFromList()
    {
        Recipe NewRecipe = new Recipe { Id = 1, Name = "Омлет", Type = "Завтрак", Ingredients = new List<string> { "Яйца", "Молоко" }, Difficulty = "Легкий", PreparationTime = 10 };
        _recipeManager.AddRecipe(NewRecipe);
        _recipeManager.DeleteRecipe(1);
        Assert.IsEmpty(_recipeManager.GetAllRecipes());//проверка, что лист с рецептами пуст
    }

    //тест для проверки  удаления несуществующего рецепта
    [Test]
    public void DeleteRecipe_UnExistingRecipeId_DeletesRecipeFromList()
    {
        Assert.Throws<Exception>(() => _recipeManager.DeleteRecipe(999)); // Проверка, что выбрасывается исключение
    }

    //тест для проверки  удаления нулевого рецепта
    [Test]
    public void DeleteRecipe_InvalidId_DeletesRecipeFromList()
    {
        Assert.Throws<Exception>(() => _recipeManager.DeleteRecipe(0)); // Проверка, что выбрасывается исключение
    }

    //тест для проверки успешного обновления рецепта
    [Test]
    public void UpdateRecipe_ExistingRecipeId_UpdateRecipe()
    {
        Recipe NewRecipe = new Recipe { Id = 1, Name = "Омлет", Type = "Завтрак", Ingredients = new List<string> { "Яйца", "Молоко" }, Difficulty = "Легкий", PreparationTime = 10 };
        _recipeManager.AddRecipe(NewRecipe);
        Recipe UpdateRecipe = new Recipe { Id = 1, Name = "Яичница", Type = "Завтрак", Ingredients = new List<string> { "Яйца" }, Difficulty = "Легкий", PreparationTime = 15 };
        _recipeManager.UpdateRecipe(UpdateRecipe);
        Recipe recipe1 = _recipeManager.GetRecipeById(1);
        Assert.AreEqual("Яичница", recipe1.Name); // проверка, что у рецепта изменилось имя
        Assert.AreEqual(15, recipe1.PreparationTime); //проверка, что у рецепта изменилось время приготовления
    }

    //тест для проверки  обновления несуществующего рецепта рецепта
    [Test]
    public void UpdateRecipe_UnExistingRecipeId_ThrowException()
    {
        Recipe NewRecipe = new Recipe { Id = 1, Name = "Омлет", Type = "Завтрак", Ingredients = new List<string> { "Яйца", "Молоко" }, Difficulty = "Легкий", PreparationTime = 10 };
        _recipeManager.AddRecipe(NewRecipe);
        Recipe UpdateRecipe = new Recipe { Id = 999, Name = "Яичница" };
        Assert.Throws<Exception>(()=> _recipeManager.UpdateRecipe(UpdateRecipe)); // Проверка, что выбрасывается исключение
    }

    //тест для проверки  обновления пустого рецепта
    [Test]
    public void UpdateRecipe_NullRecipeId_ThrowsNullReferenceException()
    {
        Assert.Throws<NullReferenceException>(() => _recipeManager.UpdateRecipe(null)); // Проверка, что выбрасывается исключение
    }

    //тест для проверки успешного получения рецептов по id
    [Test]
    public void GetRecipeById_ExistingRecipeId_GetRecipe()
    {
        Recipe NewRecipe = new Recipe { Id = 1, Name = "Омлет", Type = "Завтрак", Ingredients = new List<string> { "Яйца", "Молоко" }, Difficulty = "Легкий", PreparationTime = 10 };
        _recipeManager.AddRecipe(NewRecipe);
        Recipe recipe1 = _recipeManager.GetRecipeById(1);
        Assert.AreEqual("Омлет", recipe1.Name);//проверка, что название соответствует названию рецепта, чей id был передан
    }

    //тест для проверки  получения несуществующих рецептов по id
    [Test]
    public void GetRecipeById_UnExistingRecipeId_ThrowException()
    {
        Assert.Throws<Exception>(() => _recipeManager.GetRecipeById(100)); // Проверка, что выбрасывается исключение
        Assert.Throws<Exception>(() => _recipeManager.GetRecipeById(-100)); // Проверка, что выбрасывается исключение
        Assert.Throws<Exception>(() => _recipeManager.GetRecipeById(0)); // Проверка, что выбрасывается исключение
    }


    //тест для проверки успешного получения всех рецептов 
    [Test]
    public void GetAllReciped_AddRecipe_RecipesList()
    {
        Recipe NewRecipe = new Recipe { Id = 1, Name = "Омлет", Type = "Завтрак", Ingredients = new List<string> { "Яйца", "Молоко" }, Difficulty = "Легкий", PreparationTime = 10 };
        _recipeManager.AddRecipe(NewRecipe);
        List<Recipe> Recipes = _recipeManager.GetAllRecipes();
        Assert.Contains(NewRecipe, Recipes); //проверка, что все добавленные рецепты отображаются
    }

    //тест для проверки успешного получения всех рецептов 
    [Test]
    public void GetAllReciped_NoRecipe_EmptyRecipesList()
    {
        
        List<Recipe> Recipes = _recipeManager.GetAllRecipes();
        Assert.IsEmpty(Recipes);//проверка, что лист с рецептами пуст
    }

    //тест для проверки успешного получения рецептов по ингредиентам
    [Test]
    public void SearchRecipesByIngredients_MatchingIngridients_ReturnReciresList()
    {

        Recipe NewRecipe = new Recipe { Id = 1, Name = "Омлет", Type = "Завтрак", Ingredients = new List<string> { "Яйца", "Молоко" }, Difficulty = "Легкий", PreparationTime = 10 };
        Recipe NewRecipe2 = new Recipe { Id = 2, Name = "Салат Цезарь", Type = "Обед", Ingredients = new List<string> { "Салат", "Курица", "Яйца" }, Difficulty = "Средний", PreparationTime = 20 };
        _recipeManager.AddRecipe(NewRecipe);
        _recipeManager.AddRecipe(NewRecipe2);
        List<Recipe> searchResult = _recipeManager.SearchRecipesByIngredients(new List<string> { "Яйца" });
        Assert.Contains(NewRecipe2 , searchResult);//проверка, что первый рецепт отображается в результате поиска
        Assert.Contains(NewRecipe, searchResult);//проверка, что второй рецепт отображается в результате поиска
    }

    //тест для проверки  получения  рецептов по несуществующим ингредиентам
    [Test]
    public void SearchRecipesByIngredients_NoMatchingIngridients_ReturnEmptyReciresList()
    {

        Recipe NewRecipe = new Recipe { Id = 1, Name = "Омлет", Type = "Завтрак", Ingredients = new List<string> { "Яйца", "Молоко" }, Difficulty = "Легкий", PreparationTime = 10 };
        _recipeManager.AddRecipe(NewRecipe);
        List<Recipe> searchResult = _recipeManager.SearchRecipesByIngredients(new List<string> { "Салат" });
        Assert.IsEmpty( searchResult);//проверка, что лист с рецептами пуст
    }

    //тест для проверки  получения  рецептов по пустым ингредиентам
    [Test]
    public void SearchRecipesByIngredients_EmptyIngridients_ThrowException()
    {

        Assert.Throws<Exception>(() => _recipeManager.SearchRecipesByIngredients(new List<string> { })); // Проверка, что выбрасывается исключение
        Assert.Throws<Exception>(() => _recipeManager.SearchRecipesByIngredients(null)); // Проверка, что выбрасывается исключение
    }



    //тест для проверки успешного получения количества рецептов
    [Test]
    public void GetRecipeCount_RecipesList_CorrectCount()
    {

        Recipe NewRecipe = new Recipe { Id = 1, Name = "Омлет", Type = "Завтрак", Ingredients = new List<string> { "Яйца", "Молоко" }, Difficulty = "Легкий", PreparationTime = 10 };
        _recipeManager.AddRecipe(NewRecipe);
        int Count = _recipeManager.GetRecipeCount();
        Assert.AreEqual(1, Count);//проверка, что количество соответствует 1 добавленному рецепту
    }

    //тест для проверки успешного получения количества рецептов
    [Test]
    public void GetRecipeCounts_NoRecipes_ReturnSero()
    {
        int Count = _recipeManager.GetRecipeCount();
        Assert.AreEqual(0, Count);//проверка, что количество соответствует 0

    }


    //тест для проверки успешного получения рецептов по типу
    [Test]
    public void GetRecipesByType_MatchingType_ReturnReciresList()
    {

        Recipe NewRecipe = new Recipe { Id = 1, Name = "Омлет", Type = "Завтрак", Ingredients = new List<string> { "Яйца", "Молоко" }, Difficulty = "Легкий", PreparationTime = 10 };
        Recipe NewRecipe2 = new Recipe { Id = 2, Name = "Салат Цезарь", Type = "Обед", Ingredients = new List<string> { "Салат", "Курица", "Яйца" }, Difficulty = "Средний", PreparationTime = 20 };
        _recipeManager.AddRecipe(NewRecipe);
        _recipeManager.AddRecipe(NewRecipe2);
        List<Recipe> searchResult = _recipeManager.GetRecipesByType("Завтрак");
        Assert.Contains(NewRecipe, searchResult);//проверка, что отображается рецепт с типом "завтрак"
      
    }

    //тест для проверки  получения рецептов по несуществующему типу
    [Test]
    public void GetRecipesByTypes_NoMatchingType_ReturnEmptyReciresList()
    {

        Recipe NewRecipe = new Recipe { Id = 1, Name = "Омлет", Type = "Завтрак", Ingredients = new List<string> { "Яйца", "Молоко" }, Difficulty = "Легкий", PreparationTime = 10 };
        _recipeManager.AddRecipe(NewRecipe);      
        List<Recipe> searchResult = _recipeManager.GetRecipesByType("Обед");
        Assert.IsEmpty(searchResult);//проверка, что отображается пустой лис с типом "обед"
    }

    //тест для проверки  получения рецептов по пустому типу
    [Test]
    public void GetRecipesByType_EmptyType_ThrowException()
    {

        Assert.Throws<Exception>(() => _recipeManager.GetRecipesByType("")); // Проверка, что выбрасывается исключение

    }



    //тест для проверки успешного получения рецептов по сложности
    [Test]
    public void GetRecipesByDifficulty_MatchingDifficulty_ReturnReciresList()
    {

        Recipe NewRecipe = new Recipe { Id = 1, Name = "Омлет", Type = "Завтрак", Ingredients = new List<string> { "Яйца", "Молоко" }, Difficulty = "Легкий", PreparationTime = 10 };
       _recipeManager.AddRecipe(NewRecipe);
        List<Recipe> searchResult = _recipeManager.GetRecipesByDifficulty("Легкий");
        Assert.Contains(NewRecipe, searchResult); //проверка, что отображается рецепт со сложностью "легкий"

    }

    //тест для проверки  получения рецептов по несуществующей сложности
    [Test]
    public void GetRecipesByDifficulty_NoMatchingDifficulty_ReturnEmptyReciresList()
    {

        Recipe NewRecipe = new Recipe { Id = 1, Name = "Омлет", Type = "Завтрак", Ingredients = new List<string> { "Яйца", "Молоко" }, Difficulty = "Легкий", PreparationTime = 10 };
        _recipeManager.AddRecipe(NewRecipe);
        List<Recipe> searchResult = _recipeManager.GetRecipesByDifficulty("Средний");
        Assert.IsEmpty(searchResult);//проверка, что не отображается рецепт со сложностью "средний"
    }

    //тест для проверки  получения рецептов по пустой сложности
    [Test]
    public void GGetRecipesByDifficulty_EmptyAndNullDifficulty_ThrowException()
    {

        Assert.Throws<Exception>(() => _recipeManager.GetRecipesByDifficulty("")); // Проверка, что выбрасывается исключение
        Assert.Throws<Exception>(() => _recipeManager.GetRecipesByDifficulty(null)); // Проверка, что выбрасывается исключение
    }

    //тест для проверки успешного получения рецептов по времение приготовления
    [Test]
    public void GetRecipesByPreparationTime_MatchingPreparationTime_ReturnReciresList()
    {

        Recipe NewRecipe = new Recipe { Id = 1, Name = "Омлет", Type = "Завтрак", Ingredients = new List<string> { "Яйца", "Молоко" }, Difficulty = "Легкий", PreparationTime = 10 };
        _recipeManager.AddRecipe(NewRecipe);
        List<Recipe> searchResult = _recipeManager.GetRecipesByPreparationTime(10);
        Assert.Contains(NewRecipe, searchResult);//проверка, что отображается рецепт с временем приготовления 10 минут

    }

    //тест для проверки  получения рецептов по несуществующему времение приготовления
    [Test]
    public void GetRecipesByPreparationTime_NoMatchingPreparationTime_ReturnEmptyReciresList()
    {

        Recipe NewRecipe = new Recipe { Id = 1, Name = "Омлет", Type = "Завтрак", Ingredients = new List<string> { "Яйца", "Молоко" }, Difficulty = "Легкий", PreparationTime = 10 };
        _recipeManager.AddRecipe(NewRecipe);
        List<Recipe> searchResult = _recipeManager.GetRecipesByPreparationTime(5);
        Assert.IsEmpty(searchResult);//проверка, что не отображается рецепт с временем приготовления 5 минут
    }

    //тест для проверки  получения рецептов по пустому времение приготовления
    [Test]
    public void GetRecipesByPreparationTime_InvalidPreparationTime_ThrowException()
    {

        Assert.Throws<Exception>(() => _recipeManager.GetRecipesByPreparationTime(0)); // Проверка, что выбрасывается исключение
        Assert.Throws<Exception>(() => _recipeManager.GetRecipesByPreparationTime(-1)); // Проверка, что выбрасывается исключение
    }


}
