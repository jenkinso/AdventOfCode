using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Puzzles.Models
{
    class Lanternfish
    {
        public sbyte InternalTimer;
        
        private const sbyte newFishTimerStart = 8;
        private const sbyte oldFishTimerStart = 6;
        
        public Lanternfish()
        {
            InternalTimer = newFishTimerStart;
        }

        public Lanternfish(sbyte timerValue)
        {
            InternalTimer = timerValue;
        }

        public bool IncrementDay()
        {
            bool spawnNewFish = false;

            InternalTimer--;

            if (InternalTimer < 0)
            {
                InternalTimer = oldFishTimerStart;
                spawnNewFish = true;
            }

            return spawnNewFish;
        }
    }
}
