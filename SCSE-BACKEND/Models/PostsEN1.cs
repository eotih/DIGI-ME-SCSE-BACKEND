﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCSE_BACKEND.Models
{
    public class PostsEN1
    {
        public int IDPostEN { get; set; }
        public int IDCat { get; set; }
        public string Title { get; set; }
        public string SlugEN { get; set; }
        public string Details { get; set; }
        public string Image { get; set; }
        public System.DateTime CreatedByDate { get; set; }
        public Nullable<System.DateTime> UpdatedByDate { get; set; }
        public string Author { get; set; }
        public int IDState { get; set; }
    }
}