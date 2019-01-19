using LearnCode.Entities;
using System;
using System.Text;

namespace LearnCode.Entities
{
    public class User:EntitBase
    {

        public int id { get; set; }
        public int Roleid { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        
    }
}
