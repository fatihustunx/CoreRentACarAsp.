using Business.Abstracts;
using Business.Conceretes;
using DataAccess.Conceretes.InMemory;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ICarService carService = new CarManager(new InMemoryCarDal());

            carService.Add(new Entities.Conceretes.Car {Id = 7,Description="new car"});
            carService.Update(new Entities.Conceretes.Car { Id = 7, Description = "update car" });
            Console.WriteLine(carService.GetById(7)); 
            carService.Delete(new Entities.Conceretes.Car { Id = 7 });

            foreach (var item in carService.GetAll())
            {
                Console.WriteLine(item.Id + " : " + item.Description);
            }

            Console.WriteLine("\nHello, World!");
        }
    }
}
