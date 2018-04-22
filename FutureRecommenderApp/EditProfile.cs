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
    [Activity(Label = "EditProfile")]
    public class EditProfile : Activity
    {
        int citySelected = 1, qualSelected = 1;
        private List<KeyValuePair<string, int>> city;
        private List<KeyValuePair<string, int>> qual;
        String email;
        String Hobbies;
        String Interests;
        String Skills;
        GetUserInfo_Result Userinfo;
        EditText firstname, lastname, Budget;
          TextView  hobby, interst, skill;
        decimal percentaage;
     

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EditProfile);
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(ApplicationContext);
            email = prefs.GetString("email", "empty");
            firstname = FindViewById<EditText>(Resource.Id._fname);
            lastname = FindViewById<EditText>(Resource.Id._lname);
            Budget = FindViewById<EditText>(Resource.Id._budget);
        
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
                foreach (var a in h)
                {
                    Hobbies += a + ", ";
                }
                foreach (var b in i)
                {
                    Interests += b.name + ", ";
                }
                foreach (var c in s)
                {
                    Skills += c.name + ", ";
                }
                firstname.Text = x.firstName;
                lastname.Text = x.lastName;
                 percentaage= (int)(x.percentage);

                Budget.Text = ((int)x.annualBudget).ToString();
               
                hobby.Text = Hobbies;
                interst.Text = Interests;
                skill.Text = Skills;
                Task<string> task4 = getLocations();

                var xyz = JsonConvert.DeserializeObject<result[]>(task4.Result);
                Task<string> task5 = getQualification();
                var yz = JsonConvert.DeserializeObject<result[]>(task5.Result);
                city = new List<KeyValuePair<string, int>>();
                qual = new List<KeyValuePair<string, int>>();
                foreach (var a in xyz)
                {
                    city.Add(new KeyValuePair<string, int>(a.name, a.id));
                }
                foreach (var b in yz)
                {
                    qual.Add(new KeyValuePair<string, int>(b.name, b.id));
                }
                List<string> cityNames = new List<string>();
                foreach (var item in city)
                    cityNames.Add(item.Key);
                List<string> qualNames = new List<string>();
                foreach (var item in qual)
                    qualNames.Add(item.Key);

                Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner1);

                spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
                var adapter = new ArrayAdapter<string>(this,
                 Android.Resource.Layout.SimpleSpinnerItem, cityNames);

                adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                spinner.Adapter = adapter;

                Spinner spinner2 = FindViewById<Spinner>(Resource.Id.spinner2);

                spinner2.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected2);
                var adapter2 = new ArrayAdapter<string>(this,
                 Android.Resource.Layout.SimpleSpinnerItem, qualNames);

                adapter2.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                spinner2.Adapter = adapter2;
            }
            catch
            {
                Toast.MakeText(this, "Error! Some Wrong Happen", ToastLength.Short).Show();

            }

            Button button = FindViewById<Button>(Resource.Id.button2);

            button.Click += delegate {
                StartActivity(typeof(DeleteSkills));
                this.Finish();

            };
            Button button1 = FindViewById<Button>(Resource.Id.button3);

            button1.Click += delegate {
                StartActivity(typeof(AddInterest));
                this.Finish();

            };
            Button button2 = FindViewById<Button>(Resource.Id.button1);

            button2.Click += delegate {
                StartActivity(typeof(AddHobby));
                this.Finish();

            };
            Button save = FindViewById<Button>(Resource.Id.button4);

            save.Click += delegate {

                String fname = firstname.Text;
                String lname = lastname.Text;
                int budget = Int32.Parse(Budget.Text);
                Task<string> task6 = editUserInfo(email, fname, lname, percentaage, budget, citySelected, qualSelected);
                try
                {
                    var x = JsonConvert.DeserializeObject<GetUserInfo_Result>(task6.Result);
                    Toast.MakeText(this, "Success! Updated", ToastLength.Short).Show();
                }
                catch
                {
                    Toast.MakeText(this, "Error! Some Wrong Happens", ToastLength.Short).Show();
                }
            };
        }


        private async Task<string> editUserInfo(string email, string firstname, string lastname, decimal percentage, int annualbudget, int location, int qualification)
        {
            var client = new System.Net.Http.HttpClient();
            string r = "2";
            var jsonRequest = new { email = email, firstName = firstname, lastName = lastname, percentage = percentage, annualBudget = annualbudget, location = location, qualification = qualification };

            var serializedJsonRequest = JsonConvert.SerializeObject(jsonRequest);
            HttpContent content = new StringContent(serializedJsonRequest, Encoding.UTF8, "application/json");

            var result = await client.PostAsync("http://futurerecommend.azurewebsites.net/api/edit/profile", content).ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
            {
                r = await result.Content.ReadAsStringAsync();
            }
            return r;
        }


        private async Task<string> getUserInfo(string email)
        {
            var client = new System.Net.Http.HttpClient();
            string r = "2";
            var jsonRequest = new { email = email };

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



        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            citySelected = city[e.Position].Value;
        }
        private void spinner_ItemSelected2(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            qualSelected = qual[e.Position].Value;

        }







        private async Task<string> getLocations()
        {
            var client = new HttpClient();
            string r = "";

            var result = await client.GetAsync("http://futurerecommend.azurewebsites.net/api/get/locations").ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
            {
                r = await result.Content.ReadAsStringAsync();
            }
            return r;
        }
        private async Task<string> getQualification()
        {
            var client = new HttpClient();
            string r = "";
            var result = await client.GetAsync("http://futurerecommend.azurewebsites.net/api/get/qualification").ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
            {
                r = await result.Content.ReadAsStringAsync();
            }
            return r;
        }

    }
}