using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
   
    class AddInterestAdapter: BaseAdapter<GetAllSkill_Result>
    {
        Activity context;
        List<GetAllSkill_Result> list;
        private List<KeyValuePair<string, int>> qual;
             List<string> qualNames;
        int Selected=60;
        String Email;

        public AddInterestAdapter(Activity _context, List<GetAllSkill_Result> _list,String email) : base()
        {
            this.Email = email;
            this.context = _context;

            this.list = _list;
            
            qual = new List<KeyValuePair<string, int>>();
           
                qual.Add(new KeyValuePair<string, int>("Very High", 100));
            qual.Add(new KeyValuePair<string, int>(" High", 80));
            qual.Add(new KeyValuePair<string, int>("Medium", 60));
            qual.Add(new KeyValuePair<string, int>("Low", 40));
            qual.Add(new KeyValuePair<string, int>("Very Low", 20));
            qualNames = new List<string>();
            foreach (var item in qual)
                qualNames.Add(item.Key);


        }

        public override int Count
        {
            get { return list.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override GetAllSkill_Result this[int index]
        {
            get { return list[index]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;

            // re-use an existing view, if one is available
            // otherwise create a new one
            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.Add_Skill_Interest_List_Item, parent, false);

            GetAllSkill_Result item = this[position];
            view.FindViewById<TextView>(Resource.Id._name_).Text = item.name;
            view.FindViewById<Spinner>(Resource.Id._rating_).ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected2);
            var adapter2 = new ArrayAdapter<string>(context,
             Android.Resource.Layout.SimpleSpinnerItem, qualNames);

            adapter2.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            view.FindViewById<Spinner>(Resource.Id._rating_).Adapter = adapter2;
            Button v = view.FindViewById<Button>(Resource.Id._add_);
            v.Click += (sender, e) => {
                try
                {
                    Task<string> task = AddInterest(Email, item.id, Selected);
                    var x = JsonConvert.DeserializeObject(task.Result);
                    if(task.IsCompleted)
                    {
                        task.Dispose();
                    }
                    
                    
                    v.Enabled = false;
                }
                catch
                {

                }
            };
            return view;
        }
        private void spinner_ItemSelected2(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            Selected = qual[e.Position].Value;

        }

        private async Task<string> AddInterest(string email, int id, int rating)
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