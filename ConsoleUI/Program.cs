using Business.Abstracts;
using Business.Conceretes;
using DataAccess.Conceretes.EntityFramework;
using DataAccess.Conceretes.InMemory;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ICarService carService = new CarManager(new EfCarDal(),new CarBusinessRules());
            //IBrandService brandService = new BrandManager(new EfBrandDal());
            //IColorService colorService = new ColorManager(new EfColorDal());

            //carService.Add(new Entities.Conceretes.Car {Id = 7,Description="new car"});
            //carService.Update(new Entities.Conceretes.Car { Id = 7, Description = "update car" });
            //Console.WriteLine(carService.GetById(7)); 
            //carService.Delete(new Entities.Conceretes.Car { Id = 7 });

            //colorService.Add(new Entities.Conceretes.Color { Id = 1, Name = "Siyah" });
            //colorService.Add(new Entities.Conceretes.Color { Id = 2, Name = "Mor" });
            //brandService.Add(new Entities.Conceretes.Brand { Id = 1, Name = "BMW" });
            //brandService.Add(new Entities.Conceretes.Brand { Id = 2, Name = "Audi" });

            //carService.Add(new Entities.Conceretes.Car { Id = 1, BrandId = 1, ColorId = 1, ModelYear = 2001, DailyPrice = 200, Description = "Good BMW" });
            //carService.Add(new Entities.Conceretes.Car { Id = 2, BrandId = 1, ColorId = 1, ModelYear = 2003, DailyPrice = 300, Description = "Nice BMW" });
            //carService.Add(new Entities.Conceretes.Car { Id = 3, BrandId = 2, ColorId = 1, ModelYear = 2004, DailyPrice = 400, Description = "Excellent Audi" });
            //carService.Add(new Entities.Conceretes.Car { Id = 4, BrandId = 2, ColorId = 2, ModelYear = 2005, DailyPrice = 500, Description = "Awesome Audi" });


            foreach (var item in carService.GetAll())
            {
                Console.WriteLine(item.Id + " : " + item.Description);
            }

            Console.WriteLine("\nHello, World!");
        }
    }
}
