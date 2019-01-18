using System;

namespace LearnCode.Entities
{
    public class ViewLevel : EntitBase
    {
        public int id { get; set; }
        public int? Contentid { get; set; }
        public int Levelid { get; set; }
        public string LevelContent { get; set; }
        public string LevelidName { get; set; }        
        public Byte[] LevelImage { get; set; }

    }
}


 