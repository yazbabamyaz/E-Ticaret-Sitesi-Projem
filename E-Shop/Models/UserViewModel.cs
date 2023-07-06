using EntityLayer.Entities;
using System.ComponentModel.DataAnnotations;

namespace E_Shop.Models
{
    public class UserViewModel
    {
        //public User User { get; internal set; }

        public int Id { get; set; }


        
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public string? Role { get; set; }
		public List<User> ListUser { get; internal set; }
	}
}
