using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
   public class GeometryForDoor
    {
        [BsonElement("type")]
        public string Type { get; set; }
        [BsonElement("coordinates")]
        public double[] Coordinates { get; set; }

    }
}
