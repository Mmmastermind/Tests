using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Threading;

//класс, который хранит все функции библиотеки
namespace RecipesModul
{
    public class RecipeManager
    {
        private List<Recipe> recipes = new List<Recipe>(); //объявление листа, в который будут добавляться рецепты

        //добавление рецепта
        public void AddRecipe(Recipe recipe)
        {
            if (recipe == null) //проверка на нулевое значение
            {
                throw new ArgumentNullException(nameof(recipe), "Рецепт не может быть null.");
            }
            
            try
            {
                //автоматически подставляем id
                    recipe.Id = recipes.Count > 0 ? recipes.Max(r => r.Id) + 1 : 1;
                    recipes.Add(recipe);
                
            }
           //непредвиденные ошибки
            catch (Exception ex)
            {
                
                throw new Exception("Ошибка при добавлении рецепта.", ex);
            }
        }

        //удаление рецепта
        public void DeleteRecipe(int id)
        {
            try
            { //проверка на нулевое значение
                if (id <= 0)
                {
                    throw new ArgumentException("Недопустимый ID рецепта.", nameof(id));
                    

                }
                //проверка на наличие рецепта с переданным id
                int initialCount = recipes.Count;
                recipes.RemoveAll(r => r.Id == id);
                if (recipes.Count == initialCount)
                {
                    throw new KeyNotFoundException($"Рецепт с ID {id} не найден.");
                   
                }
            }
            //непредвиденные ошибки
            catch (Exception ex)
            {
                
                throw new Exception($"Ошибка при удалении рецепта с ID {id}.", ex);
                
            }
            
        }

        //обновление рецепта
        public void UpdateRecipe(Recipe updatedRecipe)
        {
            
            try
            {
                //проверка на нулевое значение
                if (updatedRecipe == null)
                {
                   
                    throw new ArgumentNullException(nameof(updatedRecipe), "Обновленный рецепт не может быть null.");
                }
               
                var existingRecipe = recipes.FirstOrDefault(r => r.Id == updatedRecipe.Id); //переменная, которая хранит рецепты с переданным id рецепта
                //проверка на наличие изначального рецепта с переданным id
                if (existingRecipe == null)
                {
                    throw new KeyNotFoundException($"Рецепт с ID {updatedRecipe.Id} не найден.");
                }
                //изменение рецепта
                existingRecipe.Name = updatedRecipe.Name;
                existingRecipe.Ingredients = updatedRecipe.Ingredients;
                existingRecipe.PreparationTime = updatedRecipe.PreparationTime;
                existingRecipe.Type = updatedRecipe.Type;
                existingRecipe.Difficulty = updatedRecipe.Difficulty;
            }
            //непредвиденные ошибки
            catch (Exception ex)
            {
               
                    throw new Exception($"Ошибка при обновлении рецепта с ID {updatedRecipe.Id}.", ex);
                
            }
        }

        //получение рецепта по id
        public Recipe GetRecipeById(int id)
        {
            try
            {
                //возвращаем список рецептов
                var recipe = recipes.FirstOrDefault(r => r.Id == id);    //переменная, которая хранит рецепты с переданным id           
                return recipe;
            }//непредвиденные ошибки
            catch (Exception ex)
            {
                    throw new Exception($"Ошибка при получении рецепта с ID {id}.", ex);
                }
            
        }

        //получение всех рецептов
        public List<Recipe> GetAllRecipes()
        {
            try
            {
                //возвращаем список рецептов
                return recipes;
            }//непредвиденные ошибки
            catch (Exception ex)
            {
             
                throw new Exception("Ошибка при получении всех рецептов.", ex);
            }
        }

        //получение рецепто по ингредиентам
        public List<Recipe> SearchRecipesByIngredients(List<string> ingredients)
        {
            try
            {
                if (ingredients == null || ingredients.Count == 0)
                {
                    throw new ArgumentException("Список ингредиентов не может быть null или пустым.", nameof(ingredients));
                }
                //возвращаем список рецептов
                return recipes.Where(r => r.Ingredients.Intersect(ingredients).Any()).ToList();
            }//непредвиденные ошибки
            catch (Exception ex)
            {
               
                throw new Exception("Ошибка при поиске рецептов по ингредиентам.", ex);
            }
        }

        //получение количества рецептов
        public int GetRecipeCount()
        {
            try
            {
                //возвращаем количество рецептов
                return recipes.Count;
            }//непредвиденные ошибки
            catch (Exception ex)
            {
            
                throw new Exception("Ошибка при получении количества рецептов.", ex);
            }
        }

        //получение рецептов по типу
        public List<Recipe> GetRecipesByType(string type)
        {
            try
            {
                if (string.IsNullOrEmpty(type))
                {
                    throw new ArgumentException("Тип не может быть null или пустым.", nameof(type));
                }
                //возвращаем список рецептов
                return recipes.Where(r => r.Type == type).ToList();
            }//непредвиденные ошибки
            catch (Exception ex)
            {
                
                throw new Exception($"Ошибка при получении рецептов по типу {type}.", ex);
            }
        }

        //получение рецептов по сложности
        public List<Recipe> GetRecipesByDifficulty(string difficulty)
        {
            try
            {//проверка на нулевое значение
                if (string.IsNullOrEmpty(difficulty))
                {
                    throw new ArgumentException("Сложность не может быть null или пустой.", nameof(difficulty));
                }
                //возвращаем список рецептов
                return recipes.Where(r => r.Difficulty == difficulty).ToList();
            }
            //непредвиденные ошибки
            catch (Exception ex)
            {
               
                throw new Exception($"Ошибка при получении рецептов по сложности {difficulty}.", ex);
            }
        }

        //вывод рецептов по времени приготовления
        public List<Recipe> GetRecipesByPreparationTime(int maxTime)
        {// проверка, что время передано правильно
            if (maxTime <= 0)
            {
                throw new Exception("Время приготовления должно быть больше 0");
            }
            //возвращаем список рецептов
            return recipes.Where(r => r.PreparationTime <= maxTime).ToList();
        }
    }
}
