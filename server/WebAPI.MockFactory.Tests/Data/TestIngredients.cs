namespace WebAPI.MockFactory.Tests.Data
{
    using System.Collections.Generic;
    using Domain.Models;

    public static class TestIngredients
    {
        public static Ingredient IngredientA = new() { Name = "Mozzarella", Price = 59, ImageLink = "IngredientA image" };
        public static Ingredient IngredientB = new() { Name = "Pepperoni", Price = 48, ImageLink = "IngredientB image" };
        public static Ingredient IngredientC = new() { Name = "Black pepper", Price = 30, ImageLink = "IngredientC image" };

        public static List<Ingredient> AllIngredients = new() { IngredientA, IngredientB, IngredientC };
    }
}
