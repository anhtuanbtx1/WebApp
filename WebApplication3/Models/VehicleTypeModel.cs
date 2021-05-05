using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class VehicleTypeModel
    {
        
        public int Id { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập tên")]
        [Display(Name = "Name",ResourceType =typeof(Resources.ResourceVehicleType))]
        public string Name { get; set; }
        [Display(Name = "Descreption", ResourceType = typeof(Resources.ResourceVehicleType))]
        public string Descreption { get; set; }
        [Display(Name="Active",ResourceType =typeof(Resources.ResourceVehicleType))]
        public string Active { get; set; }
        public IFormFile Image { get; set; }
    }
}
