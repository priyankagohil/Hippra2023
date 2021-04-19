using Hippra.Models.SQL;
using System.Collections.Generic;
namespace Hippra.Models.POCO
{
    public class ConnResultModel
    {
        public List<Connection> Connections { get; set; }
        public int TotalCount { get; set; }
    }
}

