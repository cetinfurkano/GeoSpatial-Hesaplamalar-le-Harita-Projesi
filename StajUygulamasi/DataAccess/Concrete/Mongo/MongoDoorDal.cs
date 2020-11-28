using DataAccess.Abstracts;
using Entities.Concrete;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.Mongo
{
    public class MongoDoorDal : DoorDal
    {
        private IMongoDatabase db;
        private IMongoClient client;
        private IMongoCollection<Door> collection;


        private static MongoDoorDal _mongoDoorDal;

        private MongoDoorDal()
        {
            client = new MongoClient();
            db = client.GetDatabase("Map");
            collection = db.GetCollection<Door>("Doors");
        }

        public static MongoDoorDal CreateInstance()
        {

            if (_mongoDoorDal == null)
            {
                _mongoDoorDal = new MongoDoorDal();
            }

            return _mongoDoorDal;
        }

        public override void Add(Door data)
        {
            collection.InsertOne(data);
         }

        public override void Delete(Door data)
        {
            collection.DeleteOne(d => d.Id == data.Id);
        }

        public override Door GetWithGeometry(Door data)
        {
            return collection.Find(d => d.Geometry.Coordinates == data.Geometry.Coordinates).FirstOrDefault();
        }
        public override Door GetWithId(Door data)
        {
            return collection.Find(d => d.Id == data.Id).FirstOrDefault();
        }

        public override List<Door> GetAll()
        {
            return collection.Find(new BsonDocument()).ToList();
        }

        public override void Update(Door data)
        {
            var result = collection.ReplaceOne
                 (
                 new BsonDocument("_id", data.Id),
                 data,
                 new ReplaceOptions { IsUpsert = true }
                 );
        }

        public override List<Door> GetDoorsWithNeighborCode(string neighborCode)
        {
            return collection.Find(d => d.NeighborCode == neighborCode).ToList();
        }

        public override List<Door> GetDoorsInNeighBor(Neighbor neighbor)
        {
            BsonDocument query = BsonDocument.Parse("{'geometry': {'$geoWithin': {'$geometry': "+neighbor.Geometry.ToBsonDocument() +"}}}");
            return collection.Find(query).ToList();
        }
    }
}
