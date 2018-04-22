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
    [Activity(Label = "AddHobby")]
    public class AddHobby : Activity
    {
        String email;
        List<GetAllSkill_Result> data;

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
            ListView listView = FindViewById<ListView>(Resource.Id.listViewHobby);
            try
            {
                Task<string> task = getHobby();
                var x = JsonConvert.DeserializeObject<GetAllSkill_Result[]>(task.Result);
                foreach (GetAllSkill_Result a in x)
                {
                    data.Add(a);
                }
            }
            catch (Exception e)
            {
                Toast.MakeText(this, "Error! Some Wrong Happen", ToastLength.Short).Show();
            }


            listView.Adapter = new AddHobbyAdapter(this, data,email);


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
       
    }
}