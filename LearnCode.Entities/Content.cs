using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LearnCode.Entities
{
    public class Content : EntitBase
    {
        public Content()
        {
            ViewLevels = new List<ViewLevel>();
        }

        [Key]
        public int id { get; set; }
        public int Subjectid { get; set; }
        public string ContentName { get; set; }
        public int ViewLevelCount { get; set; }

        public List<ViewLevel> ViewLevels { get; set; }
    }


}


 