using LearnCode.Entities;
using System.Collections.Generic;

namespace LearnCode.Entities
{
    public class Role:EntitBase
    {
        
        public int id { get; set; }
        public int Userid { get; set; }
        public string  RoleName { get; set; }
        
    }
}
