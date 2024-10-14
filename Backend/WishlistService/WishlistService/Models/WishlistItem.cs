using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WishlistService.Models
{
    public class WishlistItem
    {
        [Key]
        public int favId { get; set; }
        public int foodid { get; set; }
        public string name { get; set; } = null!;
        public int caloric { get; set; }
        public string type { get; set; } = null!;
        public int fat { get; set; }
        public int carbon { get; set; }
        public int protein { get; set; }
        public int CategoryId { get; set; }
        public string image { get; set; } = null!;
        public int UserId { get; set; } 
    }

}
