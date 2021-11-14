using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCSE_BACKEND.Models
{
    public class DocumentEN1
    {
        public int IDEN { get; set; }
        public string Title { get; set; }
        public string SlugEN { get; set; }
        public string Details { get; set; }
        public Nullable<System.DateTime> CreatedByDate { get; set; }
        public Nullable<System.DateTime> UpdatedByDate { get; set; }
    }
}