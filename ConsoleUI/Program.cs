using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestProductManager();
            //TestCategoryManager();

        }

        //private static void TestCategoryManager()
        //{
        //    CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
        //    foreach (var c in categoryManager.GetAll())
        //    {
        //        Console.WriteLine(c.CategoryName);

        //    }
        //}

        //private static void TestProductManager()
        //{
        //    ProductManager productManager = new ProductManager(new EfProductDal());
        //    foreach (var product in productManager.GetProductDetails())
        //    {
        //        Console.WriteLine(product.ProductName + " " + product.CategoryName  );
        //    }
        //}
    }
}
