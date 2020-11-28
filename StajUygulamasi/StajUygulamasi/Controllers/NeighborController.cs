using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Concrete;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace StajUygulamasi.Controllers
{
    public class NeighborController : Controller
    {
       private NeighborManager _neighborManager = NeighborManager.CreateInstance();
       private DoorManager _doorManager = DoorManager.CreateInstance();
        
        [HttpPost]
        public JsonResult AddNeighbor(Neighbor neighbor)
        {
            try
            {
                //var neighbor = JsonConvert.DeserializeObject<Neighbor>(stringNeighbor);
                _neighborManager.Add(neighbor);
                return Json(new { msg = "Başarılı", data = _neighborManager.Get(neighbor), drm = true });
            }
            catch (Exception ex)
            {
                return Json(new { msg = ex.Message.ToString(), drm = false });
            }
        }

        [HttpPost]
        public JsonResult DeleteNeighbor(string neighborId)
        {
            var neighbor = _neighborManager.Get(new Neighbor { Id = neighborId });
            var doors = _doorManager.GetDoorsWithNeighborCode(neighbor.Id).Select(d=> d.Id);

            _neighborManager.Delete(neighbor);

            return Json(doors);
        }

        public JsonResult GetNeighbor(string id)
        {
            return Json(_neighborManager.Get(new Neighbor { Id = id}));
        }

        //[HttpPost]
        //public JsonResult UpdateNeighbor(Neighbor neighbor)
        //{
        //   // var neighbor = JsonConvert.DeserializeObject<Neighbor>(stringNeighbor);
        //    try
        //    {
        //        _neighborManager.Update(neighbor);
              
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { msg =  ex.Message.ToString(), drm = false, data = _neighborManager.Get(neighbor) });
        //    }

        //    return Json(new { msg = "Başarılı", drm = true});
        //}

        public JsonResult GetAll()
        {
            return Json(_neighborManager.GetAll());
        }


    }
}