using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete.Exceptions
{
    public class DoesntIntersectException : Exception
    {
        public DoesntIntersectException(string message) : base(message) { }
        
    }
}
