using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace bookstore.Models
{
     
    
    public partial class User
    {
        internal sealed class Usermetadata
        {
            public int IdUser { get; set; }
            [Required(ErrorMessage = "Tên tài khoản không được để trống.")]
            [StringLength(20, ErrorMessage = "Tên tài khoản phải từ 6 kí tự", MinimumLength = 6)]
            public string username { get; set; }
            [Required(ErrorMessage = "Mật khẩu không được để trống.")]
            [StringLength(20, ErrorMessage = "Mật khẩu phải từ 8 kí tự", MinimumLength = 8)]
            public string password { get; set; }
            
            [Required(ErrorMessage = "Bạn chưa nhập lại mật khẩu.")]
            [System.ComponentModel.DataAnnotations.Compare("password", ErrorMessage = "Mật khẩu không trùng khớp, vui lòng nhập lại.")]
            public string confirmPassword { get; set; }
            public string role { get; set; }
            [Required(ErrorMessage = "Họ tên không được để trống.")]
            public string name { get; set; }
            public string address { get; set; }
            public string email { get; set; }
            [Required(ErrorMessage = "Số điện thoại không được để trống.")]
            public string phone { get; set; }

        }
    }
}