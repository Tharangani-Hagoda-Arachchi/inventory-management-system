using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Models
{
    public class UserInventoryJunction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserInventoryJoinId { get; set; }

        [Required]
        public int InventoryId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string Action { get; set; }

    }
}
