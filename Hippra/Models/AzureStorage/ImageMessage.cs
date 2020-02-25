using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Hippra.Models.AzureStorage
{
    public class ImageMessage
    {
        [Display(Name = "Image File")]
        public IFormFile ImageFile { get; set; }
    }
}
