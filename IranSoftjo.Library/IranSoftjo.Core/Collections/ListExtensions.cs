using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mehr.Collections
{
   public static class ListExtensions
    {
       public static List<T> AddItems<T>(this List<T> list, params T[] items)
       {
           list.AddRange(items);
           return list;
       }
    }
}
