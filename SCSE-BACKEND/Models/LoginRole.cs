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
    
    public partial class LoginRole
    {
        public int IDUser { get; set; }
        public string FullName { get; set; }
        public string RoleName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> CreatedByDate { get; set; }
        public Nullable<int> IDState { get; set; }
    }
}
