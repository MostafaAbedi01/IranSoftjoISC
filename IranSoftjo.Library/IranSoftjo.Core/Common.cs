using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mehr
{
    public static class Common
    {
        public static IEnumerable<int> Numbers(int minInclusive, int maxInclusive)
        {
            for (int i = minInclusive; i <= maxInclusive; i++)
                yield return i;
        }
    }
}
