using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Models
{
    public class CustomModels:UserSignIn_Result
    {
        public String email { set; get; }
        public String password { set; get; }

    }
    public partial class UserSkills:UserAddSkills_Result
    {
        public String email { set; get; }
    }
    public partial class UserInterests : UserAddInterests_Result
    {
        public String email { set; get; }
    }
    public partial class UserHobbies: UserAddHobbies_Result
    {
        public String email { set; get; }
    }
}