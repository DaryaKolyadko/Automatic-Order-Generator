using System;
using System.Collections.Generic;

namespace AutomaticOrderGeneration.Util
{
    class DoubleComparator: IComparer<Double>
    {
        private static double permissableError = Math.Pow(10, -10);
        public int Compare(double x, double y)
        {
            double diff = Math.Abs(x - y);

            if (diff < permissableError | diff.Equals(permissableError))
            {
                return 0;
            }

            if (x - y > permissableError)
            {
                return 1;
            }

            return -1;
        }
    }
}
