using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Business.Concrete.Comparers
{
    public class DoorComparer : IEqualityComparer<Door>
    {
        public bool Equals([AllowNull] Door x, [AllowNull] Door y)
        {
            return x.Id.Equals(y.Id);
        }

        public int GetHashCode([DisallowNull] Door obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
