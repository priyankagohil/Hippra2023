using Hippra.Models.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hippra.Models.POCO
{
    public class SearchResultModel
    {
        public List<Case> Cases { get; set; }
        public int TotalCount { get; set; }
    }
}
