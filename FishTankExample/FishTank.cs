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

    class FishTank: IDisposable
    {
        public List<AquariumObject> items = new List<AquariumObject>();
        public string Name { get; set; }
        private Random rnd;  //so we get actually more or less random numbers

        //our fishtank constructor
        public FishTank(string name)
        {
            this.Name = name;
            rnd = new Random();
        }

        //Do fishtank stuff!  Give our fishies a chance to play.
        public void run()
        {
            foreach(var item in new List<AquariumObject>(items)) //note we wrap in a new list that we will iterate. otherwise we can't remove items.
            {
                if (item is Creature)
                {
                    var creature = item as Creature;
                    if (creature.alive)
                        creature.live();
                    else
                    {
                        System.Console.WriteLine("FISHTANK: eww a dead creature.  remove: " + creature.Name);
                        items.Remove(item);
                    }
                }
                else
                {
                    System.Console.WriteLine("FISHTANK: A " + item.Name + " doing what " + item.Name + "'s do.");
                }
            }
            
            System.Console.WriteLine("FISHTANK:  bubble... bubble...");

        }

        public void add(AquariumObject aobj)
        {
            if (aobj.isCompatibleFishTank(this))
            {
                aobj.MyFishTank = this;
                this.items.Add(aobj);
            }
            
        }

        /**
         * Call me to get an instance of Random object. Otherwise, if you make your own, in too quick a succession,
         *  the timer seed is identical and so you get identical random numbers (which isn't so helpful)
         */ 
        internal Random getRandom()
        {
            return rnd;
        }

        /**
         * Clean up everything when we're done.
         */ 
        public void Dispose()
        {
            System.Console.WriteLine("FISHTANK: Disposing of the tank of " + items.Count + " items.");

            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];
                
                if (item is Creature)
                {
                    var creature = item as Creature;
                    if (creature.alive)
                    {
                        creature.kill();
                        creature.log(" Flushed down the sink.");
                    }
                    else
                    {
                        item.log(" Was already dead - flushed down the sink.");
                    }
                }
                else
                {
                    item.log(" Pulled out of the tank.");
                }

                items[i] = null; //no need to items.Remove (which messes up our counter anyway)
                 
            }

            items = null;
            System.Console.WriteLine("FISHTANK: Tank is empty.");
        }
    }

    /**
    * Some kind of object that can be in the aquarium
    */
    abstract class AquariumObject
    {
        public string Name { get; set; }
        public FishTank MyFishTank { get; set; }

        public AquariumObject(string name)
        {
            Name = name;
        }

        /**
         * Is this a compatible fishtank for this AqO?
         */
        public virtual bool isCompatibleFishTank(FishTank fishTank)
        {
            //to start out with, all fishtanks are good.
            log("Cool, I like this aquarium.");
            return true;
        }


        public void log(string message)
        {
            System.Console.WriteLine(this.GetType().Name + "("+this.Name+"): " + message);
        }
    }
}
