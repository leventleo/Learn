using System.Collections.Generic;
using LearnCode.Entities;

namespace LearnCode.MvcUI.Models
{

    public class AddViewModel
    { 
        public List<Lesson> Lessons { get; set; }
        public Lesson Lesson { get; set; }
        public Subject  Subject { get; set; }
        public ViewLevel  ViewLevel { get; set; }

    }
}


