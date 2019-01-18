using System;

namespace LearnCode.Entities
{
    public class EntitBase : IEntity
    {
        public DateTime CreatTime { get; set; } = DateTime.Now;
        public DateTime UpdateTime { get; set; } = DateTime.Now;
        public DateTime? Tombstone { get; set; } 
        public bool IsActive { get; set; } = true;
    }
}


 