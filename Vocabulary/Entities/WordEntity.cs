using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vocabulary.Entities
{
    public class WordEntity
    {

        public int WordId { get; set; }
        public string WordName { get; set; }
        public IList<DefinitionEntity> Definitions { get; set; }
        public WordTypeEntity WordType { get; set; }
        public BookEntity Book { get; set; }
        public UnitEntity Unit { get; set; }
        
        
    }
}
