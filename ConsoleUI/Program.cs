using Business.Concrete;
using Business.Constants;
using DataAccess.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {

            RentalsAdd();



        }
        private static void AddManager()
        {
            CarManager carmanager = new CarManager(new EfCarDAL());
            BrandManager brandmanager = new BrandManager(new EfBrandDAL());
            ColorManager colormanager = new ColorManager(new EfColorDAL());



            colormanager.Add(new Color { ColorId = 20, ColorName = "Red" });
            brandmanager.Add(new Brand { BrandId = 20, BrandName = "Hyunadi" });
            carmanager.Add(new Car { Id = 20, BrandId = 20, ColorId = 20, DailyPrice = 15547100, Description = "te11miz", ModelYear = 2019 });
        }
        private static void GetCarDetails()
        {

            CarManager carManager = new CarManager(new EfCarDAL());
            foreach (var item in carManager.GetCarDetails().Data)
            {
                Console.WriteLine(item.BrandName + " " + item.ColorName + " " + item.DailyPrice + " " + item.CarName);
            }
        }
        private static void GetAll()
        {
            CarManager carManager = new CarManager(new EfCarDAL());
            foreach (var item in carManager.GetAll().Data)
            {
                Console.WriteLine(item.Description);
            }
        }
        private static void RentalsAdd()
        {
            RentalsManager manager = new RentalsManager(new EfRentalsDAL());
            manager.Add(new Rentals { CarId = 2, CustomerId = 2, RentDate = DateTime.Now.AddDays(10 / 05 / 1998 ), ReturnDate = DateTime.Now.AddDays(10 / 8 / 2000).Date ,RentalId=2});
            Console.WriteLine(Messages.Success);
        }

    }
}