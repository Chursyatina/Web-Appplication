namespace WebAPI.MockFactory.Tests.Data
{
    using System.Collections.Generic;
    using Domain.Models;

    public static class TestDoughs
    {
        public static Dough DoughA = new() { Name = "Traditional", PriceMultiplier = 1 };
        public static Dough DoughB = new() { Name = "Thin", PriceMultiplier = 2 };

        public static List<Dough> AllDoughs = new() { DoughA, DoughB };
    }
}
