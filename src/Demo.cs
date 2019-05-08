using System;
using System.Collections.Generic;
using System.Linq;
using RandomNameGeneratorLibrary;

namespace SeqDemo
{
    class Demo
    {
        private readonly IPersonNameGenerator _personNameGenerator;
        private readonly Random _random;

        public Demo(IPersonNameGenerator personNameGenerator, Random random)
        {
            _personNameGenerator = personNameGenerator;
            _random = random;
        }

        private List<string> Users =>
            _personNameGenerator.GenerateMultipleFirstAndLastNames(10)
                .Select(x => $"{x.Replace(" ", ".")}@nowhere.com").ToList();


        private List<(string Name, decimal Cost, decimal Price)> Products =>
            new List<(string, decimal, decimal)>
            {
                ("Bowling Ball", 43.37m, 59.99m),
                ("Golf Shirt", 27.65m, 49.99m),
                ("Bobble Head", 6.77m, 14.99m),
                ("Comically Oversized Coffee Mug", 12.12m, 19.99m),
                ("Fuzzy Socks", 2.22m, 9.99m)
            };

        private (string Name, decimal Cost, decimal Price) RandomProduct => Products[_random.Next(0, Products.Count - 1)];
        
        public Transaction RandomTransaction => GetTransaction();

        private Transaction GetTransaction()
        {
            var (name, cost, price) = RandomProduct;
            return new Transaction
            {
                User = Users[_random.Next(0, Users.Count - 1)],
                Action = _random.Next(0, 100) > 97 ? "Returned" : "Purchased",
                Product = name,
                Price = price,
                Cost = cost
            };
        }
    }
}