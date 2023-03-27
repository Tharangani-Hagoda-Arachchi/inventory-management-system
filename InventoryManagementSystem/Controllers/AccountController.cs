using InventoryManagementSystem.Context;
using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace InventoryManagementSystem.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly MVCContext _mvcContext;
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;

        public AccountController(MVCContext context)
        {
            _mvcContext = context;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


       public void connectonString()
        {
            con.ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=InvontaryMnagement;Trusted_Connection=True;MultipleActiveResultSets=true";

        }


        [HttpPost]
        public IActionResult Verify(Account account)
        {
            connectonString();
            con.Open();
            com.Connection = con;
            com.CommandText = "select UserId from Accounts where Name='" + account.UserName + "'  AND Password ='" + account.Password + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                int  uid= dr.GetInt32(0);
                TempData["id"] = uid;
               
                
                return RedirectToAction("Index","Inventory"); 
            }
          
            else
            {
                con.Close();
                return RedirectToAction("Login", "Account");
            }
           
        }


        [HttpGet]
        public IActionResult Signin()
        { 
            Account account= new Account();
            return View(account);
        }


        [HttpPost]
        public IActionResult Signin(Account account)
        {
            _mvcContext.Attach(account);
            _mvcContext.Entry(account).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            _mvcContext.SaveChanges();
            return RedirectToAction("Login");

        }

    }

    
}
