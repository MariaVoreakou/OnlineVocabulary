﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vocabulary.Models
{
    public class SubUnitModel
    {
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public string SubUnitName { get; set; }
        public int BookId { get; set; }

        #region Navigation Properties
        public BookModel Book { get; set; }
        #endregion
    }
}
