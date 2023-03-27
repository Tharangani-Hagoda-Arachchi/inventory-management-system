using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Models
{
    public class Inventory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvontaryId { get; set; }

        [Required(ErrorMessage = "Please Enter Name")]
        [StringLength(300)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Plese Enter Units")]
        public double AvailableUnit { get; set; }

        [Required(ErrorMessage = "Please Enter Re-Order level ")]
        public double ReorderLevel { get; set; }

        [Required(ErrorMessage = "Please Enter Unit Price")]
        public double UnitPrice { get; set; }

        internal static IQueryable<Inventory> AsNoTracking()
        {
            throw new NotImplementedException();
        }
    }
}
