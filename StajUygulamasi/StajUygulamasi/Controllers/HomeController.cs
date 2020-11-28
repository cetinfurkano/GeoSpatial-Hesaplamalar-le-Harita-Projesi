using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Business.Concrete;
using Business.Concrete.Handlers;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using StajUygulamasi.Models;

namespace StajUygulamasi.Controllers
{
    public class HomeController : Controller
    {
        DoorManager _doorManager = DoorManager.CreateInstance();
        NeighborManager _neighborManager = NeighborManager.CreateInstance();
        
        public IActionResult Index()
        {
            return View();
        }
        

        //public JsonResult BringData()
        //{
        //    HomeIndexViewModel model = new HomeIndexViewModel
        //    {
        //        neighbors = _neighborManager.GetAll(),
        //        doors = _doorManager.GetAll()
        //    };

        //    return Json(model);
        //}

        public JsonResult BringAddress()
        {
            var neighbors = _neighborManager.GetAll();
            var doors = _doorManager.GetAll();

            var address = doors.Join(neighbors,
                door => door.NeighborCode,
                neighbor => neighbor.Id,
                (door, neighbor) => new
                {
                    neighborName = neighbor.NeighborName,
                    doorCode = door.DoorCode,
                    geometry = door.Geometry
                });

            return Json(address);
        }

        [HttpPost]
        public JsonResult UpdateHandle(UpdateHandlerModel model)
        {
            List<Door> doors = new List<Door>();
            List<Neighbor> neighbors = new List<Neighbor>();

            foreach (var fakeDoor in model.Points)
            {
                var door = _doorManager.Get(new Door { Id = fakeDoor.Id });
                door.Geometry = fakeDoor.Geometry;
                doors.Add(door);
            }

            foreach (var fakeNeighbor in model.Polygons)
            {
                var neighbor = _neighborManager.Get(new Neighbor { Id = fakeNeighbor.Id });
                neighbor.Geometry = fakeNeighbor.Geometry;
                neighbors.Add(neighbor);
            }

            try
            {
                UpdateHandler.Control(doors, neighbors);
            }
            catch (Exception ex)
            {
                UpdateHandler.TakeOver();
                return Json(new { msg = ex.Message, drm = false});
            }

            return Json(new { msg = "Başarılı", drm = true });
        }
    }
}
