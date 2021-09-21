using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCSE_BACKEND.Models
{
    public class Document
    {
        public int DocId { get; set; }
        public string DocName { get; set; }
        public byte[] DocContent { get; set; }
    }
}