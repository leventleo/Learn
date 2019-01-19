using LearnCode.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LearnCode.Entities
{
    public class User:EntitBase
    {

        public int id { get; set; }
        public int Roleid { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        
    }
}
