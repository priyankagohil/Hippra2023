using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hippra.Models.SQL
{
    public class CaseTags
    {
        public DateTime ID { get; set; }
        public string Tag { get; set; }
 /*       public string commentGUID { get; set; }*/
        public int CaseID { get; set; }
        public Case Case { get; set; }
    }
}
