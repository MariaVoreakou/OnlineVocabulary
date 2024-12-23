using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vocabulary.Models
{
    public class DefinitionModel
    {
        public int DefinitionId { get; set; }
        public string Definition { get; set; }
        public IList<string> Examples { get; set; }
        public WordModel Word { get; set; }
    }
}
