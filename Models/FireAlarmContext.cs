using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIFireAlarm_221119.Models
{
    //TODO 04: Database context maken
    public class FireAlarmContext: DbContext
    {
        public FireAlarmContext(DbContextOptions<FireAlarmContext> options)
            :base(options)
        {

        }

        public DbSet<FireAlarm> FireAlarmItems { get; set; }

    }
}
