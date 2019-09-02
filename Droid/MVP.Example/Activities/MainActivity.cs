using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Common.Entities;
using Common.IViews;
using Common.Models;
using Common.Presenters;
using MVP.Example.Adapters;
using Newtonsoft.Json;

namespace MVP.Example.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, IItemsView
    {
        private ItemsViewPresenter<Student> presenter;

        private ItemsAdapter adapterItem;
        private FloatingActionButton btnAdd;

        #region Overriden Methods

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            presenter = new ItemsViewPresenter<Student>(this);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.activity_main_toolbar);
            SetSupportActionBar(toolbar);

            var recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);

            recyclerView.HasFixedSize = true;
            recyclerView.SetAdapter(adapterItem = new ItemsAdapter(this, presenter));

            adapterItem.ItemClick += OnItemClick;

            btnAdd = FindViewById<FloatingActionButton>(Resource.Id.fab);
            btnAdd.Click += BtnAdd_Click;
        }

        protected override void OnResume()
        {
            base.OnResume();

            presenter.OnResume();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            btnAdd.Click -= BtnAdd_Click;
            btnAdd.Dispose();

            adapterItem.ItemClick -= OnItemClick;
            adapterItem.Dispose();

            presenter.Dispose();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            switch (id)
            {
                case Resource.Id.main_action_delete:
                    presenter.DeleteAllCommand.Execute(null);
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        #endregion

        #region Private Methods

        private void BtnAdd_Click(object sender, EventArgs eventArgs)
        {
            GoToDetailView();
        }

        private void OnItemClick(object sender, Student s)
        {
            var item = JsonConvert.SerializeObject(s);
            GoToDetailView(item);
        }

        public void GoToDetailView(string item = null)
        {
            using (Intent myIntent = new Intent(this, typeof(ActivityItemDetail)))
            using (Bundle bundle = new Bundle())
            {
                bundle.PutString("item", item);
                myIntent.PutExtras(bundle);

                StartActivity(myIntent);
            }
        }

        #endregion
    }
}

