namespace Linq1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Restriction Operators
            var outOfStockProducts = ListGenerator.ProductsList.Where(p => p.UnitsInStock == 0);
            var expensiveInStockProducts = ListGenerator.ProductsList.Where(p => p.UnitsInStock > 0 && p.UnitPrice > 3);
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            var shortNamedDigits = digits.Where(d => d.Length < Array.IndexOf(digits, d));
            #endregion

            #region Element Operators
            var firstOutOfStock = ListGenerator.ProductsList.FirstOrDefault(p => p.UnitsInStock == 0);
            var firstExpensiveProduct = ListGenerator.ProductsList.FirstOrDefault(p => p.UnitPrice > 1000);
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var secondNumberGreaterThanFive = numbers.Where(n => n > 5).Skip(1).FirstOrDefault();
            #endregion

            #region Aggregate Operators
            var oddNumbersCount = numbers.Count(n => n % 2 != 0);
            var customerOrdersCount = ListGenerator.CustomersList.Select(c => new { c.CustomerName, OrderCount = c.Orders.Length });
            var categoryProductCount = ListGenerator.ProductsList.GroupBy(p => p.Category).Select(g => new { Category = g.Key, Count = g.Count() });
            var totalSum = numbers.Sum();
            #endregion

            #region Ordering Operators
            var sortedProductsByName = ListGenerator.ProductsList.OrderBy(p => p.ProductName);
            var sortedProductsByStock = ListGenerator.ProductsList.OrderByDescending(p => p.UnitsInStock);
            var sortedDigits = digits.OrderBy(d => d.Length).ThenBy(d => d);
            var sortedProductsByCategoryPrice = ListGenerator.ProductsList.OrderBy(p => p.Category).ThenByDescending(p => p.UnitPrice);
            #endregion

            #region Transformation Operators
            var productNames = ListGenerator.ProductsList.Select(p => p.ProductName);
            var wordVariations = digits.Select(w => new { Upper = w.ToUpper(), Lower = w.ToLower() });
            var productDetails = ListGenerator.ProductsList.Select(p => new { p.ProductName, Price = p.UnitPrice });
            var numbersWithIndex = numbers.Select((n, index) => new { Number = n, MatchesIndex = n == index });
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };
            var pairs = from a in numbersA from b in numbersB where a < b select new { a, b };
            var ordersLessThan500 = ListGenerator.CustomersList.SelectMany(c => c.Orders).Where(o => o.Total < 500);
            var ordersFrom1998 = ListGenerator.CustomersList.SelectMany(c => c.Orders).Where(o => o.OrderDate.Year >= 1998);
            #endregion


            Console.WriteLine("Out of stock products:");
            foreach (var product in outOfStockProducts) Console.WriteLine(product.ProductName);

            Console.WriteLine("\nFirst out of stock product:");
            Console.WriteLine(firstOutOfStock?.ProductName ?? "None");

            Console.WriteLine("\nExpensive in-stock products (> 3.00 per unit):");
            foreach (var product in expensiveInStockProducts) Console.WriteLine(product.ProductName);

            Console.WriteLine("\nShort named digits:");
            foreach (var digit in shortNamedDigits) Console.WriteLine(digit);

            Console.WriteLine("\nSecond number greater than 5:");
            Console.WriteLine(secondNumberGreaterThanFive);

            Console.WriteLine("\nCount of odd numbers:");
            Console.WriteLine(oddNumbersCount);

            Console.WriteLine("\nOrder count per customer:");
            foreach (var customer in customerOrdersCount) Console.WriteLine($"{customer.CustomerName}: {customer.OrderCount} orders");

            Console.WriteLine("\nCategory product count:");
            foreach (var category in categoryProductCount) Console.WriteLine($"{category.Category}: {category.Count} products");

            Console.WriteLine("\nTotal sum of numbers:");
            Console.WriteLine(totalSum);

            Console.WriteLine("\nOrders with total less than 500:");
            foreach (var order in ordersLessThan500) Console.WriteLine($"Order ID: {order.OrderID}, Total: {order.Total}");

            Console.WriteLine("\nOrders from 1998 or later:");
            foreach (var order in ordersFrom1998) Console.WriteLine($"Order ID: {order.OrderID}, Date: {order.OrderDate.ToShortDateString()}");

            Console.WriteLine("\nPairs where a < b:");
            foreach (var pair in pairs) Console.WriteLine($"{pair.a} is less than {pair.b}");

            Console.ReadLine();
        }
    }
}
