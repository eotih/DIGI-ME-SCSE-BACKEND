using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCSE_BACKEND.Models
{
    public class Account1
    {
        public int IDUser { get; set; }
        public int IDRole { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int IDState { get; set; }
        public Nullable<System.DateTime> CreatedByDate { get; set; }
        public string Phone { get; set; }
        public string FullName { get; set; }
        public string Image { get; set; }
        public string Sex { get; set; }
    }
}