using System.Collections.Generic;

namespace LearnCode.Entities
{
    public class Subject : EntitBase
    {
        public Subject()
        {
            Contents = new List<Content>();
        }

        public int id { get; set; }
        public int Lessonid { get; set; }
        public string SubjectName { get; set; }

        public List<Content> Contents { get; set; }
    }


}


 