using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCTS.DST {
    class DSTHelper
    {

        private static int counterguid = 1000000;

        public static string GenerateNewGuid()
        {
            return (counterguid++).ToString();
        }

    }
}
