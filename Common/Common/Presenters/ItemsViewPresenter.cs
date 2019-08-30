using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Common.Helpers;
using Common.IViews;
using Common.Models;

namespace Common.Presenters
{
    public class ItemsViewPresenter<T> : BaseViewPresenter, IDisposable
    {
        private readonly IDataStore<T> modelDataStore;
        private readonly IItemsView view;

        public ObservableCollection<T> Items { get; private set; }
        public Command LoadItemsCommand { get; private set; }
        public Command AddItemCommand { get; private set; }
        public Command DeleteAllCommand { get; private set; }

        public ItemsViewPresenter(IItemsView view, IDataStore<T> model)
        {
            this.view = view;
            modelDataStore = model;
            Init();
        }

        #region Public Methods

        public void Dispose()
        {
            modelDataStore.Dispose();
        }

        #endregion

        #region Private Methods

        private void Init()
        {
            Items = new ObservableCollection<T>();

            LoadItemsCommand = new Command(async () => await LoadItems());
            AddItemCommand = new Command<T>(async (T item) => await AddItem(item));
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

        private async Task AddItem(T item)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Add(item);
                await modelDataStore.AddItemAsync(item);
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
