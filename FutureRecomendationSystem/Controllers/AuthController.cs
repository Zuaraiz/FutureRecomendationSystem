using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.ComponentModel;
using FutureRecomendationSystem.Models;

namespace FutureRecomendationSystem.Controllers
{
    public class AuthController : ApiController
    {
        [HttpPut]
        [Route("api/UserSignup")]
        public int signUp([FromBody]CustomModels userData)
        {
            FRDBEntities db = new FRDBEntities();
             return db.UserSignUp(userData.email,userData.password,userData.firstName,userData.lastName,userData.percentage,userData.annualBudget,"islamabad","fsc");

        }
        [HttpPost]
        public void signIn()
        {

        }
    }
}
