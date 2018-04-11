using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Server.Implementation;
using Server.Models;

namespace Server.DataProvider
{
    public class MockDataProvider : IDataProvider
    {
        List<InformationModel<string>> data = new List<InformationModel<string>>();
        List<GetTrainingData_Result> TrainingData = new List<GetTrainingData_Result>();
     
        public object GetTrainingData()
        {using (FRDBEntities db = new FRDBEntities())
            {

               TrainingData= db.GetTrainingData().ToList<GetTrainingData_Result>();
            }

        foreach (var item in TrainingData)
            {
                InformationModel<string> temp = new InformationModel<string>();
                temp.Lable = item.MajorLabel.ToString();
                temp.Features = new List<string>() { item.skill, BinData(item.skill, item.skillrate), item.interest, BinData(item.interest, item.interestrate) };
                data.Add(temp);
              

            }
                





            return data;
        }



        public object GetTrainingData(List<DBSet> dbData)
        {
            var data = new List<InformationModel<string>>();
            foreach (var item in dbData)
            {
                data.Add(new InformationModel<string>()
                {
                    Lable = item.Major,
                    Features = new List<string>() { item.Skill, item.SkillRate, item.Interest, item.InterestRate }
                });
            }
            
            return data;
        }

        public void SaveTrainingData(object modelInfos)
        {
            if(modelInfos==null)
                throw new ArgumentNullException();

            throw new NotImplementedException();
        }
        static string BinData(string name, int value)
        {
            if (value <= 20)
                return "very low " + name;
            else if (value > 20 && value <= 40)
                return "low " + name;
            else if (value > 40 && value <= 60)
                return "average " + name;
            else if (value > 60 && value <= 80)
                return "high " + name;
            else
                return "very high " + name;
        }
    }
}
