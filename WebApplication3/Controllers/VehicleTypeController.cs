using Domain.Entitites;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PSafe.AM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            return View();
        }
        public IActionResult CreateSaveChange(VehicleType vehicleType)
        {
            string messages = "";
            StatusQuery Notification; 
            if(ModelState.IsValid)
            {
                messages = "product " + vehicleType.Name + " created successfully";
                try
                {
                    _vehicletypeRepository.Insert(vehicleType);
                    var statusInsert = _vehicletypeRepository.SaveChanges();
                    Notification = new StatusQuery("warning", "", "Vui lòng kiểm tra lại");
                    ViewBag.Status = Notification.Status;
                    ViewBag.Value = Notification.Value;
                    ViewBag.Type = Notification.Type;
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
                return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var _vehicle = _vehicletypeRepository.GetById(id);
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
