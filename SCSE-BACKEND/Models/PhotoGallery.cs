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
    
    public partial class PhotoGallery
    {
        public int ID { get; set; }
        public int IDCat { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Image { get; set; }
        public Nullable<System.DateTime> CreatedByDate { get; set; }
        public Nullable<System.DateTime> UpdatedByDate { get; set; }
    }
}
