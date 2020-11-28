using Entities.Concrete;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstracts
{
    public abstract class NeighborDal : DatabaseOperations<Neighbor>
    {
        public abstract Neighbor GetIntersects(Door door);
        public abstract Neighbor GetIntersects(Neighbor neighbor);
    }
}
