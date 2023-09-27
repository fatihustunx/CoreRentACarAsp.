using Business.Abstracts;
using Business.BusinessRules;
using Business.BusinessRules.Conceretes;
using Business.Conceretes;
using DataAccess.Conceretes.EntityFramework;
using DataAccess.Conceretes.InMemory;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // IoC !!

            //InMemoryCar(carService);
            //EfCarBrandColor(carService);

            //UserCustomers();

            //NewMethod();

            //OperationsOfRental();

            Console.WriteLine("\nHello, World!");
        }

        private static void NewMethod()
        {
            ICarService carService = new CarManager(new EfCarDal());

            var result = carService.GetCarDetails();

            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine(item.Id + " : " + item.BrandName
                        + " : " + item.ColorName + " : " + item.DailyPrice);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }

        private static void OperationsOfRental()
        {
            RentalManager rentalManager = new RentalManager(new EfRentalDal(),new RentalBusinessRules(new EfRentalDal()));

            //rentalManager.Add(new Entities.Conceretes.Rental { Id = 3, CarId = 3, CustomerId = 1, RentDate = DateTime.Now });

            var result = rentalManager.GetAll();

            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine(item.Id);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }

        private static void UserCustomers()
        {
            UserManager userManager = new UserManager(new EfUserDal());
            userManager.Add(new Core.Entities.Conceretes.User { Id = 1, FirstName = "Fatih", LastName = "Üstün", Email = "abc@gmail.com" });

            CustomerManager customerManager = new CustomerManager(new EfCustomerDal());
            customerManager.Add(new Entities.Conceretes.Customer { Id = 1, UserId = 1, CompanyName = "Wistaster" });
            var result = customerManager.GetAll();
            foreach (var item in result.Data)
            {
                Console.WriteLine(item.CompanyName);
            }
        }

        private static void EfCarBrandColor(ICarService carService)
        {
            IBrandService brandService = new BrandManager(new EfBrandDal());
            IColorService colorService = new ColorManager(new EfColorDal());

            colorService.Add(new Entities.Conceretes.Color { Id = 1, Name = "Siyah" });
            colorService.Add(new Entities.Conceretes.Color { Id = 2, Name = "Mor" });
            brandService.Add(new Entities.Conceretes.Brand { Id = 1, Name = "BMW" });
            brandService.Add(new Entities.Conceretes.Brand { Id = 2, Name = "Audi" });

            carService.Add(new Entities.Conceretes.Car { Id = 1, BrandId = 1, ColorId = 1, ModelYear = 2001, DailyPrice = 200, Description = "Good BMW" });
            carService.Add(new Entities.Conceretes.Car { Id = 2, BrandId = 1, ColorId = 1, ModelYear = 2003, DailyPrice = 300, Description = "Nice BMW" });
            carService.Add(new Entities.Conceretes.Car { Id = 3, BrandId = 2, ColorId = 1, ModelYear = 2004, DailyPrice = 400, Description = "Excellent Audi" });
            carService.Add(new Entities.Conceretes.Car { Id = 4, BrandId = 2, ColorId = 2, ModelYear = 2005, DailyPrice = 500, Description = "Awesome Audi" });
        }

        private static void InMemoryCar(ICarService carService)
        {
            carService.Add(new Entities.Conceretes.Car { Id = 7, Description = "new car" });
            carService.Update(new Entities.Conceretes.Car { Id = 7, Description = "update car" });
            //Console.WriteLine(carService.GetById(7));
            carService.Delete(new Entities.Conceretes.Car { Id = 7 });
        }
    }
}