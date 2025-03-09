using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace bookstore.Models
{
    public partial class Book
    {
        internal sealed class Bookmetadata
        {
            public int IdBook { get; set; }
            [Required(ErrorMessage = "Không được để trống giá trị này")]
            [StringLength(1000)]
            public string nameBook { get; set; }
            [Required(ErrorMessage = "Không được để trống giá trị này")]
            [StringLength(1000)]
            public string description { get; set; }
            [Required(ErrorMessage = "Không được để trống giá trị này")]
            public Nullable<int> price { get; set; }
            [Required(ErrorMessage = "Không được để trống giá trị này")]
            public string image { get; set; }
            [Required(ErrorMessage = "Không được để trống giá trị này")]
            public Nullable<int> quality { get; set; }
            [Required(ErrorMessage = "Không được để trống giá trị này")]
            public Nullable<int> IdCate { get; set; }
            public Nullable<int> IdPub { get; set; }
        }

    }
}