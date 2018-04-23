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
    [Activity(Label = "DeleteHobbies")]
    public class DeleteHobbies : Activity
    {
        String email;
        List<String> data;
        int SelectedId, SelectedRating = 60;
        String SelectedSkill;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.deleteHobby);
            data = new List<String>();
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(ApplicationContext);
            email = prefs.GetString("email", "empty");

            if (email.Equals("empty"))
            {
                StartActivity(typeof(MainActivity));
                this.Finish();
            }

            try
            {
                Task<string> task = getUserHobby();
                var x = JsonConvert.DeserializeObject<String[]>(task.Result);
                foreach (String a in x)
                {
                    data.Add(a);
                }
              
            }
            catch (Exception e)
            {
                Toast.MakeText(this, "Error! Some Wrong Happen", ToastLength.Short).Show();
            }


            Spinner spinner = FindViewById<Spinner>(Resource.Id.skillspinner);

            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = new ArrayAdapter<string>(this,
             Android.Resource.Layout.SimpleSpinnerItem, data);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            Button button = FindViewById<Button>(Resource.Id.AddSkill);
            if (data.Count == 0)
            {
                button.Enabled = false;
            }




            button.Click += delegate {
                spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);

                Task<string> task = DeleteInterest(email, SelectedSkill);
                var x = JsonConvert.DeserializeObject(task.Result);
                data.Remove(SelectedSkill);

              
                Toast.MakeText(this, "Skill Deleted Sucessfully", ToastLength.Short).Show();

                adapter = new ArrayAdapter<string>(this,
            Android.Resource.Layout.SimpleSpinnerItem, data);

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
        private async Task<string> getUserHobby()
        {
            var client = new HttpClient();
            string r = "";
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
        private async Task<string> DeleteInterest(string email, string name)
        {
            var client = new HttpClient();





            string r = "2";
            var jsonRequest = new { email = email, value = name };

            var serializedJsonRequest = JsonConvert.SerializeObject(jsonRequest);
            HttpContent content = new StringContent(serializedJsonRequest, Encoding.UTF8, "application/json");

            var result = await client.PostAsync("http://futurerecommend.azurewebsites.net/api/delete/interest", content).ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
            {
                r = await result.Content.ReadAsStringAsync();
            }
            return r;
        }
        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
          
            SelectedSkill = data[e.Position];

        }
    }
}