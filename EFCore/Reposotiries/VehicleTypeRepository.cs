using Domain.Entitites;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.Reposotiries
{
    public class VehicleTypeRepository : Repository<VehicleType>, IVehicleTypeRepository
    {
        public VehicleTypeRepository(postgresContext context) : base(context)
        {
        }
    }
}
