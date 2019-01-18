namespace LearnCode.Entities
{
    public class SourceFile : EntitBase
    {
        public int id { get; set; }
        public string SourceType { get; set; }
        public string FileName { get; set; }
        public long Length { get; set; }

    }
}


 