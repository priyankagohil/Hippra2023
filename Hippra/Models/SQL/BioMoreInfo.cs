using System;
namespace Hippra.Models.SQL
{
    public class BioMoreInfo
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string Topic { get; set; }
        public string Position { get; set; }
        public string Timeline { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
    }
}
