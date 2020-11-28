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
    public class Door : IEntity
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
         public string Id { get; set; }
        [BsonElement("neighborCode")]
        public string NeighborCode { get; set; }
        [BsonElement("doorCode")]
        public int DoorCode { get; set; }
        [BsonElement("geometry")]
        public GeometryForDoor Geometry { get; set; }
        
    }
}
