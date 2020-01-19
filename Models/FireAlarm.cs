using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIFireAlarm_221119.Models
{
    //TODO 01: Model FireAlarm aanmaken
    //TODO 02: Install via NuGet: Microsoft.EntityFrameworkCore.SqlServer
    //TODO 03: Microsoft.EntityFrameworkCore.InMemory
    public class FireAlarm
    {
        public UInt64 ID { get; set; }
        public string Location { get; set; }
        public string Reason { get; set; }
        public bool Active { get; set; }

    }
}
