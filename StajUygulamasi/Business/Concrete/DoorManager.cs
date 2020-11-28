using Business.Concrete.Exceptions;
using DataAccess.Abstracts;
using DataAccess.Concrete.Mongo;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class DoorManager
    {
        private static DoorManager _doorManager;

        private DoorDal _doorDal = MongoDoorDal.CreateInstance();
        private NeighborDal _neighborDal = MongoNeighborDal.CreateInstance();


        private DoorManager() { }

        public static DoorManager CreateInstance()
        {

            if (_doorManager == null)
            {
                _doorManager = new DoorManager();
            }


            return _doorManager;
        }


        public void Add(Door data)
        {
            var intersect = _neighborDal.GetIntersects(data);

            if(intersect == null)
            {
                throw new DoesntIntersectException("Eklemeye çalıştığınız kapı herhangi bir mahalleye ait olmalıdır.");
            }
            data.NeighborCode = intersect.Id;
            _doorDal.Add(data);
        }

        public void Delete(Door data)
        {
            _doorDal.Delete(data);
        }
        public void Update(Door data)
        {
            var intersect = _neighborDal.GetIntersects(data);

            //if (intersect == null)
            //{
            //    throw new DoesntIntersectException("Güncellemeye çalıştığınız kapı herhangi bir mahalleye ait olmalıdır.");
            //}
            data.NeighborCode = intersect != null ? intersect.Id: "";

            _doorDal.Update(data);
        }
        public Door Get(Door data)
        {
            return String.IsNullOrEmpty(data.Id) ? _doorDal.GetWithGeometry(data) : _doorDal.GetWithId(data);
        }
        public List<Door> GetAll()
        {
            return _doorDal.GetAll();
        }
        public List<Door> GetDoorsWithNeighborCode(string neighborCode)
        {
            return _doorDal.GetDoorsWithNeighborCode(neighborCode);
        }
        public List<Door> GetDoorsInNeighBor(Neighbor neighbor)
        {
            return _doorDal.GetDoorsInNeighBor(neighbor);
        }
    }
}
