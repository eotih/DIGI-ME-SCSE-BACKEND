//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SCSE_BACKEND.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Account
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