using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using MySql.Data.MySqlClient;
using Vocabulary;
using Vocabulary.Models;

namespace TextTasks.Controllers
{
    [Route("[action]")]
    public class TasksController : Controller
    {
        private MySqlDatabase MySqlDatabase { get; set; }
        public TasksController(MySqlDatabase mySqlDatabase)
        {
            this.MySqlDatabase = mySqlDatabase;
        }

        //For AJAX Calls
        public async Task<JsonResult> GetBooksJsonAsync()
        {
            var books = new List<BookModel>();
            var cmd = this.MySqlDatabase.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT b.BookName, b.BookId FROM Book b";
            using (var reader = await cmd.ExecuteReaderAsync())
                while (await reader.ReadAsync())
                {
                    var b = new BookModel
                    {
                        BookName = reader.GetString(0),
                        BookId = reader.GetInt32(1)
                    };
                    books.Add(b);
                }
            return Json(books);
        }
        public async Task<JsonResult> GetUnitsJsonAsync(int bookId)
        {
            var units = new List<UnitModel>();

            var cmd = this.MySqlDatabase.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT DISTINCT u.UnitName FROM Unit u
                                WHERE u.BookId = @BookId";
            cmd.Parameters.AddWithValue("@BookId", bookId);

            using (var reader = await cmd.ExecuteReaderAsync())
                while (await reader.ReadAsync())
                {
                    var u = new UnitModel()
                    {
                        UnitName = reader.GetString(0),
                        BookId = bookId
                    };
                    units.Add(u);
                }
            return Json(units);
        }
        public async Task<JsonResult> GetSubUnitsJsonAsync(string unitName, int bookId)
        {
            var subUnits = new List<SubUnitModel>();

            var cmd = this.MySqlDatabase.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT u.SubUnitName, u.UnitId FROM Unit u
                                WHERE u.UnitName = @UnitName AND u.BookId = @BookId";
            cmd.Parameters.AddWithValue("@UnitName", unitName);
            cmd.Parameters.AddWithValue("@BookId", bookId);

            using (var reader = await cmd.ExecuteReaderAsync())
                while (await reader.ReadAsync())
                {
                    var u = new SubUnitModel()
                    {
                        SubUnitName =  reader.GetString(0),
                        UnitId = reader.GetInt32(1),
                    };
                    subUnits.Add(u);
                }
            return Json(subUnits);
        }

        //THIS WORKS
        [HttpPost]
        public IActionResult InsertBook([FromForm] BookModel input)
        {
            var cmd = this.MySqlDatabase.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO Book (BookName, BookTypeId, BookLanguageId) VALUES (@BookName, @BookTypeId, @BookLanguageId)";
            cmd.Parameters.AddWithValue("@BookName", input.BookName);
            cmd.Parameters.AddWithValue("@BookTypeId", input.BookTypeId);
            cmd.Parameters.AddWithValue("@BookLanguageId", input.BookLanguageId);
            var recs = cmd.ExecuteNonQuery();

            //actionName: the Name of the action to that controller
            //controllerName: the Name of controller(without the suffix controller)
            return RedirectToAction("Books", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> InsertUnitsAsync([FromForm] UnitModel input)
        {
            for (int i=0; i< input.SubUnitName.Count; i++)
            {
                var cmd = this.MySqlDatabase.Connection.CreateCommand() as MySqlCommand;
                cmd.CommandText = @"INSERT INTO Unit (UnitName, SubUnitName, BookId) VALUES (@UnitName, @SubUnitName, @BookId)";
                cmd.Parameters.AddWithValue("@UnitName", input.UnitName);
                cmd.Parameters.AddWithValue("@SubUnitName", input.SubUnitName[i]);
                cmd.Parameters.AddWithValue("@BookId", input.BookId);
                var recs = cmd.ExecuteNonQuery();
            }
            //actionName: the Name of the action to that controller
            //controllerName: the Name of controller(without the suffix controller)
            return RedirectToAction("BookUnits", "Home", new {BookId = input.BookId });
        }



        [HttpPost]
        public void EditBook(BookModel input)
        {
            var cmd = this.MySqlDatabase.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE Book SET (BookName = @newBookName) WHERE BookId = @BookId;";
            cmd.Parameters.AddWithValue("@newBookName", input.BookName);
            cmd.Parameters.AddWithValue("@BookId", input.BookId);

            var recs = cmd.ExecuteNonQuery();
        }


    }
}