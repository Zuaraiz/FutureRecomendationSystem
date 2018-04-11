using System;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using Server.Models;
using Server.Controllers;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Net.NetworkInformation;
using System.Text;
using Quartz;
using Server.DataProvider;

namespace Server.Implementation
{
    public class RecommendationUpdateJob : IJob
    {
        private FRDBEntities db = new FRDBEntities();


        public class DataSet
        {
            public int major { get; set; }
            public string skill { get; set; }
            public int skillRate { get; set; }
            public string interest { get; set; }
            public int interestRate { get; set; }
        }
        public class TestDataSet
        {
            public int uid { get; set; }
            public string major { get; set; }
            public double probabilty { get; set; }

        }
        public void Execute(IJobExecutionContext context)
        {
            List<TestDataSet> Recommendations= new List<TestDataSet>();
            Recommendations = Main();

            using (FRDBEntities db = new FRDBEntities())
            {

                db.DeleteRecommenadations();
            }
            foreach (var recommend in Recommendations)
            {
                using (FRDBEntities db = new FRDBEntities())
                {

                    db.AddRecommenadation(recommend.uid, Convert.ToInt32(recommend.major), (int)(recommend.probabilty  * 100));
                }
            }

        }

     

     
            public List<TestDataSet> Main()
            {
                IDataProvider dataProvider = new MockDataProvider();
                List<GetTestData_Result> tempData = new List<GetTestData_Result>();
                List<TestDataSet> testData = new List<TestDataSet>();
                List<IDictionary<string, double>> dictionaries = new List<IDictionary<string, double>>();
                using (FRDBEntities db = new FRDBEntities())
                {
                    tempData = db.GetTestData().ToList<GetTestData_Result>();

                }

                Classifier<string> classifier = new Classifier<string>();
                var sampleData = dataProvider.GetTrainingData() as List<InformationModel<string>>;
                classifier.Teach(sampleData);
                int temp = tempData.FirstOrDefault<GetTestData_Result>().UserID;
                List<GetTestData_Result> list = new List<GetTestData_Result>();
            int lenth = tempData.Count;
            int count = 1;
            foreach (var item in tempData)
            {
                if (temp == item.UserID)
                {
                    IDictionary<string, double> dict = classifier.Classify(new List<string>() { item.skill, BinData(item.skill, item.skillrate), item.interest, BinData(item.interest, item.interestrate) });
                    dictionaries.Add(dict);
                    if (count == lenth)
                    {
                        foreach (var d in add(dictionaries))
                        {
                            testData.Add(new TestDataSet { uid = temp, major = d.Key, probabilty = d.Value });
                        }
                    }

                }
                else
                {
                    foreach (var d in add(dictionaries))
                    {
                        testData.Add(new TestDataSet { uid = temp, major = d.Key, probabilty = d.Value });
                    }

                    temp = item.UserID;
                    dictionaries.Clear();
                    IDictionary<string, double> dict = classifier.Classify(new List<string>() { item.skill, BinData(item.skill, item.skillrate), item.interest, BinData(item.interest, item.interestrate) });
                    dictionaries.Add(dict);

                }
                count++;
            }

            return testData;
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
            public IDictionary<string, double> add(List<IDictionary<string, double>> list)
            {
                var result = list
           .SelectMany(d => d)
           .GroupBy(
             kvp => kvp.Key,
             (key, kvps) => new { Key = key, Value = kvps.Sum(kvp => kvp.Value) }
           )
           .ToDictionary(x => x.Key, x => x.Value);
                return result;
            }


        
    }
    }