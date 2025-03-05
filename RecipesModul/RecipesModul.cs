using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Threading;
namespace RecipesModul
{
    public class RecipeManager
    {
        private List<Recipe> recipes = new List<Recipe>();


        public void AddRecipe(Recipe recipe)
        {
            try
            {
                if (string.IsNullOrEmpty(recipe.Name) || string.IsNullOrEmpty(recipe.Type) || recipe.Ingredients == null || recipe.PreparationTime == 0 || string.IsNullOrEmpty(recipe.Difficulty))
                {
                    throw new Exception("Ошибка при добавлении рецепта.");
                }
                else
                {
                    recipe.Id = recipes.Count > 0 ? recipes.Max(r => r.Id) + 1 : 1;
                    recipes.Add(recipe);
                }
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException(nameof(recipe), "Рецепт не может быть null.");
            }
            catch (Exception ex)
            {
                
                throw new Exception("Ошибка при добавлении рецепта.", ex);
            }
        }


        public void DeleteRecipe(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new ArgumentException("Недопустимый ID рецепта.", nameof(id));
                    

                }
                int initialCount = recipes.Count;
                recipes.RemoveAll(r => r.Id == id);
                if (recipes.Count == initialCount)
                {
                    throw new KeyNotFoundException($"Рецепт с ID {id} не найден.");
                   
                }
            }
          
            catch (Exception ex)
            {
                
                throw new Exception($"Ошибка при удалении рецепта с ID {id}.", ex);
                
            }
            
        }


        public void UpdateRecipe(Recipe updatedRecipe)
        {
            
            try
            {
                if (updatedRecipe == null)
                {
                   
                    throw new ArgumentNullException(nameof(updatedRecipe), "Обновленный рецепт не может быть null.");
                }
               
                var existingRecipe = recipes.FirstOrDefault(r => r.Id == updatedRecipe.Id);
                if (existingRecipe == null)
                {
                    throw new KeyNotFoundException($"Рецепт с ID {updatedRecipe.Id} не найден.");
                }
                existingRecipe.Name = updatedRecipe.Name;
                existingRecipe.Ingredients = updatedRecipe.Ingredients;
                existingRecipe.PreparationTime = updatedRecipe.PreparationTime;
                existingRecipe.Type = updatedRecipe.Type;
                existingRecipe.Difficulty = updatedRecipe.Difficulty;
            }
           
            catch (Exception ex)
            {
                if (updatedRecipe == null)
                {

                    throw new ArgumentNullException(nameof(updatedRecipe), "Обновленный рецепт не может быть null.");
                }
                var existingRecipe = recipes.FirstOrDefault(r => r.Id == updatedRecipe.Id);
                if (existingRecipe == null)
                {
                    throw new KeyNotFoundException($"Рецепт с ID {updatedRecipe.Id} не найден.");
                }
                else
                {
                    throw new Exception($"Ошибка при обновлении рецепта с ID {updatedRecipe.Id}.", ex);
                }
            }
        }


        public Recipe GetRecipeById(int id)
        {
            try
            {
                var recipe = recipes.FirstOrDefault(r => r.Id == id);               
                return recipe;
            }
            catch (Exception ex)
            {
                    throw new Exception($"Ошибка при получении рецепта с ID {id}.", ex);
                }
            
        }


        public List<Recipe> GetAllRecipes()
        {
            try
            {
                return recipes;
            }
            catch (Exception ex)
            {
             
                throw new Exception("Ошибка при получении всех рецептов.", ex);
            }
        }


        public List<Recipe> SearchRecipesByIngredients(List<string> ingredients)
        {
            try
            {
                if (ingredients == null || ingredients.Count == 0)
                {
                    throw new ArgumentException("Список ингредиентов не может быть null или пустым.", nameof(ingredients));
                }
                return recipes.Where(r => r.Ingredients.Intersect(ingredients).Any()).ToList();
            }
            catch (Exception ex)
            {
               
                throw new Exception("Ошибка при поиске рецептов по ингредиентам.", ex);
            }
        }


        public int GetRecipeCount()
        {
            try
            {
                return recipes.Count;
            }
            catch (Exception ex)
            {
            
                throw new Exception("Ошибка при получении количества рецептов.", ex);
            }
        }


        public List<Recipe> GetRecipesByType(string type)
        {
            try
            {
                if (string.IsNullOrEmpty(type))
                {
                    throw new ArgumentException("Тип не может быть null или пустым.", nameof(type));
                }
                return recipes.Where(r => r.Type == type).ToList();
            }
            catch (Exception ex)
            {
                
                throw new Exception($"Ошибка при получении рецептов по типу {type}.", ex);
            }
        }


        public List<Recipe> GetRecipesByDifficulty(string difficulty)
        {
            try
            {
                if (string.IsNullOrEmpty(difficulty))
                {
                    throw new ArgumentException("Сложность не может быть null или пустой.", nameof(difficulty));
                }
                return recipes.Where(r => r.Difficulty == difficulty).ToList();
            }
            
            catch (Exception ex)
            {
               
                throw new Exception($"Ошибка при получении рецептов по сложности {difficulty}.", ex);
            }
        }


        public List<Recipe> GetRecipesByPreparationTime(int maxTime)
        {
            try
            {

                return recipes.Where(r => r.PreparationTime <= maxTime).ToList();
            }
            catch (Exception)
            {

                throw new Exception($"Ошибка при получении рецептов по времени приготовления.");
            }

        
            
            
            
        }
    }
}
