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
    public partial class check
    {
        public string email { get; set; }
        public int id { get; set; }
        public int rating { get; set; }

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
            TextView button2 = FindViewById<TextView>(Resource.Id.textView2);
            
                button2.Click += delegate {
                    SignUp();
                };
            button.Click += delegate {
                SignIn();
            };
            check c = new check { email = "usman@gmail.com", id = 8, rating = 20 };
            Task<string> task = tryfunc(c);
            var x = JsonConvert.DeserializeObject(task.Result);
        }


        public void SignUp()
        {
            StartActivity(typeof(SignUp));
        }

            public void SignIn()
        {
            EditText username = FindViewById<EditText>(Resource.Id.editText1);
            EditText password = FindViewById<EditText>(Resource.Id.editText2);
            String email = username.Text;
            String pass = password.Text;
            Task<string> task = Login(email, pass);
            var x = JsonConvert.DeserializeObject(task.Result);

            StartActivity(typeof(Dashboard));

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

        private async Task<string> tryfunc(check c)
        {
            var client = new HttpClient();


            string r = "1";
            var jsonRequest = new { email = "usman@gmail.com", id = 8, rating = 20 };
        

            var serializedJsonRequest = JsonConvert.SerializeObject(jsonRequest);
            HttpContent content = new StringContent(serializedJsonRequest, Encoding.UTF8, "application/x-www-form-urlencoded");
            var result = await client.PostAsync("http://futurerecommend.azurewebsites.net/api/add/skills", content).ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
            {
                r = await result.Content.ReadAsStringAsync();
            }
            return r;
        }


    }
}

