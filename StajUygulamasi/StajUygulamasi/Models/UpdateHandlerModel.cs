using Entities.Concrete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StajUygulamasi.Models
{
    public class UpdateHandlerModel
    {
        public UpdateHandlerModel()
        {
            Points = new List<DoorPostModel>();
            Polygons = new List<NeighborPostModel>();
        }

        public List<DoorPostModel> Points { get; set; }
        public List<NeighborPostModel> Polygons { get; set; }
    }
}
