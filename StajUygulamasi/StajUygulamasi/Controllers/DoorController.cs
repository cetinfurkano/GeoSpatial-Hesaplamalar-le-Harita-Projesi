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
    public class DoorController : Controller
    {
        DoorManager _doorManager = DoorManager.CreateInstance();

        [HttpPost]
        public JsonResult AddDoor(Door door)
        {
            try
            {
                //var door = JsonConvert.DeserializeObject<Door>(stringDoor);
                _doorManager.Add(door);
                return Json(new { msg = "Başarılı", drm = true, data = _doorManager.Get(door) });
            }
            catch (Exception ex)
            {
                return Json(new { msg = ex.Message.ToString(), drm = false });
            }

        }


        [HttpPost]
        public JsonResult DeleteDoor(string doorId)
        {
            var door = _doorManager.Get(new Door { Id = doorId });

            _doorManager.Delete(door);

            return Json(door);
        }


        //[HttpPost]
        //public JsonResult UpdateDoor(Door door)
        //{
        //   // var door = JsonConvert.DeserializeObject<Door>(stringDoor);
        //    try
        //    {
        //        _doorManager.Update(door);
              
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { msg = ex.Message.ToString(), drm = false, data = _doorManager.Get(door) });
        //    }
        //    return Json(new { msg = "Başarılı", drm = true });
        //}


        public JsonResult GetDoor(string id)
        {
            return Json(_doorManager.Get(new Door { Id = id }));
        }

        public JsonResult GetAll()
        {
            return Json(_doorManager.GetAll());
        }
    }
}