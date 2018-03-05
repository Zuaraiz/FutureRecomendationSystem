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
    public class UserController : ApiController
    {
        [HttpPost]
        [Route("api/add/skills")]
        public List<UserAddSkills_Result> AddSkills([FromBody] UserSkills skill)

        {
            FRDBEntities db = new FRDBEntities();
            return db.UserAddSkills(skill.email, skill.id, skill.rating).ToList<UserAddSkills_Result>();

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
            return db.UserAddHobbies(hobby.email, userHobby.name).ToList<UserAddHobbies_Result>();
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
        public List<String> Hobbies([FromBody] UserHobbies hobby)

        {
            FRDBEntities db = new FRDBEntities();
            return db.GetUserHobbies(hobby.email).ToList<String>();
        }



        [HttpPost]
        [Route("api/delete/interest")]
        public int DeleteSkills([FromBody] UserInterest interest)

        {
            FRDBEntities db = new FRDBEntities();
            using (db)
            {
                return db.DeleteUserInterest(interest.user, interest.id);
            }


        }
        [HttpPost]
        [Route("api/delete/Hobby")]
        public int DeleteHobbies([FromBody] UserHobbies hobby)

        {
            FRDBEntities db = new FRDBEntities();
            using (db)
            {
                return db.DeleteUserHobbies(hobby.user, hobby.id);
            }


        }
        [HttpPost]
        [Route("api/delete/skills")]
        public int DeleteSkills([FromBody] UserSkill skill)

        {
            FRDBEntities db = new FRDBEntities();
            using (db)
            {
                return db.DeleteUserSkills(skill.user, skill.id);
            }


        }


        [HttpPost]
        [Route("api/edit/profile")]
        public GetUserInfo_Result EditProfile([FromBody]CustomModels userData)
        {
            FRDBEntities db = new FRDBEntities();
            FRDBEntities db1 = new FRDBEntities();
            using (db)
            {
                db.UpdateUserInfo(userData.email, userData.firstName, userData.lastName, userData.percentage, userData.annualBudget, userData.location, userData.qualification);
            }


           return db1.GetUserInfo(userData.email).FirstOrDefault<GetUserInfo_Result>();
        }
        [HttpGet]
        [Route("api/get/locations")]
        public List<GetAllLocation_Result> AllLocation([FromBody] UserSkill skill)

        {
            FRDBEntities db = new FRDBEntities();
            using (db)
            {
                return db.GetAllLocation().ToList<GetAllLocation_Result>();
            }
        }

        [HttpGet]
        [Route("api/get/qualification")]
        public List<GetAllPreDegree_Result> AllQual([FromBody] UserSkill skill)

        {
            FRDBEntities db = new FRDBEntities();
            using (db)
            {
                return db.GetAllPreDegree().ToList<GetAllPreDegree_Result>();
            }
        }



    }
}
