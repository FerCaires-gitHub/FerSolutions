using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class ParserModel
    {
        public string Site { get; set; }

        public IDictionary<string,string> HtmlCodes { get; set; }

    }
}
