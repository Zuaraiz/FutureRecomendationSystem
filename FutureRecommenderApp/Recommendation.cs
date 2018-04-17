using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FutureRecommenderApp.Resources.extra;

namespace FutureRecommenderApp
{
  
    [Activity(Label = "Recommendation")]
    public class Recommendation : Activity
    {
        ListView listView;
        List<RecommendModel> data;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            data = new List<RecommendModel>();
            SetContentView(Resource.Layout.Recommendation);
            listView = FindViewById<ListView>(Resource.Id.listView1);
            Task<string> task = recommeds();
            var x = JsonConvert.DeserializeObject<RecommendModel[]>(task.Result);
            foreach (RecommendModel a in x)
            {
                data.Add(a);
            }
           
            
            listView.Adapter = new CusotmListAdapter(this, data);

            // Create your application here
        }

        private async Task<string> recommeds()
        {
            var client = new HttpClient();
            string r = "";
            var jsonRequest = new { email = "usman@gmail.com" };

            var serializedJsonRequest = JsonConvert.SerializeObject(jsonRequest);
            HttpContent content = new StringContent(serializedJsonRequest, Encoding.UTF8, "application/json");
            var result = await client.PostAsync("http://futurerecommend.azurewebsites.net/api/get/Recommendation", content).ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
            {
                r = await result.Content.ReadAsStringAsync();
            }
            return r;
        }

    }
}