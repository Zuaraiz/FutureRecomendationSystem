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
            using (db)
            {
                return db.UserAddSkills(skill.email, skill.id, skill.rating).ToList<UserAddSkills_Result>();
                }
        }
        [HttpPost]
        [Route("api/add/Interests")]
        public List<UserAddInterests_Result> AddInterests([FromBody] UserInterests interest)

        {
            FRDBEntities db = new FRDBEntities();
            using (db)
            {
                return db.UserAddInterests(interest.email, interest.id, interest.rating).ToList<UserAddInterests_Result>();
            }
        }
        [HttpPost]
        [Route("api/add/hobbies")]
        public List<UserAddHobbies_Result> AddHobbies([FromBody] UserHobbies hobby)

        {
            FRDBEntities db = new FRDBEntities();
            FRDBEntities db1 = new FRDBEntities();
            GetHobbyById_Result userHobby;
            using (db)
            {
                 userHobby = db.GetHobbyById(hobby.id).FirstOrDefault<GetHobbyById_Result>();
            }
            using (db1)
            {
                return db1.UserAddHobbies(hobby.email, userHobby.name).ToList<UserAddHobbies_Result>();
            }
        }



        [HttpPost]
        [Route("api/user/skills")]
        public List<GetUserSkills_Result> Skills([FromBody] CustomModels skill)

        {
            FRDBEntities db = new FRDBEntities();
            return db.GetUserSkills(skill.email).ToList<GetUserSkills_Result>();

        }
        [HttpPost]
        [Route("api/user/interests")]
        public List<GetUserInterests_Result> Interests([FromBody] CustomModels interest)

        {
            FRDBEntities db = new FRDBEntities();
            return db.GetUserInterests(interest.email).ToList<GetUserInterests_Result>();

        }
        [HttpPost]
        [Route("api/user/hobbies")]
        public List<String> Hobbies([FromBody] CustomModels hobby)

        {
            FRDBEntities db = new FRDBEntities();
            using (db)
            {
                return db.GetUserHobbies(hobby.email).ToList<String>();
            }
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
            FRDBEntities db1 = new FRDBEntities();
            int id;
            using (db1)

            {
               
                    int? i =db1.GetUserId(hobby.email).FirstOrDefault();
                id = i ?? 0;
            }
                using (db)

            {

                return db.DeleteUserHobbies(id, hobby.id);
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


        [HttpPost]
        [Route("api/user/profile")]
        public GetUserInfo_Result UserProfile ([FromBody]CustomModels userData)
        {
            FRDBEntities db = new FRDBEntities();
            using (db)
            {
                return db.GetUserInfo(userData.email).FirstOrDefault<GetUserInfo_Result>();
            }

        }

        [HttpGet]
        [Route("api/get/locations")]
        public List<GetAllLocation_Result> AllLocation()

        {
            FRDBEntities db = new FRDBEntities();
            using (db)
            {
                return db.GetAllLocation().ToList<GetAllLocation_Result>();
            }
        }

        [HttpGet]
        [Route("api/get/qualification")]
        public List<GetAllPreDegree_Result> AllQual()

        {
            FRDBEntities db = new FRDBEntities();
            using (db)
            {
                return db.GetAllPreDegree().ToList<GetAllPreDegree_Result>();
            }
        }

        [HttpPost]
        [Route("api/get/skills")]
        public List<GetAllSkill_Result> skills([FromBody] UserSkills skill)

        {
            List<GetAllSkill_Result> temp = new List<GetAllSkill_Result>();
            List<GetAllSkill_Result> result = new List<GetAllSkill_Result>();
            List<GetUserSkills_Result> checkList = new List<GetUserSkills_Result>();
            FRDBEntities db = new FRDBEntities();
            using (db)
            
            {
                checkList = db.GetUserSkills(skill.email).ToList<GetUserSkills_Result>();
              
                temp= db.GetAllSkill().ToList<GetAllSkill_Result>();
                foreach(GetAllSkill_Result obj in temp)
                {
                    if (!(checkList.Any(x => x.name == obj.name)))
                        {
                        result.Add(obj);

                    }
                   
                }
                return result;
            }
        }

        [HttpPost]
        [Route("api/get/interests")]
        public List<GetAllInterest_Result> interest([FromBody] UserSkills skill)

        {
            List<GetAllInterest_Result> temp = new List<GetAllInterest_Result>();
            List<GetAllInterest_Result> result = new List<GetAllInterest_Result>();
            List<GetUserInterests_Result> checkList = new List<GetUserInterests_Result>();

            FRDBEntities db = new FRDBEntities();
            using (db)
            {
             checkList = db.GetUserInterests(skill.email).ToList<GetUserInterests_Result>();

                temp = db.GetAllInterest().ToList<GetAllInterest_Result>();
                foreach (GetAllInterest_Result obj in temp)
                {
                    if (!(checkList.Any(x => x.name == obj.name)))
                    {
                        result.Add(obj);

                    }

                }
             return result;
                
            }
        }

        [HttpPost]
        [Route("api/get/hobbies")]
        public List<GetAllHobby_Result> hobbies([FromBody] UserSkills skill)

        {
            List<GetAllHobby_Result> temp = new List<GetAllHobby_Result>();
            List<GetAllHobby_Result> result = new List<GetAllHobby_Result>();
            List<string> checkList = new List<string>();
            FRDBEntities db = new FRDBEntities();
            using (db)
            {
                checkList = db.GetUserHobbies(skill.email).ToList<string>();

                temp = db.GetAllHobby().ToList<GetAllHobby_Result>();
                foreach (GetAllHobby_Result obj in temp)
                {
                    if (!(checkList.Any(x => x == obj.name)))
                    
                        result.Add(obj);

                    }

                }
                return result;
                 }
        }


    }

