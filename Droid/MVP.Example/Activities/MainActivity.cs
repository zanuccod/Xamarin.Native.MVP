using System;
using Android.App;
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

        private ItemsAdapter adapter;
        private FloatingActionButton addBtn;

        #region Overriden Methods

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            presenter = new ItemsViewPresenter<Student>(this, new StudentSqLiteDataStore());

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            var recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);

            recyclerView.HasFixedSize = true;
            recyclerView.SetAdapter(adapter = new ItemsAdapter(this, presenter));

            adapter.ItemClick += OnItemClick;

            addBtn = FindViewById<FloatingActionButton>(Resource.Id.fab);
            addBtn.Click += AddBtn_Click;

        }

        protected override void OnResume()
        {
            base.OnResume();

            if (presenter.Items.Count == 0)
                presenter.LoadItemsCommand.Execute(null);

            addBtn.Click += AddBtn_Click;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            addBtn.Click -= AddBtn_Click;
            addBtn.Dispose();

            adapter.ItemClick -= OnItemClick;
            adapter.Dispose();

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
            if (id == Resource.Id.action_settings)
            {
                return true;
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

        private void AddBtn_Click(object sender, EventArgs eventArgs)
        {
            GoToDetailView();
        }

        private void OnItemClick(object sender, Student s)
        {
            var item = JsonConvert.SerializeObject(s);
            GoToDetailView(item);
        }

        public void GoToDetailView(params string[] parameters)
        {
            
        }

        #endregion
    }
}

