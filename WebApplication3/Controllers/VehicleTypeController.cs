using Domain.Entitites;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Common;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class VehicleTypeController : Controller
    {
        private readonly IVehicleTypeRepository _vehicletypeRepository;
        public VehicleTypeController(IVehicleTypeRepository vehicleTypeRepository)
        {
            _vehicletypeRepository = vehicleTypeRepository;
        }
        public IActionResult Index()
        {
            StatusQuery Notification;
            Notification = new StatusQuery("warning", "", "Vui lòng kiểm tra lại");
            ViewBag.Status = Notification.Status;
            ViewBag.Value = Notification.Value;
            ViewBag.Type = Notification.Type;
            var _listVehicle = _vehicletypeRepository.GetAll();
            List<VehicleTypeModel> ListVehicleTypeModel = new List<VehicleTypeModel>();

            foreach (var item in _listVehicle)
                if (item.Id != 0)
                {
                    var vehicleTypeModel = new VehicleTypeModel() {
                        Active = "Đã kích hoạt",
                        Descreption = item.Descreption,
                        Name = item.Name,
                        Id = item.Id
                    };
                    ListVehicleTypeModel.Add(vehicleTypeModel);
                }
                else
                {
                    var vehicleTypeModel = new VehicleTypeModel()
                    {
                        Active = "bbbbb",
                        Descreption = item.Descreption,
                        Name = item.Name,
                        Id = item.Id
                    };
                    ListVehicleTypeModel.Add(vehicleTypeModel);
                }
                    

            return View(ListVehicleTypeModel);
        }
        public IActionResult Create()
        {
            
            StatusQuery Notification;
            Notification = TempDataHelper.Get<StatusQuery>(TempData, "Notification");
            if (Notification != null)
            {
                ViewBag.Status = Notification.Status;
                ViewBag.Value = Notification.Value;
                ViewBag.Type = Notification.Type;
            }
            return View();
        }
        public IActionResult CreateSaveChange(VehicleType vehicleType)
        {
          
            if(ModelState.IsValid)
            {                
                try
                {
                    _vehicletypeRepository.Insert(vehicleType);
                    var statusInsert = _vehicletypeRepository.SaveChanges();                 
                }
                catch(Exception)
                {
                    return View(vehicleType);
                }
            }
            else
            {
                return View(vehicleType);
            }
            return RedirectToAction("Index");                       
        }
        public IActionResult Delete(int id)
        {
            var _vehicle = _vehicletypeRepository.GetById(id);       
            return View(_vehicle);
        }
        [HttpPost]
        public IActionResult DeleteConfirm(int id)
        {
            
            var _vehicle = _vehicletypeRepository.GetById(id);    
                _vehicletypeRepository.Delete(_vehicle);
                var statusDelete = _vehicletypeRepository.SaveChanges(); 
                if(statusDelete > 0)
            {
                TempDataHelper.Put<StatusQuery>(TempData, "Notification", new StatusQuery("success", "", "Xoa thanh cong"));
                return RedirectToAction("Create", "VehicleType");
            }
            
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            StatusQuery Notification;
            var _vehicle = _vehicletypeRepository.GetById(id);
            if (_vehicle.Id == 2)
            {
                Notification = new StatusQuery("warning", "", "Chuc vu ton tai");
                ViewBag.Status = Notification.Status;
                ViewBag.Value = Notification.Value;
                ViewBag.Type = Notification.Type;

            }
            return View(_vehicle);
        }
        public IActionResult EditConfirm(VehicleType model)
        {
            var _vehicle = _vehicletypeRepository.GetById(model.Id);
            _vehicle.Id = model.Id;
            _vehicle.Name = model.Name;
            _vehicle.Descreption = model.Descreption;
            var statusEdit = _vehicletypeRepository.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
