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

    public class DeleteModels
    { 
        public String email { set; get; }
        public String value { set; get; }

    }
    public class RecommendModel
    {
        public String UniversityName { set; get; }
      
        public String Degree { set; get; }
        public decimal fee { set; get; }
        public int rating { set;get;} 
        public string url { set; get; }
        public string ratingType { set; get; }

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