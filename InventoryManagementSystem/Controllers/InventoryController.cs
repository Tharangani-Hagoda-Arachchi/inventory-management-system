using InventoryManagementSystem.Context;
using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace InventoryManagementSystem.Controllers
{
    public class InventoryController : Controller
    {
       
        private readonly MVCContext _mvcContext;
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;

        public InventoryController(MVCContext context)
        {
            _mvcContext = context;
        }
        
        public IActionResult Index(int pg = 1)
        {
            List<Inventory> inventories = _mvcContext.Inventories.ToList();

            const int pageSize = 5;
            if(pg < 1)
                pg= 1;

            int restCount = inventories.Count;
            var pager = new Pager(restCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = inventories.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;

            return View(data);
        }

        public void connectonString()
        {
            con.ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=InvontaryMnagement;Trusted_Connection=True;MultipleActiveResultSets=true";

        }

       
        [HttpGet]
        public IActionResult Create()
        {
            Inventory inv = new Inventory();
            return View(inv);
        }


        [HttpPost]
        public IActionResult Create(Inventory invontary)
        {
            var id = TempData["id"];
            int uid = (int)id;

            _mvcContext.Attach(invontary);
            _mvcContext.Entry(invontary).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            _mvcContext.SaveChanges();

            connectonString();
            con.Open();
            com.Connection = con;
            com.CommandText = "select TOp 1 InvontaryId from Inventories ORDER BY InvontaryId DESC ";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                int iid = dr.GetInt32(0);
                TempData["i_id"] = iid;

            }
            con.Close();

            con.Open();
            int inv_id = (int)TempData["i_id"];

            com.CommandText = "insert into UserInventoryJunctions(InventoryId,UserId,Action) values('" + inv_id + "','" + uid +"','create')";
            dr = com.ExecuteReader();
            con.Close();
            return RedirectToAction("Index");

        }

        
        [HttpGet]
        public IActionResult Edit(int id)
        {
            TempData["eid"] = id;
   
            Inventory invontary = _mvcContext.Inventories.Where(p => p.InvontaryId == id).FirstOrDefault();
            return View(invontary);

        }
        
         
        [HttpPost]
        public IActionResult Edit(Inventory invontary)
        {
            var id = TempData["id"];
            int uid = (int)id;
            var eid = TempData["eid"];
            int editid = (int)eid;

            _mvcContext.Attach(invontary);
            _mvcContext.Entry(invontary).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _mvcContext.SaveChanges();

            connectonString();
            con.Open();
            com.Connection = con;
            com.CommandText = "insert into UserInventoryJunctions(InventoryId,UserId,Action) values('" + editid + "','" + uid + "','Edit')";
            dr = com.ExecuteReader();
          
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            TempData["did"] = id;

            Inventory invontary = _mvcContext.Inventories.Where(p => p.InvontaryId == id).FirstOrDefault();
            return View(invontary);

        }

        [HttpPost]
        public IActionResult Delete(Inventory invontary)
        {

            var id = TempData["id"];
            int uid = (int)id;
            var did = TempData["did"];
            int deleteid = (int)did;

            _mvcContext.Attach(invontary);
            _mvcContext.Entry(invontary).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            _mvcContext.SaveChanges();

            connectonString();
            con.Open();
            com.Connection = con;
            com.CommandText = "insert into UserInventoryJunctions(InventoryId,UserId,Action) values('" + deleteid + "','" + uid + "','Delete')";
            dr = com.ExecuteReader();
            return RedirectToAction("Index");

        }


        public IActionResult Detail(int id)
        {
            Inventory invontary = _mvcContext.Inventories.Where(p => p.InvontaryId == id).FirstOrDefault();
            return View(invontary);

        }


    }
}
