using Business.Concrete.Comparers;
using Business.Concrete.Exceptions;
using DataAccess.Abstracts;
using DataAccess.Concrete.Mongo;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Business.Concrete
{
    public class NeighborManager
    {
        
        private static NeighborManager _neighborManager;

        private NeighborDal _neighborDal = MongoNeighborDal.CreateInstance();
        private DoorDal _doorDal = MongoDoorDal.CreateInstance();

        private NeighborManager() { }

        public static NeighborManager CreateInstance()
        {
            if (_neighborManager == null)
            {
                _neighborManager = new NeighborManager();
            }

            return _neighborManager;
        }


        public void Add(Neighbor data)
        {
                if (_neighborDal.GetIntersects(data) != null)
                {
                    throw new IntersectException("Eklemeye çalıştığınız mahalle, herhangi bir mahalle ile kesişmemelidir.");
                }
            
            _neighborDal.Add(data);
            var addedData = Get(data);

            var newInside = _doorDal.GetDoorsInNeighBor(addedData);

            foreach (var door in newInside)
            {
                door.NeighborCode = addedData.Id;
                _doorDal.Update(door);
            }

        }
        public void Delete(Neighbor data)
        {
            var alreadyInside = _doorDal.GetDoorsWithNeighborCode(data.Id);

            foreach (var door in alreadyInside)
            {
                _doorDal.Delete(door);
            }
            _neighborDal.Delete(data);
        }
        public void Update(Neighbor data)
        {
            var newInside = _doorDal.GetDoorsInNeighBor(data);
           
            //if (mode != Mode.HandlerActive)
            //{
            //    if (_neighborDal.GetIntersects(data) != null)
            //    {
            //        throw new IntersectException("Güncellemeye çalıştığınız mahalle, herhangi bir mahalle ile kesişmemelidir.");
            //    }

            //    var alreadyInside = _doorDal.GetDoorsWithNeighborCode(data.Id);
            //    var notInside = alreadyInside.Except(newInside, new DoorComparer());

            //    if (notInside.Count() > 0)
            //    {
            //        throw new OrphanDoorException("Kapıları mahallesiz bırakarak güncelleme yapamazsınız.");
            //    }
            //}

            foreach (var door in newInside)
            {
                door.NeighborCode = data.Id;
                _doorDal.Update(door);
            }

            _neighborDal.Update(data);

        }
        public Neighbor Get(Neighbor data)
        {
            return String.IsNullOrEmpty(data.Id) ? _neighborDal.GetWithGeometry(data) : _neighborDal.GetWithId(data);
        }
        public List<Neighbor> GetAll()
        {
            return _neighborDal.GetAll();
        }
        public Neighbor GetIntersects(Door door)
        {
            return _neighborDal.GetIntersects(door);
        }

        public Neighbor GetIntersects(Neighbor neighbor)
        {
            return _neighborDal.GetIntersects(neighbor);
        }
    }
}
