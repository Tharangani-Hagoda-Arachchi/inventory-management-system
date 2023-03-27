using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Context
{
    public class MVCContext : DbContext
    {
        public MVCContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Inventory>Inventories { get; set; }
        public DbSet<Account>Accounts { get; set; } 
        public DbSet<UserInventoryJunction> userInventoryJunctions { get; set; }
    }
}
