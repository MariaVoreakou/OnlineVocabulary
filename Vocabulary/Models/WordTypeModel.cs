using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vocabulary.Models
{
    public class WordTypeModel
    {
        public int WordTypeId { get; set; }
        public string WordTypeName { get; set; }

        #region Navigation Properties
        public WordModel Word { get; set; }
        #endregion
    }
}
