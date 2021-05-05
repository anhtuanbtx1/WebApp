using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class StatusQuery
    {
        public StatusQuery()
        {

        }
        public StatusQuery(string type, string status, string value)
        {
            Status = status;
            Value = value;
            Type = type;
        }
        public string Status { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
    }
}
