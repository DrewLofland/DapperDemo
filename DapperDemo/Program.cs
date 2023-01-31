using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace DapperDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            string connString = config.GetConnectionString("DefaultConnection");
            IDbConnection conn = new MySqlConnection(connString);
            var repo = new DapperDepartmentRepository(conn);
            //Console.WriteLine("please enter a new department name");
            //var newDepartment = Console.ReadLine();
            //repo.InsertDepartment(newDepartment);
            //var departments = repo.GetAllDepartments(); 
            //foreach(var dept in departments)
            //{
            //    Console.WriteLine(dept.Name);
            //}
            var prodRepo = new DapperProductRepository(conn);
            var products = prodRepo.GetAllProducts();

            foreach (var product in products)
            {
                Console.WriteLine($"{product.ProductID} {product.Name} {product.Price}");

            }
           // CREATE PRODUCT
            Console.WriteLine("What is the new product name?");
            var prodName = (Console.ReadLine());

            Console.WriteLine("What is the price?");
            var prodPrice = double.Parse(Console.ReadLine());

            Console.WriteLine("What is the categoryID?");
            var prodCat = int.Parse(Console.ReadLine());

            prodRepo.CreateProduct(prodName, prodPrice, prodCat);

            products = prodRepo.GetAllProducts();
            foreach (var product in products)
            {
                Console.WriteLine($"{product.ProductID} {product.Name} {product.Price}");

            }


            //UPDATE PRODUCT
            Console.WriteLine("Enter productID to update");
            var prodID = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter updated name");
            prodName = (Console.ReadLine());

            prodRepo.UpdateProduct(prodID, prodName);

            products = prodRepo.GetAllProducts();
            foreach (var product in products)
            {
                Console.WriteLine($"{product.ProductID} {product.Name} {product.Price}");
            }

            //DELETE PRODUCT
            Console.WriteLine("What is the productID you want to delete?");
            prodID = int.Parse(Console.ReadLine());

            prodRepo.DeleteProduct(prodID);

            products = prodRepo.GetAllProducts();
            foreach (var product in products)
            {
                Console.WriteLine($"{product.ProductID} {product.Name} {product.Price}");
            }
        }
    }
}