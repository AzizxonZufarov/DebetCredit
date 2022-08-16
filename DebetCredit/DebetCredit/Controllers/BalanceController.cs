using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using DebetCredit.Models;
using System.Globalization;
using DebetCredit.Services;
using System.Web.Security;

namespace DebetCredit.Controllers
{
    //[Authorize]
    public class BalanceController : Controller
    {
        private readonly BalanceService balanceService = new BalanceService();

        string connectionString = @"Data Source = ACER\SQLEXPRESS; Initial Catalog = DebetCreditdb; Integrated Security=True";

        [HttpGet]
        public ActionResult Index()
        {
            var model = new BalanceTableModel
            {
                Rows = balanceService.GetAll().ToList()
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new BalanceModel());
        }

        [HttpPost]
        public ActionResult Create(BalanceModel balanceModel)
        {
            balanceService.Add(balanceModel);

            return RedirectToAction("Index");
        }

        // GET: Balance/Edit/5
        public ActionResult Edit(int ID)
        {
            var model = balanceService.GetById(ID);

            if (model is null)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }
        
        // POST: /Product/Edit/5
        [HttpPost]
        public ActionResult Edit(BalanceModel model)
        {
            balanceService.Update(model.ID, model);

            return RedirectToAction("Index");
        }

        // GET: Balance/Delete/5
        public ActionResult Delete(int id)
        {
            balanceService.Delete(id);

            return RedirectToAction("Index");
        }

        public ActionResult Inaction()
        {
            var model = new BalanceTableModel()
            {
                Rows = balanceService.GetAll(category: "Доход").ToList()
            };

            return View(model);
        }

        public ActionResult Outaction()
        {
            var model = new BalanceTableModel()
            {
                Rows = balanceService.GetAll(category: "Расход").ToList()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult DateRange(string start, string end)
        {
            var table = new DataTable();
            var sql = "SELECT * FROM Table_2 WHERE Date BETWEEN @From AND @To;";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var adapter = new SqlDataAdapter(sql, connection))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@From", Convert.ToDateTime(start, new CultureInfo("en-GB")));
                    adapter.SelectCommand.Parameters.AddWithValue("@To", Convert.ToDateTime(end, new CultureInfo("en-GB")));
                    adapter.Fill(table);
                }

                connection.Close();
            }

            var model = new BalanceTableModel();

            foreach (var t in table.Rows)
            {
                var row = t as DataRow;
                var item = new BalanceModel
                {
                    ID = row.Field<int>("ID"),
                    Date = row.Field<DateTime>("Date"),
                    Inout = row.Field<string>("Inout"),
                    Category = row.Field<string>("Category"),
                    Summ = row.Field<int>("Summ"),
                    Comment = row.Field<string>("Comment")
                };

                model.Rows.Add(item);
            }

            return View(model);
        }
    }
}
