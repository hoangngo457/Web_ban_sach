using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bookstore.Models.Metadata
{
    public class OrderDetail
    {
        internal sealed class OrderDetailmetadata
        {
            public int IdBook { get; set; }
            [DisplayFormat(DataFormatString = "{0:D5}")]
            public int IdOrder { get; set; }
            public Nullable<int> quanlity { get; set; }
            public Nullable<int> price { get; set; }

        }
    }
}