using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mehr
{
    public class BigConstant
    {
        public const int One6Zero = 1000 * 1000;
        public const int One7Zero = One6Zero * 10;
        public const int One8Zero = One6Zero * 100;
        public const int One9Zero = One6Zero * 1000;

        public const int Max6Digit = One6Zero - 1;
        public const int Max7Digit = One7Zero - 1;
        public const int Max8Digit = One8Zero - 1;
        public const int Max9Digit = One9Zero - 1;

        public const int Min6Digit = 100 * 1000;
        public const int Min7Digit = One6Zero;
        public const int Min8Digit = One7Zero;
        public const int Min9Digit = One8Zero;
    }
}
