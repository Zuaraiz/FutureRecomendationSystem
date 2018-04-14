using Android.App;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System;
using System.Text;

namespace FutureRecommenderApp
{ public partial class UserSignIn_Result
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int location { get; set; }
        public int qualification { get; set; }
        public decimal percentage { get; set; }
        public decimal annualBudget { get; set; }
    }
    [Activity(Label = "FutureRecommenderApp", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Button button = FindViewById<Button>(Resource.Id.button1);

            button.Click += delegate {
                SignIn();
            };
        }

       

        public void SignIn()
        {
            EditText username = FindViewById<EditText>(Resource.Id.editText1);
            EditText password = FindViewById<EditText>(Resource.Id.editText2);
            String email = username.Text;
            String pass = password.Text;
            Task<string> task = Login(email, pass);
            var x = JsonConvert.DeserializeObject(task.Result);
       


        }
        private async Task<string> Login(string username, string password)
        {
            var client = new HttpClient();
            string r = "";
            var jsonRequest = new { email = username, password = password };

            var serializedJsonRequest = JsonConvert.SerializeObject(jsonRequest);
            HttpContent content = new StringContent(serializedJsonRequest, Encoding.UTF8, "application/json");
            var result = await client.PostAsync("http://futurerecommend.azurewebsites.net/api/Signin", content).ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
            {
                r = await result.Content.ReadAsStringAsync();
            }
            return r;
        }


    }
}

