using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace LearnCode.MvcUI.Models
{
    public class ViewLevelwithImage
    {

        public int id { get; set; }
        public int? Contentid { get; set; }
        public int Levelid { get; set; }
        public string LevelContent { get; set; }
        public string LevelidName { get; set; }
        public IFormFile LevelImage { get; set; }

    }


    public class GoogleImageModel
    {

        public int id { get; set; }
        public string FileName { get; set; }
        public IFormFile Photo { get; set; }

    }

    public class SourceFileStub
    {
        public int id { get; set; }
        public string SourceType { get; set; }
        public IFormFile File { get; set; }
        public string FileName { get; set; }
    }


     
     
     
}


