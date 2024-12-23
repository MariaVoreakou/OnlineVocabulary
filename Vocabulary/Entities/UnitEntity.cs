using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vocabulary.Entities
{
    public class UnitEntity
    {
        public int UnitId { get; set; }
        public string UnitName { get; set; }


        #region Navigation Properties
        public BookEntity Book { get; set; }
        #endregion
    }
}
