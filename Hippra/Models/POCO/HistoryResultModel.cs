using Hippra.Models.SQL;
using System.Collections.Generic;
namespace Hippra.Models.POCO
    
{
    public class HistoryResultModel
    {
        public List<PostHistory> Histories { get; set; }
        public int TotalCount { get; set; }
    }
}

