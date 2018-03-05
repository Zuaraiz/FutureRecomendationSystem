using APIs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIs.Controllers
{
    public class UserController : ApiController
    {
        [HttpPost]
        [Route("api/add/skills")]
        public List<UserAddSkills_Result> AddSkills([FromBody] UserSkills skill)

        {
            FRDBEntities db = new FRDBEntities();
            return db.UserAddSkills(skill.email,skill.id, skill.rating).ToList<UserAddSkills_Result>();
            
        }
        [HttpPost]
        [Route("api/add/Interests")]
        public List<UserAddInterests_Result> AddInterests([FromBody] UserInterests interest)

        {
            FRDBEntities db = new FRDBEntities();
            return db.UserAddInterests(interest.email, interest.id, interest.rating).ToList<UserAddInterests_Result>();

        }
        [HttpPost]
        [Route("api/add/hobbies")]
        public List<UserAddHobbies_Result> AddHobbies([FromBody] UserHobbies hobby)

        {
            FRDBEntities db = new FRDBEntities();
            GetHobbyById_Result userHobby = db.GetHobbyById(hobby.id).FirstOrDefault<GetHobbyById_Result>();
            return db.UserAddHobbies(hobby.email,userHobby.name ).ToList<UserAddHobbies_Result>();
        }
        [HttpPost]
        [Route("api/user/skills")]
        public List<GetUserSkills_Result> Skills([FromBody] UserSkills skill)

        {
            FRDBEntities db = new FRDBEntities();
            return db.GetUserSkills(skill.email).ToList<GetUserSkills_Result>();

        }
        [HttpPost]
        [Route("api/user/interests")]
        public List<GetUserInterests_Result> Interests([FromBody] UserInterests interest)

        {
            FRDBEntities db = new FRDBEntities();
            return db.GetUserInterests(interest.email).ToList<GetUserInterests_Result>();

        }
        [HttpPost]
        [Route("api/user/hobbies")]
        public List<String>Hobbies([FromBody] UserHobbies hobby)

        {
            FRDBEntities db = new FRDBEntities();
            return db.GetUserHobbies(hobby.email).ToList<String>();
        }
    }
}
