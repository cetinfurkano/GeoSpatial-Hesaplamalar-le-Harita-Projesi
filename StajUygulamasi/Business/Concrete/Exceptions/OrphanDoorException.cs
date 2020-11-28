using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete.Exceptions
{
    public class OrphanDoorException: Exception
    {
        public OrphanDoorException(string message) : base(message) { }
    }
}
