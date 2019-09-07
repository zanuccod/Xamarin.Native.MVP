
using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Common.Entities;
using Common.IViews;
using Common.Models;
using Common.Presenters;
using Newtonsoft.Json;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace MVP.Example.Activities
{
    [Activity(Label = "ActivityItemDetail", Theme = "@style/AppTheme.NoActionBar", ParentActivity = typeof(MainActivity))]
    public class ActivityItemDetail : AppCompatActivity, IItemDetailView
    {
        Toolbar toolbar;

        EditText editTextName, editTextCounty, editTextBornDate;
        FloatingActionButton btnSave;

        ItemsDetailViewPresenter<Student> presenter;

        #region Overriden Methods

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_item_detail);

            Title = "activity_item_detail";

            presenter = new ItemsDetailViewPresenter<Student>(this);

            toolbar = (Toolbar)FindViewById(Resource.Id.activity_item_detail_toolbar);
            SetSupportActionBar(toolbar);

            // allow to come back from back arrow button on action bar
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            editTextName = FindViewById<EditText>(Resource.Id.item_name);
            editTextCounty = FindViewById<EditText>(Resource.Id.item_country);
            editTextBornDate = FindViewById<EditText>(Resource.Id.item_born_date);

            btnSave = FindViewById<FloatingActionButton>(Resource.Id.btn_save);
            btnSave.Click += BtnSave_Click;

        }

        protected override void OnResume()
        {
            base.OnResume();

            presenter.OnResume();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            btnSave.Click -= BtnSave_Click;
            btnSave.Dispose();

            toolbar.Dispose();
            presenter.Dispose();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    NavUtils.NavigateUpFromSameTask(this);
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }

        #endregion

        #region Interface Methods

        public void PopulateViewValues(Student s)
        {
            editTextName.Text = s.Name;
            editTextCounty.Text = s.Country;
            editTextBornDate.Text = s.BornDate;
        }

        public Student GetStudentViewValues()
        {
            return new Student()
            {
                Name = editTextName.Text,
                Country = editTextCounty.Text,
                BornDate = editTextBornDate.Text
            };
        }

        public void CloseActivity()
        {
            Finish();
        }

        public void HideSaveBtn()
        {
            btnSave.Visibility = ViewStates.Gone;
        }

        public string GetDataFromParent()
        {
            if (Intent.HasExtra("item") && !string.IsNullOrEmpty(Intent.GetStringExtra("item")))
                return Intent.GetStringExtra("item");
            return null;
        }

        #endregion

        #region Private Methods

        private void BtnSave_Click(object sender, EventArgs e)
        {
            presenter.AddNewItem();
        }

        #endregion
    }
}
