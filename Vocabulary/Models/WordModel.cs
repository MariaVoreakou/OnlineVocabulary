using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vocabulary.Models
{
    public class WordModel
    {
        public int WordId { get; set; }
        public string Word { get; set; }
        public BookModel Book { get; set; }
        public WordTypeModel WordType { get; set; }
        public IList<DefinitionModel> Definitions { get; set; }

    }
}
