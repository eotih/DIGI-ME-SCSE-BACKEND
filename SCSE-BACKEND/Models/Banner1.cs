using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCSE_BACKEND.Models
{
    public class Banner1
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string CreatedByUser { get; set; }
        public Nullable<System.DateTime> CreatedByDate { get; set; }
        public string UpdateByUser { get; set; }
        public Nullable<System.DateTime> UpdatedByDate { get; set; }
    }
}