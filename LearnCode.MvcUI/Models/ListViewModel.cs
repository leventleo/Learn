using System.Collections.Generic;
using LearnCode.Entities;

namespace LearnCode.MvcUI.Models
{
    public class ListViewModel
    {
        public List<Lesson> Lessons { get; set; }
        public List<CommandIndexView> CommandIndices { get; set; }
        public List<CommandIndex> CommandIndicesModel { get; set; }
        public List<Subject> Subjects  { get; set; }
        public List<Content> Contents { get; set; }
        public List<ViewLevel>  viewLevels { get; set; }
        public List<SourceFile> SourceFiles { get; set; }
        public ViewLevel  ViewLevel { get; set; }
        public CommandIndex CommandIndex  { get; set; }
        public string SubjectName { get; set; }
        public string LessonName { get; set; }
        public string ContentName { get; set; }
        public ViewLevelwithImage ViewLevelwithImage { get; set; }
        public SourceFileStub SourceFileStub { get; set; }
        public int selectedSubject { get; set; }
        public int selectedLesson { get; set; }
        public Dictionary<string,int> KeyValues  { get; set; }
        
    }
}