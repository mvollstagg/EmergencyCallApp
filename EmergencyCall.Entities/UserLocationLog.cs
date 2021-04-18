using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergencyCall.Entities
{
    public class UserLocationLog
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public double Latitude { get; set; }
        public double Longtitude { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
