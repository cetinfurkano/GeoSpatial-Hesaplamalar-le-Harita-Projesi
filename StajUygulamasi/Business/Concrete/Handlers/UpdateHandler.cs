using Business.Concrete.Comparers;
using Business.Concrete.Exceptions;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete.Handlers
{
    public static class UpdateHandler
    {
        private static List<Door> dataDoors = new List<Door>();
        private static List<Neighbor> dataNeighbors = new List<Neighbor>();

        private static DoorManager doorManager = DoorManager.CreateInstance();
        private static NeighborManager neighborManager = NeighborManager.CreateInstance();

        public static void Control(List<Door> doors, List<Neighbor> neighbors)
        {
            foreach (var door in doors)
            {
                dataDoors.Add(doorManager.Get(door));
                doorManager.Update(door);
            }

            foreach (var neighbor in neighbors)
            {
                dataNeighbors.Add(neighborManager.Get(neighbor));
                neighborManager.Update(neighbor);
            }

                Handle(doors);
                Handle(neighbors);
        }
        public static void TakeOver()
        {
            foreach (var door in dataDoors)
            {
                doorManager.Update(door);
            }

            foreach (var neighbor in dataNeighbors)
            {
                neighborManager.Update(neighbor);
            }
        }

        private static void Handle(List<Door> doors)
        {
            foreach (var door in doors)
            {
                var intersect = neighborManager.GetIntersects(door);

                if (intersect == null)
                {
                    throw new DoesntIntersectException("Güncellemeye çalıştığınız kapı herhangi bir mahalleye ait olmalıdır.");
                }
            }
        }

        private static void Handle(List<Neighbor> neighbors)
        {
            foreach (var neighbor in neighbors)
            {
                if (neighborManager.GetIntersects(neighbor) != null)
                {
                    throw new IntersectException("Güncellemeye çalıştığınız mahalle, herhangi bir mahalle ile kesişmemelidir.");
                }

                var alreadyInside = doorManager.GetDoorsWithNeighborCode(neighbor.Id);
                var newInside = doorManager.GetDoorsInNeighBor(neighbor);

                var notInside = alreadyInside.Except(newInside, new DoorComparer());


                foreach (var door in notInside)
                {
                    var intersect = neighborManager.GetIntersects(door);

                    if (intersect == null)
                    {
                        throw new OrphanDoorException("Kapıları mahallesiz bırakarak güncelleme yapamazsınız.");
                    }
                }
            }
        }
    }
}
