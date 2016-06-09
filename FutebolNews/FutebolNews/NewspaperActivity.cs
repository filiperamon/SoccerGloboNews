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
using Android.Support.V7.Widget;
using FutebolNews.Entity;
using FutebolNews.Server;
using Android.Text;

namespace FutebolNews
{
    [Activity(Label = "NewspaperActivity")]
    public class NewspaperActivity : Activity
    {        
        RecyclerView mRecyclerView;
        RecyclerView.LayoutManager mLayoutManager;
        NewspaparAdapter mAdapter;
        Channel mNewspaper;
        ServiceGetRss serviceRest;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            mNewspaper = new Channel();
            serviceRest = new ServiceGetRss();

            SetContentView(Resource.Layout.Newspaper);            
            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            
            mLayoutManager = new LinearLayoutManager(this);

            mRecyclerView.SetLayoutManager(mLayoutManager);

            mNewspaper = serviceRest.getRssNews(Intent.GetStringExtra("RootObject"));
            this.Title = "Notidias do " + mNewspaper.title;

            mAdapter = new NewspaparAdapter(mNewspaper);
            mAdapter.ItemClick += OnItemClick;

            RegisterForContextMenu(mRecyclerView);
            mRecyclerView.SetAdapter(mAdapter);
        }
        
        void OnItemClick(object sender, int position)
        {
            var uri = Android.Net.Uri.Parse(mNewspaper.item[position].link);
            var intent = new Intent(Intent.ActionView, uri);
            StartActivity(intent);
        }   
    }

    public class NewsViewHolder : RecyclerView.ViewHolder
    {
        public ImageView Image { get; private set; }
        public TextView Detalhe { get; private set; }        

        // Get references to the views defined in the CardView layout.
        public NewsViewHolder(View itemView, Action<int> listener)
            : base(itemView)
        {
            Image = itemView.FindViewById<ImageView>(Resource.Id.imageView);
            Detalhe = itemView.FindViewById<TextView>(Resource.Id.textView);
            
            itemView.Click += (sender, e) => listener(base.Position);
        }
    }


    public class NewspaparAdapter : RecyclerView.Adapter
        {
            // Event handler for item clicks:
            public event EventHandler<int> ItemClick;

            // Underlying data set (newspaper):
            public Channel mNewspaper;

            // Load the adapter with the data set (newspaper) at construction time:
            public NewspaparAdapter(Channel pNewspaper)
            {
                mNewspaper = pNewspaper;
            }

            public override RecyclerView.ViewHolder
                OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                // Inflate the CardView for the news:
                View itemView = LayoutInflater.From(parent.Context).
                            Inflate(Resource.Layout.NewsItem, parent, false);

            NewsViewHolder vh = new NewsViewHolder(itemView, OnClick);
                return vh;
            }

            public override void
                OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {
                NewsViewHolder vh = holder as NewsViewHolder;

            vh.Image.SetImageBitmap(mNewspaper.item[position].urlImg);
            // ISpanned sp = Android.Text.Html.FromHtml(mNewspaper.item[position].description);
            vh.Detalhe.Text = Html.FromHtml(mNewspaper.item[position].title + @"<\br>" + mNewspaper.item[position].description).ToString();
            }
            
            public override int ItemCount
            {
                get { return mNewspaper.item.Count; }
            }
            
            void OnClick(int position)
            {
                if (ItemClick != null)
                    ItemClick(this, position);
            }

        }    
}