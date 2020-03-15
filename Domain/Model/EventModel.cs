using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class EventModel

    {
        public string Home { get; set; }
        public string Away { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<string> Urls { get; set; }


    }
}
