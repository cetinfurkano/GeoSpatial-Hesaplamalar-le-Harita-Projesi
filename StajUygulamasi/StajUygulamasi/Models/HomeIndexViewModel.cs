using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StajUygulamasi.Models
{
    public class HomeIndexViewModel
    {
        public List<Neighbor> neighbors { get; set; }
        public List<Door> doors { get; set; }
    }
}
