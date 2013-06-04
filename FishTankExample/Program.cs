using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * Ken Burcham's fiddle with C# objects demo
 * 5/30/2013
 */
namespace FishTankExample
{
    class Program
    {
        private const int NUM_TIMES_TO_RUN = 15;
        private const int HOW_LONG_BETWEEN_RUNS = 1000; //ms

        static void Main(string[] args)
        {
            using (FishTank ft = new FishTank("Aquarium")) //using "using" will call dispose for us when done.
            {

                System.Console.WriteLine("Hello world!");
                System.Console.WriteLine("... and we have a FishTank: " + ft.Name);

                ft.add(new Trout("Billy1"));
                ft.add(new Trout("Billy2"));
                ft.add(new Trout("Billy3"));
                ft.add(new Trout("Billy4"));
                ft.add(new Pirhana("Jimmy1"));
                ft.add(new Pirhana("Jimmy2"));
                ft.add(new Pirhana("Jimmy3"));
                ft.add(new Rock("Lumpy1"));
                ft.add(new Reticulata("Slurpy1"));

                for (int i = 0; i < NUM_TIMES_TO_RUN; i++)
                {
                    ft.run();
                    System.Threading.Thread.Sleep(HOW_LONG_BETWEEN_RUNS);
                }
            }

            Console.ReadKey(); //by the time we get here, the tank is disposed.
        }
    }
}
