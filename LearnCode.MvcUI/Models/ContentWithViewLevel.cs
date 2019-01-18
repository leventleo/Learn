using LearnCode.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnCode.MvcUI.Models
{
    public class ContentWithViewLevel
    {
        public int Contentid { get; set; }
        public List<ViewLevel> ViewLevels { get; set; }
    }
}
