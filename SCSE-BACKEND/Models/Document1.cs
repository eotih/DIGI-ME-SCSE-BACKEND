using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCSE_BACKEND.Models
{
    public class Document1
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Details { get; set; }
        public Nullable<System.DateTime> CreatedByDate { get; set; }
        public Nullable<System.DateTime> UpdatedByDate { get; set; }
    }
}