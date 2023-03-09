using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSideShedule
{
    public class TimeSchedule
    {
        private string _startTime;
        public string StartTime {
            set { _startTime = value; }
            get { return _startTime; }
        }
        private string _endTime;
        public string EndTime
        {
            set { _endTime = value; }
            get { return _endTime; }
        }
    }
}
