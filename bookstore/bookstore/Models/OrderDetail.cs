//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace bookstore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class OrderDetail
    {
        public int IdBook { get; set; }
        [DisplayFormat(DataFormatString = "{0:D5}")]
        public int IdOrder { get; set; }
        public Nullable<int> quanlity { get; set; }
        public Nullable<int> price { get; set; }
    
        public virtual Book Book { get; set; }
        public virtual Order Order { get; set; }
    }
}
