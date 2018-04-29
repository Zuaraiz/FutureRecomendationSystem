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
using static FutureRecommenderApp.AddSkill;

namespace FutureRecommenderApp
{
    [Activity(Label = "Add Hobby")]
    public class AddHobby : Activity
    {
        String email;
        List<GetAllSkill_Result> data;
    
        List<string> skillNames;
        int SelectedId, SelectedRating = 60;
        String SelectedSkill;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.addHobby);
            data = new List<GetAllSkill_Result>();
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(ApplicationContext);
            email = prefs.GetString("email", "empty");


            if (email.Equals("empty"))
            {
                StartActivity(typeof(MainActivity));
                this.Finish();
            }
           


            try
            {
                Task<string> task = getHobby();
                var x = JsonConvert.DeserializeObject<GetAllSkill_Result[]>(task.Result);
                foreach (GetAllSkill_Result a in x)
                {
                    data.Add(a);
                }
                skillNames = new List<string>();
                foreach (var item in data)
                    skillNames.Add(item.name);
            }
            catch (Exception e)
            {
                Toast.MakeText(this, "Error! Some Wrong Happen", ToastLength.Short).Show();
            }


            Spinner spinner = FindViewById<Spinner>(Resource.Id.skillspinner);

            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = new ArrayAdapter<string>(this,
             Android.Resource.Layout.SimpleSpinnerItem, skillNames);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            Button button = FindViewById<Button>(Resource.Id.AddSkill);
            if (skillNames.Count == 0)
            {
                button.Enabled = false;
            }




            button.Click += delegate {
                spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
              
                Task<string> task = AddHobbies(email, SelectedId);
                var x = JsonConvert.DeserializeObject(task.Result);
                skillNames.Remove(SelectedSkill);

                data.Remove(new GetAllSkill_Result { id = SelectedId, name = SelectedSkill });
                Toast.MakeText(this, "Hobby added Sucessfully", ToastLength.Short).Show();

                adapter = new ArrayAdapter<string>(this,
            Android.Resource.Layout.SimpleSpinnerItem, skillNames);

                adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                spinner.Adapter = adapter;



            };


            // Create your application here
        }
        public override void OnBackPressed()
        {

            StartActivity(typeof(Dashboard));
            this.Finish();
        }
        private async Task<string> getHobby()
        {
            var client = new HttpClient();
            string r = "";
            var jsonRequest = new { email = email };

            var serializedJsonRequest = JsonConvert.SerializeObject(jsonRequest);
            HttpContent content = new StringContent(serializedJsonRequest, Encoding.UTF8, "application/json");
            var result = await client.PostAsync("http://futurerecommend.azurewebsites.net/api/get/hobbies", content).ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
            {
                r = await result.Content.ReadAsStringAsync();
            }
            return r;
        }
        private async Task<string> AddHobbies(string email, int id)
        {
            var client = new HttpClient();





            string r = "2";
            var jsonRequest = new { email = email, id = id };

            var serializedJsonRequest = JsonConvert.SerializeObject(jsonRequest);
            HttpContent content = new StringContent(serializedJsonRequest, Encoding.UTF8, "application/json");

            var result = await client.PostAsync("http://futurerecommend.azurewebsites.net/api/add/hobbies", content).ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
            {
                r = await result.Content.ReadAsStringAsync();
            }
            return r;
        }
        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            SelectedId = data[e.Position].id;
            SelectedSkill = data[e.Position].name;

        }
    }
}