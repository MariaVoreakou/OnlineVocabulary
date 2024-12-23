using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vocabulary.Entities
{
    public class DefinitionEntity
    {
        public int DefinitionId { get; set; }
        public string DefinitionText { get; set; }
        public IList<string> Examples { get; set; }

        #region Navigation Properties
        public WordEntity Word { get; set; }
        

        #endregion
    }
}
