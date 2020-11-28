using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete.Models
{
    public class NeighborPostModel
    {
        public string Id { get; set; }
        public GeometryForNeighbor Geometry { get; set; }
    }
}
