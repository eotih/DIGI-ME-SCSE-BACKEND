using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCSE_BACKEND.Models
{
    public class Contact1
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Details { get; set; }
        public Nullable<System.DateTime> CreatedByDate { get; set; }
        public Nullable<System.DateTime> UpdatedByDate { get; set; }
        public string Subtitle { get; set; }
    }
}