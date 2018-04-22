using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace FutureRecommenderApp.Resources.extra
{
    public class RecommendModel
    {
        public String UniversityName { set; get; }

        public String Degree { set; get; }
        public decimal fee { set; get; }
        public int rating { set; get; }
        public string url { set; get; }

    }
    class CusotmListAdapter : BaseAdapter<RecommendModel>
    {
        Activity context;
        List<RecommendModel> list;

        public CusotmListAdapter(Activity _context, List<RecommendModel> _list) : base()
        {

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

        public override RecommendModel this[int index]
        {
            get { return list[index]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;

            // re-use an existing view, if one is available
            // otherwise create a new one
            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.RecommendationItem, parent, false);

            RecommendModel item = this[position];
            view.FindViewById<TextView>(Resource.Id.university).Text = item.UniversityName;
            view.FindViewById<TextView>(Resource.Id.degree).Text = item.Degree;
            view.FindViewById<TextView>(Resource.Id.budget).Text ="fee: "+item.fee.ToString()+ " perSemester.";



            return view;
        }
    }
}