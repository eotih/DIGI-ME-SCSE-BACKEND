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
    
    public partial class OrganizationConfiguration
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int IDField { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Logo { get; set; }
        public string Fanpage { get; set; }
        public string Youtube { get; set; }
        public int IDBank { get; set; }
        public string UpdatedByUser { get; set; }
        public Nullable<System.DateTime> UpdatedByDate { get; set; }
    }
}
