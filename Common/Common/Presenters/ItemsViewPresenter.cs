using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Common.Helper;
using Common.Helpers;
using Common.IViews;
using Common.Models;
using Newtonsoft.Json;

namespace Common.Presenters
{
    public class ItemsViewPresenter<T> : BaseViewPresenter, IDisposable
    {
        private readonly IDataStore<T> modelDataStore;
        private readonly IItemsView view;

        public ObservableCollection<T> Items { get; private set; }
        public Command LoadItemsCommand { get; private set; }
        public Command DeleteAllCommand { get; private set; }

        public ItemsViewPresenter(IItemsView view, IDataStore<T> model = null)
        {
            this.view = view;
            modelDataStore = model ?? ServiceLocator.Instance.Get<IDataStore<T>>();

            Init();
        }

        #region Public Methods

        public void Dispose()
        {

        }

        public void OnResume()
        {
            LoadItemsCommand.Execute(null);
        }

        public void AddItem()
        {
            view.GoToDetailView();
        }

        public void ViewItem(T item)
        {
            var strItem = JsonConvert.SerializeObject(item);
            view.GoToDetailView(strItem);
        }

        #endregion

        #region Private Methods

        private void Init()
        {
            Items = new ObservableCollection<T>();

            LoadItemsCommand = new Command(async () => await LoadItems());
            DeleteAllCommand = new Command<T>(async (T item) => await DeleteAllItems());
        }

        private async Task LoadItems()
        {

            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                (await modelDataStore.GetItemsAsync()).ForEach(x => Items.Add(x));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task DeleteAllItems()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                await modelDataStore.DeleteAllAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                IsBusy = false;
            }
        }

        #endregion
    }
}
