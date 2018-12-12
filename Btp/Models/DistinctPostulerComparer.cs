using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Btp.Models
{
    public class DistinctPostulerComparer : IEqualityComparer<Postuler>
    {
        public bool Equals(Postuler x, Postuler y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(Postuler obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}