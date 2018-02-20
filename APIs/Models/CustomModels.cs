using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIs.Models
{
    public class CustomModels:UserSignIn_Result
    {
        public String email { set; get; }
        public String password { set; get; }

    }
}