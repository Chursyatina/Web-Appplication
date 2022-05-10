namespace Infrastructure.EF
{
    using System.Collections.Generic;
    using Application.Services;
    using Domain.Models;

    public static class BaseInitializer
    {
        public static void Initialize(this DatabaseContext context)
        {
            if (!context.Database.EnsureCreated())
            {
                return;
            }

            var ingredientList = new List<Ingredient>()
            {
                new Ingredient()
                {
                    Name = "Mozzarella ",
                    Price = 59,
                    ImageLink = "https://nemagaz.ru/wa-data/public/shop/products/16/19/1916/ImageLinks/1259/1259.970.jpg",
                    Pizzas = new List<Pizza>(),
                    IsAvailable = true,
                    IsObligatory = true,
                },
                new Ingredient()
                {
                    Name = "Pepperoni",
                    Price = 48,
                    ImageLink = "https://th.bing.com/th/id/OIP._qc-JyiqXuESTu4d2-Uv7wHaFn?pid=ImgDet&rs=1",
                    Pizzas = new List<Pizza>(),
                    IsAvailable = true,
                    IsObligatory = true,
                },
                new Ingredient()
                {
                    Name = "Black pepper",
                    Price = 30,
                    ImageLink = "https://th.bing.com/th/id/R.230f7f2997d8d4847bff0c8a9710284d?rik=4UxrVIIeEhI0eg&pid=ImgRaw",
                    Pizzas = new List<Pizza>(),
                    IsAvailable = true,
                    IsObligatory = false,
                },
                new Ingredient()
                {
                    Name = "Ham",
                    Price = 60,
                    ImageLink = "https://calorizator.ru/sites/default/files/ImageLinkcache/product_512/product/ham-1.jpg",
                    Pizzas = new List<Pizza>(),
                    IsAvailable = true,
                    IsObligatory = true,
                },
                new Ingredient()
                {
                    Name = "Dill",
                    Price = 25,
                    ImageLink = "https://th.bing.com/th/id/R.2918e9aea1dc8d68cfd9d75d5515b337?rik=46txHXTQt8PpJg&pid=ImgRaw",
                    Pizzas = new List<Pizza>(),
                    IsAvailable = true,
                    IsObligatory = false,
                },
                new Ingredient()
                {
                    Name = "Parsley",
                    Price = 24,
                    ImageLink = "https://th.bing.com/th/id/OIP.PTkRgM4Ghx5ze1OLYsd0mAHaHa?pid=ImgDet&rs=1",
                    Pizzas = new List<Pizza>(),
                    IsAvailable = true,
                    IsObligatory = false,
                },
                new Ingredient()
                {
                    Name = "Tomato",
                    Price = 34,
                    ImageLink = "https://th.bing.com/th/id/R.5f07433624fd630965f7b163824c78de?rik=dG2Xx1ovNKGZFg&pid=ImgRaw",
                    Pizzas = new List<Pizza>(),
                    IsAvailable = true,
                    IsObligatory = false,
                },
                new Ingredient()
                {
                    Name = "Fish",
                    Price = 54,
                    ImageLink = "https://ImageLinks6.alphacoders.com/529/529319.jpg",
                    Pizzas = new List<Pizza>(),
                    IsAvailable = true,
                    IsObligatory = true,
                },
                new Ingredient()
                {
                    Name = "Cheddar",
                    Price = 45,
                    ImageLink = "https://th.bing.com/th/id/R.3a0447f3ed4161f4a72d2e49a7e8312d?rik=BABv628NLdwREg&pid=ImgRaw",
                    Pizzas = new List<Pizza>(),
                    IsAvailable = true,
                    IsObligatory = false,
                },
            };

            var additionalIngredientsList = new List<AdditionalIngredient>()
            {
                new AdditionalIngredient() { Name = "Mozzarella ", Price = 59, ImageLink = "https://nemagaz.ru/wa-data/public/shop/products/16/19/1916/ImageLinks/1259/1259.970.jpg", PizzasVariations = new List<PizzaVariation>(), IsAvailable = true, },
                new AdditionalIngredient() { Name = "Pepperoni", Price = 48, ImageLink = "https://th.bing.com/th/id/OIP._qc-JyiqXuESTu4d2-Uv7wHaFn?pid=ImgDet&rs=1", PizzasVariations = new List<PizzaVariation>(), },
                new AdditionalIngredient() { Name = "Black pepper", Price = 30, ImageLink = "https://th.bing.com/th/id/R.230f7f2997d8d4847bff0c8a9710284d?rik=4UxrVIIeEhI0eg&pid=ImgRaw", PizzasVariations = new List<PizzaVariation>(), IsAvailable = true, },
                new AdditionalIngredient() { Name = "Ham", Price = 60, ImageLink = "https://calorizator.ru/sites/default/files/ImageLinkcache/product_512/product/ham-1.jpg", PizzasVariations = new List<PizzaVariation>(), IsAvailable = true, },
                new AdditionalIngredient() { Name = "Dill", Price = 25, ImageLink = "https://th.bing.com/th/id/R.2918e9aea1dc8d68cfd9d75d5515b337?rik=46txHXTQt8PpJg&pid=ImgRaw", PizzasVariations = new List<PizzaVariation>(), IsAvailable = true, },
                new AdditionalIngredient() { Name = "Parsley", Price = 24, ImageLink = "https://th.bing.com/th/id/OIP.PTkRgM4Ghx5ze1OLYsd0mAHaHa?pid=ImgDet&rs=1", PizzasVariations = new List<PizzaVariation>(), IsAvailable = true, },
                new AdditionalIngredient() { Name = "Tomato", Price = 34, ImageLink = "https://th.bing.com/th/id/R.5f07433624fd630965f7b163824c78de?rik=dG2Xx1ovNKGZFg&pid=ImgRaw", PizzasVariations = new List<PizzaVariation>(), IsAvailable = true, },
                new AdditionalIngredient() { Name = "Fish", Price = 54, ImageLink = "https://ImageLinks6.alphacoders.com/529/529319.jpg", PizzasVariations = new List<PizzaVariation>(), IsAvailable = true, },
                new AdditionalIngredient() { Name = "Cheddar", Price = 45, ImageLink = "https://th.bing.com/th/id/R.3a0447f3ed4161f4a72d2e49a7e8312d?rik=BABv628NLdwREg&pid=ImgRaw", PizzasVariations = new List<PizzaVariation>(), IsAvailable = true, },
            };

            var pizzaList = new List<Pizza>()
            {
                new Pizza()
                {
                    Name = "Pepperoni",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam condimentum ut lacus in egestas. Phasellus scelerisque sem molestie nulla sodales con",
                    Price = 0,
                    ImageLink = "https://th.bing.com/th/id/OIP.N9N9nxAB6IOE2vxuXMYMTwHaGI?pid=ImgDet&rs=1",
                    SingleItemImageLink = "https://th.bing.com/th/id/R.ade692772b8449f1a020671445e983d9?rik=FTaAAumCQE8W0w&pid=ImgRaw",
                    Ingredients = new List<Ingredient>() { ingredientList[0],  ingredientList[1],  ingredientList[2], },
                    IsAvailable = true,
                },
                new Pizza()
                {
                    Name = "4 cheese ",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam condim",
                    Price = 0,
                    SingleItemImageLink = "https://th.bing.com/th/id/OIP.-s22GLfbUmwlIg7dfbjNJAHaHa?pid=ImgDet&rs=1",
                    ImageLink = "https://th.bing.com/th/id/OIP.lU5tM2RWjQqWk6eWTo_GJAHaE8?pid=ImgDet&rs=1",
                    Ingredients = new List<Ingredient>() { ingredientList[0],  ingredientList[1],  ingredientList[2], ingredientList[6],  ingredientList[5], },
                    IsAvailable = true,
                },
                new Pizza()
                {
                    Name = "Meat",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam condim",
                    Price = 0,
                    SingleItemImageLink = "https://static8.depositphotos.com/1177973/861/i/450/depositphotos_8618524-stock-photo-delicious-pizza-with-seafood-on.jpg",
                    ImageLink = "https://th.bing.com/th/id/OIP.dm43n9JtXyiC09PsFrza7gHaGh?pid=ImgDet&rs=1",
                    Ingredients = new List<Ingredient>() { ingredientList[0],  ingredientList[1],  ingredientList[2], },
                    IsAvailable = true,
                },
                new Pizza()
                {
                    Name = "Fish",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam condimentum ut lacus in egestas. Phasellus scelerisque sem molestie nulla sodales con",
                    Ingredients = new List<Ingredient>() { ingredientList[1],  ingredientList[2],  ingredientList[3], ingredientList[8],  ingredientList[7], },
                    SingleItemImageLink = "https://th.bing.com/th/id/OIP.CF-HAAvaG4TJm_dddWzwRAHaGm?pid=ImgDet&rs=1",
                    ImageLink = "https://fb.ru/misc/i/gallery/38575/3146765.jpg",
                    Price = 0,
                    IsAvailable = true,
                },
                new Pizza()
                {
                    Name = "All inclusive",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam condimentum ut lacus in egestas. Phasellus scelerisque sem molestie nulla sodales con",
                    Ingredients = new List<Ingredient>() { ingredientList[1],  ingredientList[2],  ingredientList[3], ingredientList[4], ingredientList[5], ingredientList[6], ingredientList[8],  ingredientList[7], },
                    ImageLink = "https://fb.ru/misc/i/gallery/38575/3146765.jpg",
                    SingleItemImageLink = "https://st2.depositphotos.com/1177973/9738/i/950/depositphotos_97385780-stock-photo-tasty-pizza-decorated-with-mushrooms.jpg",
                    Price = 0,
                    IsAvailable = true,
                },
            };

            var orderStatusList = new List<OrderStatus>()
            {
                new OrderStatus() { Name = "Готовится" },
                new OrderStatus() { Name = "Готов" },
            };

            var doughTypeList = new List<Dough>()
            {
                new Dough() { Name = "Traditional", PriceMultiplier = 1 },
                new Dough() { Name = "Thin", PriceMultiplier = 2 },
            };

            var sizeList = new List<Size>()
            {
                new Size() { Name = "Small", PriceMultiplier = 1 },
                new Size() { Name = "Medium", PriceMultiplier = 1.5m },
                new Size() { Name = "Large", PriceMultiplier = 2 },
            };

            pizzaList.ForEach(e => e.Price = PriceCountingService.GetStartingPriceForPizza(e));

            ingredientList.ForEach(e => context.Ingredients.Add(e));
            additionalIngredientsList.ForEach(e => context.AdditionalIngredients.Add(e));
            pizzaList.ForEach(e => context.Pizzas.Add(e));
            orderStatusList.ForEach(e => context.OrderStatuses.Add(e));
            doughTypeList.ForEach(e => context.Doughs.Add(e));
            sizeList.ForEach(e => context.Sizes.Add(e));
            context.SaveChanges();
        }
    }
}
