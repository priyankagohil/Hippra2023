using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hippra.Models.SQL
{
    public class Tag
    {
        public int ID { get; set; }
        // tag 
        //public int TagID { get; set; }
        public string TagName { get; set; }

        public List<Case> Cases { get; set; }

        //public List<CaseComment> Comments { get; set; }
    }
}
