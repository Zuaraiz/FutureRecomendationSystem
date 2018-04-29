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
    [Activity(Label = "Add Interest")]
    public class AddInterest : Activity
    {
        String email;
        List<GetAllSkill_Result> data;
        private List<KeyValuePair<string, int>> qual;
        List<string> qualNames;
        List<string> skillNames;
        int SelectedId, SelectedRating = 60;
        String SelectedSkill;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.addInterest);
            data = new List<GetAllSkill_Result>();
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(ApplicationContext);
            email = prefs.GetString("email", "empty");
            qual = new List<KeyValuePair<string, int>>();


            qual.Add(new KeyValuePair<string, int>("Very High", 100));
            qual.Add(new KeyValuePair<string, int>(" High", 80));
            qual.Add(new KeyValuePair<string, int>("Medium", 60));
            qual.Add(new KeyValuePair<string, int>("Low", 40));
            qual.Add(new KeyValuePair<string, int>("Very Low", 20));
            qualNames = new List<string>();
            foreach (var item in qual)
                qualNames.Add(item.Key);
            if (email.Equals("empty"))
            {
                StartActivity(typeof(MainActivity));
                this.Finish();
            }

            try
            {
                Task<string> task = getInterest();
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

            Spinner spinner2 = FindViewById<Spinner>(Resource.Id.skillRatingSpinner);

            spinner2.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected2);
            var adapter2 = new ArrayAdapter<string>(this,
             Android.Resource.Layout.SimpleSpinnerItem, qualNames);

            adapter2.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner2.Adapter = adapter2;
            Button button = FindViewById<Button>(Resource.Id.AddSkill);
            if (skillNames.Count == 0)
            {
                button.Enabled = false;
            }




            button.Click += delegate
            {
                spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
                spinner2.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected2);
                Task<string> task = AddInteres(email, SelectedId, SelectedRating);
                var x = JsonConvert.DeserializeObject(task.Result);
                skillNames.Remove(SelectedSkill);

                data.Remove(new GetAllSkill_Result { id = SelectedId, name = SelectedSkill });
                Toast.MakeText(this, "Interest added Sucessfully", ToastLength.Short).Show();

                adapter = new ArrayAdapter<string>(this,
            Android.Resource.Layout.SimpleSpinnerItem, skillNames);

                adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                spinner.Adapter = adapter;



                // Create your application here
            };
            }
        public override void OnBackPressed()
        {

            StartActivity(typeof(Dashboard));
            this.Finish();
        }
        private async Task<string> getInterest()
        {
            var client = new HttpClient();
            string r = "";
            var jsonRequest = new { email = email };

            var serializedJsonRequest = JsonConvert.SerializeObject(jsonRequest);
            HttpContent content = new StringContent(serializedJsonRequest, Encoding.UTF8, "application/json");
            var result = await client.PostAsync("http://futurerecommend.azurewebsites.net/api/get/interests", content).ConfigureAwait(false);
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
        private void spinner_ItemSelected2(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            SelectedRating = qual[e.Position].Value;

        }
        private async Task<string> AddInteres(string email, int id, int rating)
        {
            var client = new HttpClient();





            string r = "2";
            var jsonRequest = new { email = email, id = id, rating = rating };

            var serializedJsonRequest = JsonConvert.SerializeObject(jsonRequest);
            HttpContent content = new StringContent(serializedJsonRequest, Encoding.UTF8, "application/json");

            var result = await client.PostAsync("http://futurerecommend.azurewebsites.net/api/add/Interests", content).ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
            {
                r = await result.Content.ReadAsStringAsync();
            }
            return r;
        }
    }
}