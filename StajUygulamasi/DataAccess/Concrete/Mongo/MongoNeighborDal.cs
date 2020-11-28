using DataAccess.Abstracts;
using Entities.Concrete;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.Mongo
{
    public class MongoNeighborDal : NeighborDal
    {

        private IMongoDatabase db;
        private IMongoClient client;
        private IMongoCollection<Neighbor> collection;

        private static MongoNeighborDal _mongoDistrictDal;



        private MongoNeighborDal()
        {
            client = new MongoClient();
            db = client.GetDatabase("Map");
            collection = db.GetCollection<Neighbor>("Neighbor");
        }

        public static MongoNeighborDal CreateInstance()
        {
            if (_mongoDistrictDal == null)
            {
                _mongoDistrictDal = new MongoNeighborDal();
            }

            return _mongoDistrictDal;
        }

        public override void Add(Neighbor data)
        {
            collection.InsertOne(data);
        }

        public override void Delete(Neighbor data)
        {
            collection.DeleteOne(d => d.Id == data.Id);
        }

        public override Neighbor GetWithId(Neighbor data)
        {
            return collection.Find(d => d.Id == data.Id).FirstOrDefault();
        }
        public override Neighbor GetWithGeometry(Neighbor data)
        {
            return collection.Find(d => d.Geometry.Coordinates == data.Geometry.Coordinates).FirstOrDefault();
        }

        public override List<Neighbor> GetAll()
        {
            return collection.Find(new BsonDocument()).ToList();
        }

        public override void Update(Neighbor data)
        {
            var result = collection.ReplaceOne
                (
                new BsonDocument("_id", data.Id),
                data,
                new ReplaceOptions { IsUpsert = true }
                );
        }

        public override Neighbor GetIntersects(Door door)
        {
            BsonDocument query = BsonDocument.Parse("{'geometry': {'$geoIntersects': {'$geometry':" + door.Geometry.ToBsonDocument() + "}}}");
            return collection.Find(query).FirstOrDefault(); 
        }

        public override Neighbor GetIntersects(Neighbor neighbor)
        {
            BsonDocument query = BsonDocument.Parse("{ '$and': [{'geometry': {'$geoIntersects': {'$geometry':" + neighbor.Geometry.ToBsonDocument() + "}}}, {'_id': {'$not': {'$eq': '"+neighbor.Id+"'}}}]}");
            return collection.Find(query).FirstOrDefault();
        }
    }
}
