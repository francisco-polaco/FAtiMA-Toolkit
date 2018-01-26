using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCTS.DST.WorldModels;

namespace MCTS.DST.Objects {
    public class ClockDate
    {
        public int Day;
        public int Second;

        public void applySeconds(int numberSeconds) {
            var nextSec = numberSeconds + Second;
            Day = (nextSec / Clock.SECONDS_IN_DAY) + Day;
            Second = nextSec % Clock.SECONDS_IN_DAY;
        }
    }
}
