namespace WebAPI.MockFactory.Tests.Data
{
    using System.Collections.Generic;
    using Domain.Models;

    public static class TestAdditionalIngredients
    {
        public static AdditionalIngredient AdditionalIngredientA = new() { Name = "Mozzarella", Price = 59, ImageLink = "IngredientA image" };
        public static AdditionalIngredient AdditionalIngredientB = new() { Name = "Pepperoni", Price = 48, ImageLink = "IngredientB image" };
        public static AdditionalIngredient AdditionalIngredientC = new() { Name = "Black pepper", Price = 30, ImageLink = "IngredientC image" };

        public static List<AdditionalIngredient> AllAdditionalIngredients = new() { AdditionalIngredientA, AdditionalIngredientB, AdditionalIngredientC };
    }
}
