using Hippra.Models.SQL;
using System.Collections.Generic;
namespace Hippra.Models.POCO
{
    public class NotificationResultModel
    {
        public List<Notification> Notifications { get; set; }
        public int TotalCount { get; set; }
    }
}
