using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete.Exceptions
{
    public class IntersectException : Exception
    {
        public IntersectException(string message) : base(message)  { } 
    }
}
