using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Hippra.Models.FTDesign
{
    public class LayerDefinition
    {
        public int ID { get; set; }
        public RenderFragment Content { get; set; }
        public int z { get; set; }
        public bool Active { get; set; }
    }
}
