using System;
using System.Collections.Generic;
using System.Linq;

namespace TSP2
{
    public static class Extensions
    {
        public static IList<T> CloneList<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }       
    }
}
