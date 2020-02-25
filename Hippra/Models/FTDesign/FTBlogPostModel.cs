using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hippra.Models.FTDesign
{
    public class FTBlogPostModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime dateTime { get; set; }
        public int PosterPublicID { get; set; }
        public string tags { get; set; }
        public string Img { get; set; }
    }
}
