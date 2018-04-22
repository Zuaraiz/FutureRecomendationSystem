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
   
    class AddHobbyAdapter: BaseAdapter<GetAllSkill_Result>
    {
        Activity context;
        List<GetAllSkill_Result> list;
        String Email;
      

        public AddHobbyAdapter(Activity _context, List<GetAllSkill_Result> _list,string email) : base()
        {
            this.Email = email;
            this.context = _context;

            this.list = _list;
            
        


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
                view = context.LayoutInflater.Inflate(Resource.Layout.Add_Hobby_List_Item, parent, false);

            GetAllSkill_Result item = this[position];
            view.FindViewById<TextView>(Resource.Id._name_).Text = item.name;
           
            Button v = view.FindViewById<Button>(Resource.Id._add_);
            v.Click += (sender, e) => {
                try
                {
                    Task<string> task = AddHobby(Email, item.id);
                    var x = JsonConvert.DeserializeObject(task.Result);
                    v.Enabled = false;
                }
                catch
                {

                }
            };
            return view;
        }
       
        private async Task<string> AddHobby(string email, int id)
        {
            var client = new HttpClient();





            string r = "2";
            var jsonRequest = new { email = email, id = id};

            var serializedJsonRequest = JsonConvert.SerializeObject(jsonRequest);
            HttpContent content = new StringContent(serializedJsonRequest, Encoding.UTF8, "application/json");

            var result = await client.PostAsync("http://futurerecommend.azurewebsites.net/api/add/hobbies", content).ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
            {
                r = await result.Content.ReadAsStringAsync();
            }
            return r;
        }
    }
}