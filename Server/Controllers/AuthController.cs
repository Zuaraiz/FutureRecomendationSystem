using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Server.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AuthController : ApiController
    { [HttpPut]
        [Route("api/Signup")]
        public int SignUp([FromBody]CustomModels userData)
        {
            FRDBEntities db = new FRDBEntities();
            int id = db.GetUserId(userData.email).FirstOrDefault<int?>() ?? default(int); ;
            if (id.Equals(0))
            {
                return db.UserSignUp(userData.email, userData.password, userData.firstName, userData.lastName, userData.percentage, userData.annualBudget, userData.location, userData.qualification);

            }
            return 0;

        }
        [HttpPost]
        [Route("api/Signin")]
        public UserSignIn_Result SignIn([FromBody] User user)

        {
            FRDBEntities db = new FRDBEntities();

            return db.UserSignIn(user.email, user.password).First<UserSignIn_Result>();
        }
    }
}
