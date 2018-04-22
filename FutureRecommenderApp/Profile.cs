using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace FutureRecommenderApp
{
    public partial class GetUserInfo_Result
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string location { get; set; }
        public string qualification { get; set; }
        public decimal percentage { get; set; }
        public decimal annualBudget { get; set; }
    }

    public partial class GetUserSkills_Result
    {
        public string name { get; set; }
        public int rating { get; set; }
    }
    [Activity(Label = "Profile", Theme = "@android:style/Theme.Holo.Light")]

    public class Profile : Activity
    {
        String email;
        String Hobbies;
        String Interests;
        String Skills;
        GetUserInfo_Result Userinfo;
        TextView emails, fullname, budget, city, qual, hobby, interst, skill;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Profile);
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(ApplicationContext);
            email = prefs.GetString("email", "empty");
            emails = FindViewById<TextView>(Resource.Id._email);
            fullname = FindViewById<TextView>(Resource.Id._name);
            budget = FindViewById<TextView>(Resource.Id._budget);
            city = FindViewById<TextView>(Resource.Id._city);
            qual = FindViewById<TextView>(Resource.Id._qualification);
            hobby = FindViewById<TextView>(Resource.Id._hobbylist);
            interst = FindViewById<TextView>(Resource.Id._interestlist);
            skill = FindViewById<TextView>(Resource.Id._skilllist);
            if (email.Equals("empty"))
            {
                StartActivity(typeof(MainActivity));
                this.Finish();
            }

            try
            {

                Userinfo = new GetUserInfo_Result();
                Task<string> task = getUserInfo(email);
                var x = JsonConvert.DeserializeObject<GetUserInfo_Result>(task.Result);
                
                Task<string> task1 = getUserHobby(email);
                var h = JsonConvert.DeserializeObject<string[]>(task1.Result);
                Task<string> task2 = getUserInterset(email);
                var i = JsonConvert.DeserializeObject<GetUserSkills_Result[]>(task2.Result);
                Task<string> task3 = getUserSkill(email);
                var s = JsonConvert.DeserializeObject<GetUserSkills_Result[]>(task3.Result);
                foreach(var a in h)
                {
                    Hobbies += a + ", ";
                }
                foreach (var b in i)
                {
                    Interests+=b.name + ", ";
                }
                foreach (var c in s)
                {
                    Skills += c.name + ", ";
                }
                emails.Text=email;
                fullname.Text = x.firstName + " " + x.lastName;
                city.Text = x.location;
                budget.Text = "Rs." + x.annualBudget;
                qual.Text = x.qualification;
                hobby.Text = Hobbies;
                interst.Text = Interests;
                skill.Text = Skills;


            }
            catch
            {
                Toast.MakeText(this, "Error! Some Wrong Happen", ToastLength.Short).Show();

            }

            Button button = FindViewById<Button>(Resource.Id.addSkill);

            button.Click += delegate {
                StartActivity(typeof(AddSkill));
                this.Finish();

            };
            Button button1 = FindViewById<Button>(Resource.Id.addInterest);

            button1.Click += delegate {
                StartActivity(typeof(AddInterest));
                this.Finish();

            };
            Button button2 = FindViewById<Button>(Resource.Id.addHobby);

            button2.Click += delegate {
                StartActivity(typeof(AddHobby));
                this.Finish();

            };
            Button edit = FindViewById<Button>(Resource.Id.goToEditProfile);

            edit.Click += delegate {
                StartActivity(typeof(EditProfile));
                this.Finish();

            };
        }




        private async Task<string> getUserInfo(string email)
        {
            var client = new HttpClient();
            string r = "2";
            var jsonRequest = new { email = email};

            var serializedJsonRequest = JsonConvert.SerializeObject(jsonRequest);
            HttpContent content = new StringContent(serializedJsonRequest, Encoding.UTF8, "application/json");

            var result = await client.PostAsync("http://futurerecommend.azurewebsites.net/api/user/profile", content).ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
            {
                r = await result.Content.ReadAsStringAsync();
            }
            return r;
        }
        private async Task<string> getUserHobby(string email)
        {
            var client = new HttpClient();
            string r = "2";
            var jsonRequest = new { email = email };

            var serializedJsonRequest = JsonConvert.SerializeObject(jsonRequest);
            HttpContent content = new StringContent(serializedJsonRequest, Encoding.UTF8, "application/json");

            var result = await client.PostAsync("http://futurerecommend.azurewebsites.net/api/user/hobbies", content).ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
            {
                r = await result.Content.ReadAsStringAsync();
            }
            return r;
        }
        private async Task<string> getUserInterset(string email)
        {
            var client = new HttpClient();
            string r = "2";
            var jsonRequest = new { email = email };

            var serializedJsonRequest = JsonConvert.SerializeObject(jsonRequest);
            HttpContent content = new StringContent(serializedJsonRequest, Encoding.UTF8, "application/json");

            var result = await client.PostAsync("http://futurerecommend.azurewebsites.net/api/user/interests", content).ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
            {
                r = await result.Content.ReadAsStringAsync();
            }
            return r;
        }
        private async Task<string> getUserSkill(string email)
        {
            var client = new HttpClient();
            string r = "2";
            var jsonRequest = new { email = email };

            var serializedJsonRequest = JsonConvert.SerializeObject(jsonRequest);
            HttpContent content = new StringContent(serializedJsonRequest, Encoding.UTF8, "application/json");

            var result = await client.PostAsync("http://futurerecommend.azurewebsites.net/api/user/skills", content).ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
            {
                r = await result.Content.ReadAsStringAsync();
            }
            return r;
        }
    }
}