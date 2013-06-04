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

   
    abstract class Creature: AquariumObject
    {
        public Creature(string name) : base(name) 
        {
            alive = true;
        } 

        public bool alive { get; set; }

        abstract public void live();
        abstract public void eat();
        
        public void kill()
        {
            alive = false;
        }

        public void starve()
        {
            log("Arggh... I STARVED TO DEATH!");
            alive = false;
        }
    }

    abstract class Fish: Creature
    {
        public Fish(string name) : base(name) {}
        abstract public void swim();
    }

    abstract class Snail: Creature
    {
        public Snail(string name) : base(name) { }
        abstract public void slide();
    }
    
    interface ICarnivore
    {
        void eatMeat();
    }

    interface IHerbivore
    {
        void eatVeggies();
    }

    interface IFishivore
    {
        void eatOtherFish();
    }


    class Trout: Fish, ICarnivore
    {
        public Trout(string name) : base(name) {}

        public override void swim()
        {
            log("><SWIM>");
        }

        public override void eat()
        {
            this.eatMeat();
        }

        public void eatMeat()
        {
            log("><EAT>");

        }

        public override void live()
        {
            this.eat();
            this.swim();
        }
    }

    class Pirhana: Fish, ICarnivore, IFishivore
    {
        private const int MAX_HUNGRIES_BEFORE_STARVING = 13;
        private const int CHANCE_OF_ESCAPING = 7;  // smaller # gives less chance of escape!
        private const int MAX_NUM_PIRHANAS_PER_TANK = 2;

        private int num_hungries;
        public Pirhana(string name) : base(name) { }
        public override void swim()
        {
            log("><<SWIM>>");
        }

        public override void eat()
        {
            eatMeat();
        }

        public override void live()
        {
            swim();
            eatOtherFish();
        }

        #region ICarnivore Members

        public void eatMeat()
        {
            eatOtherFish();
        }

        #endregion

        /**
         * Make sure we don't have too many pirhana's!  we can only be with one other.
         */ 
        public override bool isCompatibleFishTank(FishTank fishTank)
        {
            int num_pirhanas = 0;
            bool is_compatible = true;

            foreach (var item in fishTank.items)
            {
                if (item is Pirhana)
                    num_pirhanas++;
            }

            if (num_pirhanas >= MAX_NUM_PIRHANAS_PER_TANK)
            {
                is_compatible = false;
                log(" NOOOOOOOOOOOOOO! don't put me in there with those other Pirhanas! Only 2 allowed.");
            }
            else
                is_compatible = base.isCompatibleFishTank(fishTank);
            

            return is_compatible;
        }

        #region IFishivore Members

        public void eatOtherFish()
        {
            log("><<EAT>>");

            int idx = MyFishTank.getRandom().Next(0, MyFishTank.items.Count);

            var item = MyFishTank.items[idx];
            if(item is Creature)
            {
                var creature = item as Creature;
                if (creature is IFishivore)
                {
                    //don't eat others like us
                }
                else if (creature is Snail)
                {
                    log(" YUCK! Snails");
                }
                else if(creature.alive)
                {
                    log(this.Name + " >>> chasing >>> " + creature.Name);
                    Random rnd = MyFishTank.getRandom();
                    if (rnd.Next(1, CHANCE_OF_ESCAPING) == 2) //give them one chance in 6 to escape!
                    {
                        log(this.Name + " >>> eating >>> " + creature.Name + " YUM!");
                        creature.kill();
                        return; //only eat one...
                    }

                    log("missed!  blast it.");
                }
            }
            log(" I guess I'm going hungry.");
            num_hungries++;

            if (num_hungries > MAX_HUNGRIES_BEFORE_STARVING)
            {
                this.starve();
            }
            
        }

        #endregion
    }

    class Reticulata : Snail
    {
        public Reticulata(string name) : base(name) { }

        public override void slide()
        {
            log("And I leave a trail of slime.");
        }

        public override void live()
        {
            slide();
            eat();
        }

        public override void eat()
        {
            log("Yummy slime!");
        }
    }


}
