using ProjectSpaceship.Spaceships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSpaceship
{
    internal class Player
    {
        private string name;
        private int level;
        private double experience;
        private double credits;
        private Spaceship spaceship;

        public Player(string name)
        {
            this.name = name;
            this.level = 0;
            this.experience = 0;
            this.credits = 100;
        }

        public string GetName()
        {
            return name;
        }
        public int GetLevel()
        {
            return level;
        }
        public void SetLevel(int level)
        {
            this.level = level;
        }
        public double GetExperience()
        {
            return experience;
        }
        public void SetExperience(double experience)
        {
            this.experience = experience;
        }
        public void IncreaseExperience(double experienceToAdd)
        {
            this.experience += experience;
        }
        public void LevelUp()
        {
            level++;
            experience = 0;
        }
        public double GetCredits()
        {
            return credits;
        }
        public void SetCredits(double credits)
        {
            this.credits = credits;
        }
        public void IncreaseCredits(double amountToAdd)
        {
            this.credits += amountToAdd;
        }
        public void DecreaseCredits(double amountToSub)
        {
            this.credits -= amountToSub;
        }
        public Spaceship GetSpaceship()
        {
            return spaceship;
        }
        public void SetSpaceship(Spaceship spaceship)
        {
            this.spaceship = spaceship;
        }
     
    }
}
