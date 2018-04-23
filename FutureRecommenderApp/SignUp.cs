using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace FutureRecommenderApp
{
    public partial class result
    {
        public int id { get; set; }
        public string name { get; set; }

    }
    [Activity(Label = "SignUp", Theme = "@android:style/Theme.Holo.Light.DarkActionBar")]
    public class SignUp : Activity
    {

        int citySelected=1, qualSelected=1;
        private List<KeyValuePair<string, int>> city;
        private List<KeyValuePair<string, int>> qual;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.signup);
            Task<string> task = getLocations();
           
            
            var x = JsonConvert.DeserializeObject<result[]>(task.Result);
            Task<string> task2 = getQualification();
            var y = JsonConvert.DeserializeObject<result[]>(task2.Result);
            city = new List<KeyValuePair<string, int>>();
            qual = new List<KeyValuePair<string, int>>();
            foreach(var a in x)
            {
                city.Add(new KeyValuePair<string, int>(a.name, a.id));
            }
            foreach (var b in y)
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


            Button button = FindViewById<Button>(Resource.Id.button1);
            
            button.Click += delegate {
                Signup();
            };
            // Create your application here
        }




        public void Signup()
        {
            
            EditText edit1 = FindViewById<EditText>(Resource.Id.editText1);
            EditText edit2 = FindViewById<EditText>(Resource.Id.editText2);
            EditText edit3 = FindViewById<EditText>(Resource.Id.editText3);
            EditText edit4 = FindViewById<EditText>(Resource.Id.editText4);
            EditText edit5 = FindViewById<EditText>(Resource.Id.editText5);
            EditText edit6 = FindViewById<EditText>(Resource.Id.editText6);



            String email = edit2.Text;
            String pass = edit4.Text;
            String fname = edit1.Text;
            String lname = edit3.Text;
            int budget = Int32.Parse(edit5.Text);
            int percentage = Int32.Parse(edit6.Text)/100;
            if (Int32.Parse(edit6.Text) <= 100 && Int32.Parse(edit6.Text) >= 40)
            {
                Task<string> task = register(email, pass, fname, lname, percentage, budget, citySelected, qualSelected);
                int x = JsonConvert.DeserializeObject<int>(task.Result);

                if (x == 0)
                {
                    Toast.MakeText(this, "Error! User Already Exists", ToastLength.Short).Show();
                }
                else if (x == 2)
                {
                    Toast.MakeText(this, "Error! Server down", ToastLength.Short).Show();

                }
                else if (x == -1)
                {
                    Toast.MakeText(this, "User registed Sucessfully!", ToastLength.Long).Show();
                    StartActivity(typeof(MainActivity));
                    this.Finish();
                }
            }
            else
            {
                Toast.MakeText(this, "Enter Percentage in Range(40-100)!", ToastLength.Long).Show();
            }




        }
        private async Task<string> register(string email, string password, string firstname, string lastname, int percentage, int annualbudget, int location, int qualification)
        {
            var client = new HttpClient();
            




            string r = "2";
            var jsonRequest = new { email=email, password=password, firstName=firstname, lastName=lastname, percentage=percentage, annualBudget=annualbudget, location=location, qualification =qualification};

            var serializedJsonRequest = JsonConvert.SerializeObject(jsonRequest);
            HttpContent content = new StringContent(serializedJsonRequest, Encoding.UTF8, "application/json");
            
            var result = await client.PutAsync("http://futurerecommend.azurewebsites.net/api/Signup", content).ConfigureAwait(false);
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