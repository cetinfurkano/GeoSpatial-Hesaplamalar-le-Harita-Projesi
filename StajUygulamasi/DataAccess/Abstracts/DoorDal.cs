using Entities.Concrete;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstracts
{
    public abstract class DoorDal : DatabaseOperations<Door>
    {
        public abstract List<Door> GetDoorsWithNeighborCode(string neighborCode);
        public abstract List<Door> GetDoorsInNeighBor(Neighbor neighbor);
    }
}
