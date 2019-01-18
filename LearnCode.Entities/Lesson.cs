using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LearnCode.Entities
{
    public class Lesson : EntitBase
    {
        public Lesson()
        {
            Subjects = new List<Subject>();
            CommandIndices = new List<CommandIndex>();
        }
        public int id { get; set; }
        [Required]
        public string LessonName { get; set; }


        public List<Subject> Subjects { get; set; }
        public List<CommandIndex> CommandIndices { get; set; }
    }


}


 