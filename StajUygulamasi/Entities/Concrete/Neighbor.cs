using Entities.Abstracts;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    [BsonIgnoreExtraElements]
    public class Neighbor : IEntity
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
         public string Id { get; set; }
        [BsonElement("neighborName")]
        public string NeighborName { get; set; }
        [BsonElement("geometry")]
        public GeometryForNeighbor Geometry { get; set; }

    }
}
