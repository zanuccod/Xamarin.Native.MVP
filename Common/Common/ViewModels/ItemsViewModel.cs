using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Common.Helpers;
using Common.Models;

namespace Common.ViewModels
{
    public class ItemsViewModel<T> : BaseViewModel, IDisposable
    {
        private readonly IDataStore<T> modelDataStore;

        public ObservableCollection<T> Items { get; private set; }
        public Command LoadItemsCommand { get; private set; }
        public Command AddItemCommand { get; private set; }
        public Command DeleteAllCommand { get; private set; }

        public ItemsViewModel(IDataStore<T> model)
        {
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
