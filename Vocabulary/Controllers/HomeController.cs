using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Vocabulary.Models;

namespace Vocabulary.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private MySqlDatabase MySqlDatabase { get; set; }
        public HomeController(
            MySqlDatabase mySqlDatabase,
            ILogger<HomeController> logger)
        {
            this.MySqlDatabase = mySqlDatabase;
            _logger = logger;
        }

        //---------Views---------//
        public async Task<IActionResult> Index()
        {
            dynamic mymodel = new ExpandoObject();
            mymodel.BookType = await GetBookTypes();
            mymodel.BookLanguage = await GetLanguages();
            mymodel.Book = await GetBooks();
            mymodel.WordType = await GetWordTypes();
            return View(mymodel);
        }
        public async Task<IActionResult> Books()
        {
            return View(await this.GetBooks());
        }
        public async Task<IActionResult> BookUnits(BookModel input)
        {
            return View(await this.GetBookUnits(input.BookId));
        }

        //"Create" to be deleted OR change Name
        public ActionResult Create()
        {
            return View();
        }


        //---------GET methods---------//

        public async Task<List<BookModel>> GetBooks()
        {
            var books = new List<BookModel>();

            var cmd = this.MySqlDatabase.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT
                              b.BookId, b.BookName,
                              bt.BookTypeId, bt.BookTypeName,
                              bl.BookLanguageId, bl.BookLanguageName
                              FROM Book b
                                JOIN BookType bt ON bt.BookTypeId = b.BookTypeId
                                JOIN BookLanguage bl ON bl.BookLanguageId = b.BookLanguageId";

            using (var reader = await cmd.ExecuteReaderAsync())
                while (await reader.ReadAsync())
                {
                    var b = new BookModel()
                    {
                        BookId = reader.GetInt32(0),
                        BookName = reader.GetString(1),
                        BookType = new BookType
                        {
                            BookTypeId = reader.GetInt32(2),
                            BookTypeName = reader.GetString(3)
                        },
                        BookLanguage = new BookLanguage
                        {
                            BookLanguageId = reader.GetInt32(4),
                            BookLanguageName = reader.GetString(5)
                        }
                    };
                    books.Add(b);
                }
            return books;
        }

        public async Task<List<SubUnitModel>> GetBookUnits(int bookId)
        {
            var units = new List<SubUnitModel>();
            var cmdUnits = this.MySqlDatabase.Connection.CreateCommand() as MySqlCommand;
            cmdUnits.CommandText = @"SELECT
                                      u.UnitName, u.SubUnitName, u.BookId, b.BookName
                                      FROM Unit u
                                      JOIN Book b ON b.BookId = @BookId";
            cmdUnits.Parameters.AddWithValue("@BookId", bookId);

            using (var reader = await cmdUnits.ExecuteReaderAsync())
                while (await reader.ReadAsync())
                {
                    var u = new SubUnitModel()
                    {
                        BookId = bookId,
                        UnitName = reader.GetString(0),
                        SubUnitName = reader.GetString(1),
                        Book = new BookModel
                        {
                            BookName = reader.GetString(3),
                        }
                    };
                    units.Add(u);
                }
            return units;
        }

        private async Task<List<BookLanguage>> GetLanguages()
        {
            var ret = new List<BookLanguage>();

            var cmd = this.MySqlDatabase.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT bl.BookLanguageId, bl.BookLanguageName FROM BookLanguage bl";

            using (var reader = await cmd.ExecuteReaderAsync())
                while (await reader.ReadAsync())
                {
                    var t = new BookLanguage()
                    {
                        BookLanguageId = reader.GetInt32(0),
                        BookLanguageName = reader.GetString(1)
                    };
                    ret.Add(t);
                }
            return ret;
        }

        private async Task<List<BookType>> GetBookTypes()
        {
            var ret = new List<BookType>();

            var cmd = this.MySqlDatabase.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT bt.BookTypeId, bt.BookTypeName FROM BookType bt";

            using (var reader = await cmd.ExecuteReaderAsync())
                while (await reader.ReadAsync())
                {
                    var t = new BookType()
                    {
                        BookTypeId = reader.GetInt32(0),
                        BookTypeName = reader.GetString(1)
                    };
                    ret.Add(t);
                }
            return ret;
        }
        private async Task<List<WordTypeModel>> GetWordTypes()
        {
            var wt = new List<WordTypeModel>();

            var cmd = this.MySqlDatabase.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT wt.WordTypeId, wt.WordTypeName FROM WordType wt";

            using (var reader = await cmd.ExecuteReaderAsync())
                while (await reader.ReadAsync())
                {
                    var t = new WordTypeModel()
                    {
                        WordTypeId = reader.GetInt32(0),
                        WordTypeName = reader.GetString(1)
                    };
                    wt.Add(t);
                }
            return wt;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
