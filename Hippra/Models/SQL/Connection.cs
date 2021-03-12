using System;
namespace Hippra.Models.SQL
{
    public class Connection
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int FriendID { get; set; }
        public int Status { get; set; }
        public string FullName = "";
        public string CurrentPosition = "";
        public string Location = "";
        public string FProfileUrl = "";
    }
}
