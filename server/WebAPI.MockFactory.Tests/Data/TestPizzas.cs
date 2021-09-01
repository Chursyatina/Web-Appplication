namespace WebAPI.MockFactory.Tests.Data
{
    using System.Collections.Generic;
    using Domain.Models;

    public static class TestPizzas
    {
        public static Ingredient IngredientA => new () { Name = "Mozzarella ", Price = 10, ImageLink = "Image link", Pizzas = new List<Pizza>(), };

        public static Ingredient IngredientB => new () { Name = "Pepperoni", Price = 10, ImageLink = "Image link", Pizzas = new List<Pizza>(), };

        public static Ingredient IngredientC => new () { Name = "black pepper", Price = 10, ImageLink = "Image link", Pizzas = new List<Pizza>(), };

        public static Pizza PizzaA => new()
        {
            Name = "Pepperoni",
            Description = "Some pepperoni description",
            Price = 12,
            ImageLink = "Image link",
            SingleItemImageLink = "Single image link",
            Ingredients = new List<Ingredient>() { IngredientA, IngredientB, IngredientC, },
        };

        public static Pizza PizzaB => new()
        {
            Name = "4 cheese ",
            Description = "Some 4 cheese description",
            Price = 12,
            ImageLink = "Image link",
            SingleItemImageLink = "Single image link",
            Ingredients = new List<Ingredient>() { IngredientA, IngredientB, IngredientC, },
        };

        public static Pizza PizzaC => new()
        {
            Name = "Meat",
            Description = "Some meat description",
            Price = 12,
            ImageLink = "Image link",
            SingleItemImageLink = "Single image link",
            Ingredients = new List<Ingredient>() { IngredientA, IngredientB, IngredientC, },
        };

        public static List<Pizza> AllPizzas => new () { PizzaA, PizzaB, PizzaC };
    }
}
