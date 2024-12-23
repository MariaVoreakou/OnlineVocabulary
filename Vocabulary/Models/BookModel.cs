using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vocabulary.Models
{
    public class BookModel
    {
        public int BookId { get; set; }
        public string BookName { get; set; }

        public int BookTypeId { get; set; }
        public int BookLanguageId { get; set; }
        public IList<string> UnitName { get; set; }

        #region Navigation Properties
        public BookType BookType { get; set; }
        public BookLanguage BookLanguage { get; set; }
        public UnitModel Unit { get; set; }
        #endregion
    }
}
