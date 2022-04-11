namespace Domain.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public User()
        {
            Orders = new List<Order>();
            Basket = new Basket();
        }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Password { get; set; }

        public List<Order> Orders { get; set; }

        [Required]
        public Basket Basket { get; set; }
    }
}
