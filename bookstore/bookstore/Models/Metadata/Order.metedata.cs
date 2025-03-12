using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bookstore.Models.Metadata
{
    public class Order
    {
        internal sealed class Ordermetadata
        {
            [DisplayFormat(DataFormatString = "{0:D5}")]
            public int IdOrder { get; set; }
            public string status { get; set; }
            public Nullable<int> MaKH { get; set; }
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public Nullable<System.DateTime> oderDate { get; set; }
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public Nullable<System.DateTime> deliveryDate { get; set; }
        }
    }
}