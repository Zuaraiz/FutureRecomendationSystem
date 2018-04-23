using Android.App;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System;
using System.Text;
using Android.Content;
using Android.Preferences;

namespace FutureRecommenderApp
{
    public partial class UserSignIn_Result
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int location { get; set; }
        public int qualification { get; set; }
        public decimal percentage { get; set; }
        public decimal annualBudget { get; set; }
    }
  
   
    [Activity(Label = "Future Councelor", Theme = "@android:style/Theme.Holo.Light.DarkActionBar", MainLauncher = true)]
    public class MainActivity : Activity
    {
        String email;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
            // Set our view from the "main" layout resource
     
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(ApplicationContext);
            email=prefs.GetString("email", "empty");
           
            if(email.Equals("empty"))
            {

            }
            else
            {
                StartActivity(typeof(Dashboard));
                this.Finish();

            }

            Button button = FindViewById<Button>(Resource.Id.button1);
            TextView button2 = FindViewById<TextView>(Resource.Id.textView2);
            
                button2.Click += delegate {
                    SignUp();
                };
            button.Click += delegate {
                SignIn();
            };
            
        }


        public void SignUp()
        {
            StartActivity(typeof(SignUp));
            this.Finish();
        }

            public void SignIn()
        {
            EditText username = FindViewById<EditText>(Resource.Id.editText1);
            EditText password = FindViewById<EditText>(Resource.Id.editText2);
            String email = username.Text;
            String pass = password.Text;
            Task<string> task = Login(email, pass);
            try
            {
                var x = JsonConvert.DeserializeObject<UserSignIn_Result>(task.Result);
                
                ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(ApplicationContext);
                ISharedPreferencesEditor editor = prefs.Edit();
                editor.PutString("email", email);
                editor.PutInt("id", x.id);
                
                editor.Apply();

                StartActivity(typeof(Dashboard));
                this.Finish();
            }
            catch(Exception e)
            {
                Toast.MakeText(this, "Error! Some Wrong Happen", ToastLength.Short).Show();

            }

        }
        private async Task<string> Login(string username, string password)
        {
            var client = new HttpClient();
            string r = "2";
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

