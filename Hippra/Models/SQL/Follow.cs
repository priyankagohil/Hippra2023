using System;
namespace Hippra.Models.SQL
{
    public class Follow
    {
        public int ID { get; set; }
        public int FollowerUserID { get; set; }
        public int FollowingUserID { get; set; }
    }
}
