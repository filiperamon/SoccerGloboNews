using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using FutebolNews.Entity;
using Android.Net;

namespace FutebolNews
{
    [Activity(Label = "Futebol News", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Button btnAdicionar;
        private Spinner spinner;
        private ListView listaTimes;        
        private ArrayAdapter<string> ListAdapter;
        private List<string> itemsAdicionados;
       
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);            
            SetContentView(Resource.Layout.Main);           

            listaTimes = FindViewById<ListView>(Resource.Id.listTimes);

            btnAdicionar = FindViewById<Button>(Resource.Id.btnAdicionar);
            btnAdicionar.Click += delegate 
            {
                atualizaLista(itemsAdicionados);
            };

            RegisterForContextMenu(listaTimes);
            listaTimes.ItemClick += ListaTimes_ItemClick;

            spinner = FindViewById<Spinner>(Resource.Id.spinner1);

            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.times_array, Android.Resource.Layout.SimpleSpinnerItem);            

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;
        }

        public void atualizaLista(List<string> itemsAdicionados)
        {
            ListAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, itemsAdicionados);
            listaTimes.Adapter = ListAdapter;
        }

        public override void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
        {
            if (v.Id == Resource.Id.listTimes)
            {
                var info = (AdapterView.AdapterContextMenuInfo)menuInfo;
                menu.SetHeaderTitle(itemsAdicionados[info.Position]);
                var menuItems = Resources.GetStringArray(Resource.Array.menu);
                for (var i = 0; i < menuItems.Length; i++)
                    menu.Add(Menu.None, i, i, menuItems[i]);
            }
        }

        public override bool OnContextItemSelected(IMenuItem item)
        {
            var info = (AdapterView.AdapterContextMenuInfo)item.MenuInfo;
            var menuItemIndex = item.ItemId;
            var menuItems = Resources.GetStringArray(Resource.Array.menu);
            var menuItemName = menuItems[menuItemIndex];
            var listItemName = itemsAdicionados[info.Position];

            itemsAdicionados.RemoveAt(info.Position);
            Toast.MakeText(this, string.Format("Exluido com sucesso!"), ToastLength.Short).Show();
            atualizaLista(itemsAdicionados);            
            return true;
        }

        private void ListaTimes_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            if (isOnline(this.ApplicationContext))
            {
                string selecao = this.listaTimes.GetItemAtPosition(e.Position).ToString();
                List<Times> times = new CatalogoTimes().getTimes();
                Times timeSelecionado = new Times();

                foreach (Times item in times)
                {
                    if (item.nome == selecao)
                        timeSelecionado = item;
                }

                Intent intent = new Intent(this, typeof(NewspaperActivity));
                Bundle b = new Bundle();
                b.PutString("RootObject", timeSelecionado.link);
                intent.PutExtras(b);
                StartActivity(intent);
            }
            else
            {
                Toast.MakeText(this, string.Format("Sem conexão com a internet."), ToastLength.Short).Show();
            }
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

            if (e.Position > 0)
            {
                if (itemsAdicionados == null)
                {
                    itemsAdicionados = new List<string>();
                }

                itemsAdicionados.Add(string.Format("" + spinner.GetItemAtPosition(e.Position)));                
            }
        }

        private bool isOnline(Context context)
        {
            ConnectivityManager cm = (ConnectivityManager)context.GetSystemService(Context.ConnectivityService);// CONNECTIVITY_SERVICE);

            NetworkInfo activeConnection = cm.ActiveNetworkInfo;
            bool isOnline = (activeConnection != null) && activeConnection.IsConnected;
            return isOnline;
        }
    }
}

