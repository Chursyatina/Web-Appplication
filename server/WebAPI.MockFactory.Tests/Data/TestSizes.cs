namespace WebAPI.MockFactory.Tests.Data
{
    using System.Collections.Generic;
    using Domain.Models;

    public static class TestSizes
    {
        public static Size SizeA = new() { Name = "Small", PriceMultiplier = 1 };
        public static Size SizeB = new() { Name = "Medium", PriceMultiplier = 1.5m };
        public static Size SizeC = new() { Name = "Large", PriceMultiplier = 2 };

        public static List<Size> AllSizes => new() {SizeA, SizeB, SizeC };
    }
}
