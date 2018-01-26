using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCTS.Math;

namespace MCTS.DST {
    class DSTHelper
    {

        private static int counterguid = 1000000;

        public static string GenerateNewGuid()
        {
            return (counterguid++).ToString();
        }

        public static int getSquaredDistance(Vector2i a, Vector2i b)
        {
            var distanceVector = b - a;
            var x = distanceVector.x;
            var y = distanceVector.y;
            return x * x + y * y;
        }

        public static double getRealDistance(Vector2i a, Vector2i b)
        {
            return System.Math.Sqrt(getSquaredDistance(a,b));
        }
    }
}
