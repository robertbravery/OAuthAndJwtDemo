using System.ComponentModel.DataAnnotations;

namespace WebOAuthDemo.Models
{
    public class UserLogins
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string UserName
        {
            get;
            set;
        }
        [Required]
        public string Password
        {
            get;
            set;
        }
        public UserLogins() { }
    }
}
